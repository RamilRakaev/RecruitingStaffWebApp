using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires
{
    public class GetQuestionnaireByNameQuery : IRequest<Questionnaire>
    {
        public GetQuestionnaireByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
