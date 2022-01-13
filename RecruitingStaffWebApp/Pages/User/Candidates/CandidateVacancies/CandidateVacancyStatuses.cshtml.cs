using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Domain.Model.CandidatesSelection.Maps;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries.Maps;
using RecruitingStaff.WebApp.ViewModels.CandidateData;
using RecruitingStaffWebApp.Pages.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitingStaff.WebApp.Pages.User.Candidates.Statuses
{
    public class CandidateVacancyStatusesModel : BasePageModel
    {
        public CandidateVacancyStatusesModel(IMediator mediator, ILogger<BasePageModel> logger)
            : base(mediator, logger)
        {
        }

        public List<CandidateVacancyViewModel> CandidateVacancyViewModels { get; set; }
        public int CandidateId { get; set; }

        public async Task OnGet(int candidateId)
        {
            CandidateVacancyViewModels = await Initial(candidateId);
        }

        public async Task<IActionResult> OnGetChangeStatus(CandidateVacancyViewModel candidateVacancyViewModel, int status)
        {
            var config = new MapperConfiguration(c => c.CreateMap<CandidateVacancyViewModel, CandidateVacancy> ());
            var mapper = new Mapper(config);
            var candidateVacancyEntity = mapper.Map<CandidateVacancy>(candidateVacancyViewModel);
            candidateVacancyEntity.CandidateStatus = (CandidateStatus)status;
            await _mediator.Send(new ChangeEntityCommand<CandidateVacancy>(candidateVacancyEntity));
            return RedirectToPage("CandidateStatuses", new { candidateId = candidateVacancyViewModel.FirstEntityId });
        }

        public async Task<IActionResult> OnPost(int candidateId, int candidateVacancyId)
        {
            await _mediator.Send(new RemoveEntityCommand<CandidateVacancy>(candidateVacancyId));
            return RedirectToPage("CandidateVacancyStatuses", new { candidateId });
        }

        private async Task<List<CandidateVacancyViewModel>> Initial(int candidateId)
        {
            List<CandidateVacancyViewModel> candidateVacancyViewModels = new();
            CandidateId = candidateId;
            var candidateVacancies = await _mediator.Send(
                new GetMapsByFirstEntityIdQuery<CandidateVacancy>(CandidateId));
            foreach (var candidateVacancy in candidateVacancies)
            {
                var config = new MapperConfiguration(c => c.CreateMap<CandidateVacancy, CandidateVacancyViewModel>());
                var mapper = new Mapper(config);
                var candidateVacancyViewModel = mapper.Map<CandidateVacancyViewModel>(candidateVacancy);
                var vacancyEntity = await _mediator.Send(new GetEntityByIdQuery<Vacancy>(candidateVacancyViewModel.SecondEntityId));
                candidateVacancyViewModel.VacancyName = vacancyEntity.Name;
                candidateVacancyViewModels.Add(candidateVacancyViewModel);

                var statuses = await _mediator.Send(new GetValuesQuery(typeof(CandidateStatus)));
                candidateVacancyViewModel.CandidateStatusSelectList = new(statuses, "Key", "Value");
                var key = (int)candidateVacancy.CandidateStatus;
                candidateVacancyViewModel.CandidateStatusSelectList.ElementAt(key).Selected = true;
            }
            return candidateVacancyViewModels;
        }
    }
}
