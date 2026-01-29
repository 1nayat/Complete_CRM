using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Infrastructure.Persistence.Models
{
    public partial class Permission
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Module { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

}
