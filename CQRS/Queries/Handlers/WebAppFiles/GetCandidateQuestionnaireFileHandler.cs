using MediatR;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.WebAppFiles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitingStaff.Infrastructure.CQRS.Queries.Handlers.WebAppFiles
{
    public class GetCandidateQuestionnaireFileHandler : IRequestHandler<GetCandidateQuestionnaireFileQuery, RecruitingStaffWebAppFile>
    {
        private readonly IMediator _mediator;

        public GetCandidateQuestionnaireFileHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RecruitingStaffWebAppFile> Handle(GetCandidateQuestionnaireFileQuery request, CancellationToken cancellationToken)
        {
            var files = await _mediator.Send(new GetEntitiesByForeignKeyQuery<RecruitingStaffWebAppFile>(f =>
            f.CandidateId.GetValueOrDefault() == request.CandidateId
            && f.QuestionnaireId.GetValueOrDefault() == request.QuestionnaireId),
            cancellationToken);
            return files.FirstOrDefault();
        }
    }
}
