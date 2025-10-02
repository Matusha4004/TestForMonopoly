namespace Core.ResultType;

public abstract record ResultAddTransaction
{
    private ResultAddTransaction() { }

    public sealed record Success() : ResultAddTransaction;

    public sealed record NotEnoughMoney() : ResultAddTransaction;
}