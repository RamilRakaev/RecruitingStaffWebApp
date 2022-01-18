using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.PreviousJobs
{
    public class CreateRecommenderModel : BasePageModel
    {
        public CreateRecommenderModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public RecommenderViewModel RecommenderViewModel { get; set; }

        public void OnGet(int candidateId, int previousJobPlacementId)
        {
            RecommenderViewModel = new()
            {
                CandidateId = candidateId,
                PreviousJobId = previousJobPlacementId
            };
        }

        public async Task<IActionResult> OnPost(RecommenderViewModel recommenderViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<RecommenderViewModel, Recommender>());
                var mapper = new Mapper(config);
                var recommenderEntity = mapper.Map<Recommender>(recommenderViewModel);
                await _mediator.Send(new CreateEntityCommand<Recommender>(recommenderEntity));
                return RedirectToPage("Recommenders", new
                {
                    candidateId = recommenderViewModel.CandidateId,
                    previousJobPlacementId = recommenderViewModel.PreviousJobId
                });
            }
            RecommenderViewModel = recommenderViewModel;
            return Page();
        }
    }
}
