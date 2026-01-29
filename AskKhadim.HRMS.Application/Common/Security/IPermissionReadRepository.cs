using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskKhadim.HRMS.Application.Common.Security
{
    public interface IPermissionReadRepository
    {
        Task<IReadOnlyList<string>> GetUserPermissionsAsync(long userId);
    }

}
