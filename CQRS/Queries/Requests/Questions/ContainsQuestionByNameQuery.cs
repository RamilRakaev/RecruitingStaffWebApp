using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questions
{
    public class ContainsQuestionByNameQuery : IRequest<bool>
    {
        public ContainsQuestionByNameQuery(string questionName)
        {
            QuestionName = questionName;
        }

        public string QuestionName { get; set; }
    }
}
