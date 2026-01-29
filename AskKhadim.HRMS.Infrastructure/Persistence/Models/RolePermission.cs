using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskKhadim.HRMS.Infrastructure.Models;

namespace AskKhadim.HRMS.Infrastructure.Persistence.Models
{
    public partial class RolePermission
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        public virtual role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }

}
