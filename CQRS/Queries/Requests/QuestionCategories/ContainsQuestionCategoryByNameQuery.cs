using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.QuestionCategories
{
    public class ContainsQuestionCategoryByNameQuery : IRequest<bool>
    {
        public ContainsQuestionCategoryByNameQuery(string questionCategoryName)
        {
            QuestionCategoryName = questionCategoryName;
        }

        public string QuestionCategoryName { get; set; }
    }
}
