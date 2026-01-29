using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskKhadim.HRMS.Application.Common.Security;
using AskKhadim.HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AskKhadim.HRMS.Infrastructure.Repositories
{
    public class PermissionReadRepository : IPermissionReadRepository
    {
        private readonly AskKhadimDbContext _context;

        public PermissionReadRepository(AskKhadimDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<string>> GetUserPermissionsAsync(long userId)
        {
            return await (
                from ur in _context.user_roles
                join rp in _context.RolePermissions on ur.role_id equals rp.RoleId
                join p in _context.Permissions on rp.PermissionId equals p.Id
                where ur.user_id == userId
                select p.Code
            ).Distinct().ToListAsync();
        }



    }

}
