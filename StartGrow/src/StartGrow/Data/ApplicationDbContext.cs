using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StartGrow.Models;

namespace StartGrow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Inversor> Inversor { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Particular> Particular { get; set; }
        public DbSet<Preferencias> Preferencias { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<TiposInversiones> TiposInversiones { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
