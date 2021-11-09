using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions
{
    public class GetQuestionsByQuestionCategoryQuery : IRequest<Question[]>
    {
        public GetQuestionsByQuestionCategoryQuery(QuestionCategory[] questionCategories)
        {
            QuestionCategories = questionCategories;
        }

        public QuestionCategory[] QuestionCategories { get; set; }
    }
}
