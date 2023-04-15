namespace $ext_safeprojectname$.Domain.UserAggregate.Roles;

public class UserRole : IdentityUserRole<Guid>, IEntity
{
    public User User { get; set; }
    public Role Role { get; set; }
}

