namespace $ext_safeprojectname$.Domain.Core.Bases.Entities;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}

