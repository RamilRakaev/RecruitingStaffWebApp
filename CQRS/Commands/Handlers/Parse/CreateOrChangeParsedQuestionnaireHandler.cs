using MediatR;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand.Maps;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Parse
{
    public class CreateOrChangeParsedQuestionnaireHandler : IRequestHandler<CreateOrChangeParsedQuestionnaireCommand, Questionnaire>
    {
        private readonly IMediator _mediator;
        private RecruitingStaffWebAppFile _file;

        public CreateOrChangeParsedQuestionnaireHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Questionnaire> Handle(CreateOrChangeParsedQuestionnaireCommand request, CancellationToken cancellationToken)
        {
            Questionnaire questionnaire = await CreateOrChangeQuestionnaire(
                request.ParsedData.Vacancy.Name,
                request.ParsedData.Questionnaire.Name,
                cancellationToken);
            foreach (var questionCategoryItem in request.ParsedData.Questionnaire.ChildElements)
            {
                QuestionCategory questionCategory = new()
                {
                    Name = questionCategoryItem.Name,
                    QuestionnaireId = questionnaire.Id,
                };
                await _mediator.Send(new CreateOrChangeEntityCommand<QuestionCategory>(questionCategory), cancellationToken);
                foreach (var questionItem in questionCategoryItem.ChildElements)
                {
                    Question question = new()
                    {
                        Name = questionItem.Name,
                        QuestionCategoryId = questionCategory.Id,
                    };
                    await _mediator.Send(new CreateOrChangeEntityCommand<Question>(question), cancellationToken);
                }
            }
            await CreateQuestionnaireDocument(questionnaire, request.ParsedData.FileExtension);
            request.ParsedData.FileSource = _file.Name;
            return questionnaire;
        }

        private async Task<Questionnaire> CreateOrChangeQuestionnaire(
            string vacancyName,
            string questionnaireName,
            CancellationToken cancellationToken)
        {
            Vacancy vacancy = new()
            {
                Name = vacancyName,
            };
            await _mediator.Send(new CreateOrChangeEntityCommand<Vacancy>(vacancy), cancellationToken);
            var vacancyQuestionnaires = await _mediator.Send(
            new GetEntitiesByForeignKeyQuery<VacancyQuestionnaire>(cq => cq.FirstEntityId == vacancy.Id),
            cancellationToken);
            Questionnaire questionnaire = new()
            {
                Id = vacancyQuestionnaires.Length == 0 ? 0 : vacancyQuestionnaires.First().SecondEntityId,
                Name = questionnaireName,
            };
            await _mediator.Send(new CreateOrChangeEntityCommand<Questionnaire>(questionnaire), cancellationToken);
            if (vacancyQuestionnaires.Length == 0)
            {
                await _mediator.Send(new TryCreateMapCommand<VacancyQuestionnaire>(vacancy.Id, questionnaire.Id),
                    cancellationToken);
            }
            return questionnaire;
        }

        private async Task CreateQuestionnaireDocument(Questionnaire questionnaire, string fileExtension)
        {
            _file = new()
            {
                Name = $"{questionnaire.Id}.{questionnaire.Name}{fileExtension}",
                FileType = FileType.QuestionnaireExample,
                QuestionnaireId = questionnaire.Id,
            };
            await _mediator.Send(new CreateEntityCommand<RecruitingStaffWebAppFile>(_file));
        }
    }
}
