namespace RecruitingStaff.Infrastructure.CQRS.Commands
{
    public enum CommandResult
    {
        ExistingEntityBeenChanged,
        NewEntityHasBeenCreated,
        ExistingEntityHasBeenRemoved,
        DataEnteredIncorrectly,
        Error
    }
}
