using Common.Domain;

namespace Shop.Domain.RoleAgg
{
    public partial class Role
    {
        public class RolePermission : BaseEntity
        {
            public Guid RoleId { get; private set; }
            public Permission Permission { get; private set; }
        }
    }
}
