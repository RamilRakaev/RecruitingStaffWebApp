using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
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
            Vacancy vacancy = new()
            {
                Name = request.ParsedData.Vacancy.Name,
            };
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy), cancellationToken);
            Questionnaire questionnaire = new()
            {
                Name = request.ParsedData.Questionnaire.Name,
                VacancyId = vacancy.Id,
            };
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire), cancellationToken);
            foreach (var questionCategoryItem in request.ParsedData.Questionnaire.ChildElements)
            {
                QuestionCategory questionCategory = new()
                {
                    Name = questionCategoryItem.Name,
                    QuestionnaireId = questionnaire.Id,
                };
                await _mediator.Send(new CreateOrChangeQuestionCategoryCommand(questionCategory), cancellationToken);
                foreach (var questionItem in questionCategoryItem.ChildElements)
                {
                    Question question = new()
                    {
                        Name = questionItem.Name,
                        QuestionCategoryId = questionCategory.Id,
                    };
                    await _mediator.Send(new CreateOrChangeQuestionCommand(question), cancellationToken);
                }
            }
            await CreateQuestionnaireDocument(questionnaire, request.ParsedData.FileExtension);
            request.ParsedData.FileSource = _file.Source;
            return questionnaire;
        }

        private async Task CreateQuestionnaireDocument(Questionnaire questionnaire, string fileExtension)
        {
            _file = new()
            {
                Source = $"{questionnaire.Id}.{questionnaire.Name}{fileExtension}",
                FileType = FileType.Questionnaire,
                QuestionnaireId = questionnaire.Id,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(_file));
        }
    }
}
