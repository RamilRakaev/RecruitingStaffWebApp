namespace RecruitingStaff.Infrastructure.CQRS.Commands.Handlers
{
    public enum ResultCommand
    {
        ExistingOneBeenChanged,
        NewOneHasBeenCreated,
        ExistingOneHasBeenRemoved,
        DataEnteredIncorrectly,
        Error
    }
}
