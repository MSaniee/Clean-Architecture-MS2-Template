namespace $ext_safeprojectname$.Domain.UserAggregate.Roles;

public class RoleClaim : IdentityRoleClaim<Guid>, IEntity
{
    public Role Role { get; set; }
}

