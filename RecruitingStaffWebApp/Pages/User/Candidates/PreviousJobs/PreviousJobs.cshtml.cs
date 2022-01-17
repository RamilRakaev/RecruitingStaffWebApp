using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.PreviousJobs;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.PreviousJobs
{
    public class PreviousJobsModel : BasePageModel
    {
        public PreviousJobsModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public int CandidateId { get; set; }
        public PreviousJobPlacementViewModel[] PreviousJobPlacementViewModels { get; set; }

        public async Task OnGet(int candidateId)
        {
            PreviousJobPlacementViewModels = await Initialization(candidateId);
        }

        public async Task OnPost(int candidateId, int previousJobPlacementId)
        {
            await _mediator.Send(new RemoveEntityCommand<PreviousJobPlacement>(previousJobPlacementId));
            PreviousJobPlacementViewModels = await Initialization(candidateId);
        }

        private async Task<PreviousJobPlacementViewModel[]> Initialization(int candidateId)
        {
            CandidateId = candidateId;
            var previousJobPlacements = await _mediator.Send(new GetJobsByCandidateIdQuery(candidateId));
            var config = new MapperConfiguration(c => c.CreateMap<PreviousJobPlacement, PreviousJobPlacementViewModel>()
            .ForMember(c => c.RecommenderNames, c => c.MapFrom(list => list.Recommenders.Select(r => r.Name))));
            var mapper = new Mapper(config);
            return mapper.Map<PreviousJobPlacementViewModel[]>(previousJobPlacements);
        }
    }
}
