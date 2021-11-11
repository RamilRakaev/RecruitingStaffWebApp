using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Requests.Questionnaires
{
    public class GetQuestionnairesByNameFragmentQuery : IRequest<Questionnaire[]>
    {
        public GetQuestionnairesByNameFragmentQuery(string nameFragment)
        {
            NameFragment = nameFragment;
        }

        public string NameFragment { get; set; }
    }
}
