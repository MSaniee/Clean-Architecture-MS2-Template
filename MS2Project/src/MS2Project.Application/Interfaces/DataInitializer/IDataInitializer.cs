using $ext_safeprojectname$.Domain.Core.DILifeTimesType;

namespace $ext_safeprojectname$.Application.Interfaces.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
