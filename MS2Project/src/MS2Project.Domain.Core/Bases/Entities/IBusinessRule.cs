namespace MS2Project.Domain.Core.Bases.Entities;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}

