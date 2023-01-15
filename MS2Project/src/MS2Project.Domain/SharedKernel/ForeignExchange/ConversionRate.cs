﻿namespace MS2Project.Domain.SharedKernel.ForeignExchange;

public class ConversionRate
{
    public string SourceCurrency { get; }

    public string TargetCurrency { get; }

    public decimal Factor { get; }

    public ConversionRate(string sourceCurrency, string targetCurrency, decimal factor)
    {
        SourceCurrency = sourceCurrency;
        TargetCurrency = targetCurrency;
        Factor = factor;
    }

    internal MoneyValue Convert(MoneyValue value) => Factor * value;

}
