using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Parse;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaffWebApp.Services.DocParse;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Parse
{
    public class CreateParsedAnswersAndCandidateDataHandler : IRequestHandler<CreateParsedAnswersAndCandidateDataCommand, bool>
    {
        private readonly IMediator _mediator;
        private RecruitingStaffWebAppFile _file;

        public CreateParsedAnswersAndCandidateDataHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateParsedAnswersAndCandidateDataCommand request, CancellationToken cancellationToken)
        {
            Vacancy vacancy = new()
            {
                Name = request.ParsedData.VacancyParsedData.Name,
            };
            await _mediator.Send(new CreateOrChangeVacancyCommand(vacancy));
            Questionnaire questionnaire = new()
            {
                Name = request.ParsedData.QuestionnaireRp.Name,
                VacancyId = vacancy.Id,
            };
            await _mediator.Send(new CreateOrChangeQuestionnaireCommand(questionnaire));
            foreach (var questionCategoryItem in request.ParsedData.QuestionnaireRp.ChildElements)
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
                    foreach (var answerItem in questionItem.ChildElements)
                    {
                        await _mediator.Send(new CreateAnswerCommand(
                            new()
                            {
                                Text = answerItem.Name,
                                QuestionId = question.Id,
                                CandidateId = request.ParsedData.CandidateId,
                            }
                            ), cancellationToken);
                    }
                }
            }
            await CreateCandidateDocument(request.ParsedData, request.ParsedData.CandidateId, questionnaire.Id);
            request.ParsedData.FileSource = _file.Source;
            return true;
        }

        private async Task CreateCandidateDocument(ParsedData parsedData, int candidateId, int questionnaireId)
        {
            var candidate = await _mediator.Send(new GetCandidateQuery(candidateId));
            _file = new()
            {
                Source = $"{candidateId}.{candidate.FullName}{parsedData.FileExtension}",
                FileType = FileType.Questionnaire,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(_file));
        }
    }
}
