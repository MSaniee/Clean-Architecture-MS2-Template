namespace MS2Project.Domain.UserAggregate;

public class User : IdentityUser<Guid>, IEntity, IAggregateRoot
{
    //public User()
    //{
    //    SecurityStamp = Guid.NewGuid().ToString();
    //}
}
