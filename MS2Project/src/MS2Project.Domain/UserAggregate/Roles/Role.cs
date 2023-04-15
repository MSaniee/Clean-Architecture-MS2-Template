
namespace $ext_safeprojectname$.Domain.UserAggregate.Roles;

public class Role : IdentityRole<Guid>, IEntity
{
    public string Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RoleClaim> Claims { get; set; }
}
