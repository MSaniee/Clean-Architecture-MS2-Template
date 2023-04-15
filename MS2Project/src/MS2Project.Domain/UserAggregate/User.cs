namespace $ext_safeprojectname$.Domain.UserAggregate;

public class User : IdentityUser<Guid>, IEntity, IAggregateRoot
{
    //public User()
    //{
    //    SecurityStamp = Guid.NewGuid().ToString();
    //}
}
