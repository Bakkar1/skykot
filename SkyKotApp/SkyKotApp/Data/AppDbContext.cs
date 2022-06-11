using KotClassLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Data
{
    public class AppDbContext : IdentityDbContext<CustomUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
           base(options)
        {

        }

    }
}
