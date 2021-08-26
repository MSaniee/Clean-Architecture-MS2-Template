using MS2Project.Domain.Core.DILifeTimesType;

namespace MS2Project.Application.Interfaces.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
