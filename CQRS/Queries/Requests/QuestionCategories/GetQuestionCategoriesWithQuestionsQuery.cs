using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetQuestionCategoriesWithQuestionsQuery : IRequest<QuestionCategory[]>
    {
        public GetQuestionCategoriesWithQuestionsQuery(int questionnaireId)
        {
            QuestionnaireId = questionnaireId;
        }

        public int QuestionnaireId { get; set; }
    }
}
