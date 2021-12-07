using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaffWebApp.Services.DocParse.Model;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Questionnaires
{
    public class CreateOrChangeParsedQuestionnaireCommand : IRequest<Questionnaire>
    {
        public CreateOrChangeParsedQuestionnaireCommand(QuestionnaireElement questionnaire)
        {
            Questionnaire = questionnaire;
        }

        public QuestionnaireElement Questionnaire { get; set; }
        public VacancyParsedData Vacancy { get; set; }
    }
}
