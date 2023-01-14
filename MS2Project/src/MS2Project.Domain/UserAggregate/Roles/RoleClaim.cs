namespace MS2Project.Domain.UserAggregate.Roles;

public class RoleClaim : IdentityRoleClaim<Guid>, IEntity
{
    public Role Role { get; set; }
}

