using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RecruitingStaff.Domain.Model;
using RecruitingStaff.WebApp.ViewModels;

namespace RecruitingStaffWebApp.Pages.User
{
    [Authorize(Policy = "RequireUserRole")]
    public class BasePageModel : PageModel
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<BasePageModel> _logger;

        public BasePageModel(IMediator mediator, ILogger<BasePageModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public static ViewModel GetViewModel<Entity, ViewModel>(Entity entity)
            where Entity : BaseEntity
            where ViewModel : BaseViewModel
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entity, ViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<ViewModel>(entity);
        }

        public static ViewModel[] GetViewModels<Entity, ViewModel>(Entity[] entity)
            where Entity : BaseEntity
            where ViewModel : BaseViewModel
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entity, ViewModel>());
            var mapper = new Mapper(config);
            return mapper.Map<ViewModel[]>(entity);
        }

        public static Entity GetEntity<Entity, ViewModel>(ViewModel viewModel)
            where Entity : BaseEntity
            where ViewModel : BaseViewModel
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ViewModel, Entity>());
            var mapper = new Mapper(config);
            return mapper.Map<Entity>(viewModel);
        }

    }
}
