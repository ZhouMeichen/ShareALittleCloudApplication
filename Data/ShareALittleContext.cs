using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareALittle.Models;

namespace ShareALittle.Models
{
    public class ShareALittleContext : DbContext
    {
        public ShareALittleContext (DbContextOptions<ShareALittleContext> options)
            : base(options)
        {
        }

        public DbSet<ShareALittle.Models.Category> Category { get; set; }

        public DbSet<ShareALittle.Models.Product> Product { get; set; }
    }
}
