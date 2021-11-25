using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Answers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Educations;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.PreviousJobs;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Recommenders;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questions;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Vacancies;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.WebAppFiles;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Candidates;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions;
using RecruitingStaffWebApp.Services.DocParse;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Infrastructure.DocParse
{
    public class QuestionnaireDbManager
    {
        private readonly IMediator _mediator;
        private ParsedData _parsedData;

        public QuestionnaireDbManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public RecruitingStaffWebAppFile File { get; private set; }

        public async Task SaveParsedData(ParsedData parsedData)
        {
            _parsedData = parsedData;
            var vacancy = await _mediator.Send(new CreateOrChangeVacancyCommand(parsedData.Vacancy));

            parsedData.Questionnaire.VacancyId = vacancy.Id;
            var questionnaire = await _mediator.Send(new CreateOrChangeQuestionnaireCommand(parsedData.Questionnaire));

            var candidate = await _mediator.Send(new CreateCandidateCommand(parsedData.Candidate, vacancy.Id, questionnaire.Id));
            await CreateFile(parsedData, candidate.Id, questionnaire.Id);

            await SaveCandidateData();
            await SaveQuestionnaire(questionnaire.Id);
        }

        private async Task SaveCandidateData()
        {
            foreach (var education in _parsedData.Educations)
            {
                education.CandidateId = education.Candidate.Id;
                await _mediator.Send(new CreateEducationCommand(education));
            }
            foreach (var previousJob in _parsedData.PreviousJobs)
            {
                previousJob.CandidateId = previousJob.CandidateId;
                await _mediator.Send(new CreatePreviousJobCommand(previousJob));
            }
            foreach (var recommender in _parsedData.Recommenders)
            {
                recommender.PreviousJobId = recommender.PreviousJob.Id;
                await _mediator.Send(new CreateRecommenderCommand(recommender));
            }
        }

        private async Task SaveQuestionnaire(int questionnaireId)
        {
            foreach (var questionCategory in _parsedData.QuestionCategories)
            {
                if (await _mediator.Send(new ContainsQuestionCategoryByNameQuery(questionCategory.Name)) == false)
                {
                    questionCategory.QuestionnaireId = questionnaireId;
                    await _mediator.Send(new CreateOrChangeQuestionCategoryCommand(questionCategory));
                }
            }
            foreach (var question in _parsedData.Questions)
            {
                if (await _mediator.Send(new ContainsQuestionByNameQuery(question.Name)) == false)
                {
                    question.QuestionCategoryId = question.QuestionCategory.Id;
                    await _mediator.Send(new CreateOrChangeQuestionCommand(question));
                }
            }
            foreach (var answer in _parsedData.Answers)
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
