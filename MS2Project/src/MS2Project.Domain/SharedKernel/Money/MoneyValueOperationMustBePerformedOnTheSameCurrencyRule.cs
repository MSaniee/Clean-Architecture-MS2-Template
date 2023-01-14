namespace MS2Project.Domain.SharedKernel.Money;

public class MoneyValueOperationMustBePerformedOnTheSameCurrencyRule : IBusinessRule
{
    private readonly MoneyValue _left;

    private readonly MoneyValue _right;

    public MoneyValueOperationMustBePerformedOnTheSameCurrencyRule(MoneyValue left, MoneyValue right)
    {
        _left = left;
        _right = right;
    }

    public bool IsBroken() => _left.Currency != _right.Currency;

    public string Message => Memos.MoneyCurrenciesMustBeSame;
}
