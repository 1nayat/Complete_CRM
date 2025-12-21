using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Domain.Entities
{
    public class Employee : OrgScopedEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
