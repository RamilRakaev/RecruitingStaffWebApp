using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model.CandidatesSelection.CandidateData;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.WebApp.ViewModels;
using System.Threading.Tasks;

namespace RecruitingStaffWebApp.Pages.User.Questionnaires
{
    public class OptionFormModel : BasePageModel
    {
        public OptionFormModel(IMediator mediator, ILogger<OptionFormModel> logger) : base(mediator, logger)
        {
            OptionViewModel = new OptionViewModel();
        }

        public OptionViewModel OptionViewModel { get; set; }
        public int QuestionId { get; set; }

        public void OnGet(int candidateId, int questionId, string propertyName = "", string value = "")
        {
            OptionViewModel = new();
            OptionViewModel.CandidateId = candidateId;
            QuestionId = questionId;
            OptionViewModel.Name = propertyName;
            OptionViewModel.Value = value;
        }

        public async Task<IActionResult> OnPost(OptionViewModel optionViewModel, int questionId)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<OptionViewModel, Option>());
                var mapper = new Mapper(config);
                var optionEntity = mapper.Map<Option>(optionViewModel);
                await _mediator.Send(new CreateOrChangeEntityByKeysCommand<Option>(optionEntity));
                return RedirectToPage("ConcreteCandidate", new
                {
                    candidateId = optionViewModel.CandidateId.Value,
                    questionId
                });
            }
            OptionViewModel = optionViewModel;
            return Page();
        }
    }
}
