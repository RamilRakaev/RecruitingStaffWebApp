using MediatR;

namespace RecruitingStaff.Infrastructure.CQRS.Commands.Requests.UniversalCommand
{
    public class CreateOrChangeByViewModelCommand : IRequest<bool>
    {
        public CreateOrChangeByViewModelCommand(object viewModel)
        {
            ViewModel = viewModel;
        }

        public object ViewModel { get; set; }
    }
}
