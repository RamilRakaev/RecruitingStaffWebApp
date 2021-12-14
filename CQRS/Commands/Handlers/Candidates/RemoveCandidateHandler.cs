using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using RecruitingStaff.Domain.Interfaces;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire;
using RecruitingStaff.Domain.Model.CandidateQuestionnaire.CandidateData;
using RecruitingStaff.Domain.Model.Options;
using RecruitingStaff.Infrastructure.CQRS.Commands.RemoveCommandHandlers;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.Candidates;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers.Candidates
{
    public class RemoveCandidateHandler : CandidateCommandHandlers, IRequestHandler<RemoveCandidateCommand, bool>
    {
        public RemoveCandidateHandler(IRepository<Answer> answerRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<Option> optionRepository,
            IRepository<RecruitingStaffWebAppFile> fileRepository,
            IOptions<WebAppOptions> options,
            IWebHostEnvironment webHost) 
            : base(answerRepository,
                  candidateRepository,
                  optionRepository,
                  fileRepository,
                  options,
                  webHost)
        {
        }

        public async Task<bool> Handle(RemoveCandidateCommand request, CancellationToken cancellationToken)
        {
            await RemoveCandidate(request.CandidateId, cancellationToken);
            return true;
        }
    }
}
