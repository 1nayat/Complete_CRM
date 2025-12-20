using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Domain.Common
{
    public abstract class OrgScopedEntity
    {
        [Column("organization_id")]
        public Guid OrganizationId { get; set; }
    }
}
