using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

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
