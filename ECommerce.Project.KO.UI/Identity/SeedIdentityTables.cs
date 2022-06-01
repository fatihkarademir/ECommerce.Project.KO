using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Identity
{
    public class SeedIdentityTables
    {
        //AppIdentityDbContext context;

        //public SeedIdentityTables(AppIdentityDbContext _context)
        //{
        //    context = _context;
        //}

        readonly RoleManager<IdentityRole> _roleManager;
        public SeedIdentityTables(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                _roleManager.CreateAsync(new IdentityRole { Name = "SysAdmin"});
                _roleManager.CreateAsync(new IdentityRole { Name = "Admin"});
                _roleManager.CreateAsync(new IdentityRole { Name = "Customer"});
            }
            
        }
    }
}
