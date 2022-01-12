using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.Domain.Model.CandidatesSelection;
using RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand;
using RecruitingStaff.Infrastructure.CQRS.Queries.Requests.UniversalQueries;
using RecruitingStaff.WebApp.ViewModels.Files;
using RecruitingStaffWebApp.Pages.User;

namespace RecruitingStaff.WebApp.Pages.User.Files
{
    public class ChangeFileModel : BasePageModel
    {
        public ChangeFileModel(IMediator mediator, ILogger<BasePageModel> logger) : base(mediator, logger)
        {
        }

        public FileViewModel FileViewModel { get; set; }

        public async Task OnGet(int fileId)
        {
            var fileEntity = await _mediator.Send(new GetEntityByIdQuery<RecruitingStaffWebAppFile>(fileId));
            var config = new MapperConfiguration(c => c.CreateMap<RecruitingStaffWebAppFile, FileViewModel>());
            var mapper = new Mapper(config);
            FileViewModel = mapper.Map<FileViewModel>(fileEntity);
            FileViewModel.FileTypeSelectList = new(
                await _mediator.Send(
                    new GetValuesQuery(typeof(FileType))),
                "Key",
                "Value");
        }

        public async Task<IActionResult> OnPost(FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(c => c.CreateMap<FileViewModel, RecruitingStaffWebAppFile>());
                var mapper = new Mapper(config);
                var fileEntity = mapper.Map<RecruitingStaffWebAppFile>(fileViewModel);
                await _mediator.Send(new ChangeEntityCommand<RecruitingStaffWebAppFile>(fileEntity));
                return RedirectToPage("Files");
            }
            ModelState.AddModelError("", "Неверно введены данные");
            FileViewModel = fileViewModel;
            FileViewModel.FileTypeSelectList = new(
                 await _mediator.Send(
                     new GetValuesQuery(typeof(FileType))),
                 "Key",
                 "Value");
            return Page();
        }
    }
}
