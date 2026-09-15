namespace Shop.Domain.UserAgg
{
    public class UserRole
    {
        public Guid UserId { get; internal set; }
        public Guid RoleId { get; private set; }
    }
}
