using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class GetQuestionCategoriesQuery : IRequest<QuestionCategory[]>
    {
    }
}
