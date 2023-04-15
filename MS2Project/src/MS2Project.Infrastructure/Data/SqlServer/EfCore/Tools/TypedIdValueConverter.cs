using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace $ext_safeprojectname$.Infrastructure.Data.SqlServer.EfCore.Tools;

public class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
        where TTypedIdValue : TypedIdValueBase
{
    public TypedIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
}