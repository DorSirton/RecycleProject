using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Contexts
{
    public class AuthenticationContext : IdentityDbContext<IdentityUser>
    {            
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }
    }
}
