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
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.PreviousJobs
{
    public class RecommendersModel : BasePageModel
    {
        public RecommendersModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public int CandidateId { get; set; }
        public int PreviousJobPlacementId { get; set; }
        public RecommenderViewModel[] RecommenderViewModels { get; set; }

        public async Task OnGet(int candidateId, int previousJobPlacementId)
        {
            RecommenderViewModels = await Initialization(candidateId, previousJobPlacementId);
        }

        public async Task OnPost(int recommenderId)
        {
            var recommenderEntity = await _mediator.Send(new RemoveEntityCommand<Recommender>(recommenderId));
            RecommenderViewModels = await Initialization(recommenderEntity.CandidateId, recommenderEntity.PreviousJobId);
        }

        private async Task<RecommenderViewModel[]> Initialization(int candidateId, int previousJobPlacementId)
        {
            CandidateId = candidateId;
            PreviousJobPlacementId = previousJobPlacementId;
            var previousJobPlacements = await _mediator.Send(new GetRecommendersQuery(previousJobPlacementId)); ;
            var config = new MapperConfiguration(c => c.CreateMap<Recommender, RecommenderViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<RecommenderViewModel[]>(previousJobPlacements);
        }
    }
}
