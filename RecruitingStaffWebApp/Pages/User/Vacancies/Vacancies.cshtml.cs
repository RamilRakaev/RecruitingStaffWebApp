using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Queries.Requests.Vacancies;
using Domain.Model;
using Domain.Model.CandidateQuestionnaire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecruitingStaffWebApp.Pages.User.Vacancies
{
    public class VacanciesModel : BasePageModel
    {
        public VacanciesModel(IMediator mediator) : base(mediator)
        {
        }

        public Vacancy[] Vacancies { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Vacancies = await _mediator.Send(new GetVacanciesQuery());
            return await RightVerification();
        }
    }
}
