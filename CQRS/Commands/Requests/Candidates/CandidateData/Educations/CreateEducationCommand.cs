using MediatR;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates.CandidateData.Educations
{
    public class CreateEducationCommand : IRequest<Education>
    {
        public CreateEducationCommand(Education education)
        {
            Education = education;
        }

        public Education Education { get; set; }
    }
}
