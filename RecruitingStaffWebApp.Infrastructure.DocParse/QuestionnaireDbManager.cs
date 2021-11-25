using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Services.DocParse;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireDbManager
    {
        private readonly IMediator _mediator;

        public QuestionnaireDbManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public RecruitingStaffWebAppFile File { get; private set; }

        public async Task SaveParsedData(ParsedData parsedData)
        {
            var vacancy = await _mediator.Send(new CreateOrChangeVacancyCommand(parsedData.Vacancy));

            parsedData.Questionnaire.VacancyId = vacancy.Id;
            var questionnaire = await _mediator.Send(new CreateOrChangeQuestionnaireCommand(parsedData.Questionnaire));

            var candidate = await _mediator.Send(new CreateCandidateCommand(parsedData.Candidate, vacancy.Id, questionnaire.Id));
            await CreateFile(parsedData, candidate.Id, questionnaire.Id);

            foreach (var questionCategory in parsedData.QuestionCategories)
            {
                if (await _mediator.Send(new ContainsQuestionCategoryByNameQuery(questionCategory.Name)) == false)
                {
                    questionCategory.QuestionnaireId = questionnaire.Id;
                    await _mediator.Send(new CreateOrChangeQuestionCategoryCommand(questionCategory));
                }
            }

            foreach (var question in parsedData.Questions)
            {
                if (await _mediator.Send(new ContainsQuestionByNameQuery(question.Name)) == false)
                {
                    question.QuestionCategoryId = question.QuestionCategory.Id;
                    await _mediator.Send(new CreateOrChangeQuestionCommand(question));
                }
            }

            foreach (var answer in parsedData.Answers)
            {
                answer.QuestionId = answer.Question.Id;
                answer.CandidateId = answer.Candidate.Id;
                await _mediator.Send(new CreateOrChangeAnswerCommand(answer));
            }
        }

        private async Task CreateFile(ParsedData parsedData, int candidateId, int questionnaireId)
        {
            File = new RecruitingStaffWebAppFile()
            {
                Source = $"{candidateId}.{parsedData.Candidate.FullName}.docx",
                FileType = FileType.Questionnaire,
                CandidateId = candidateId,
                QuestionnaireId = questionnaireId,
            };
            await _mediator.Send(new CreateRecruitingStaffWebAppFileCommand(File));
        }
    }
}
