namespace RecruitingStaff.Infrastructure.CQRS.Commands
{
    public enum CommandResult
    {
        ExistingOneBeenChanged,
        NewOneHasBeenCreated,
        ExistingOneHasBeenRemoved,
        DataEnteredIncorrectly,
        Error
    }
}
