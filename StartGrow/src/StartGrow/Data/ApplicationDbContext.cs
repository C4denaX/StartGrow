﻿using System;
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
       
        public DbSet<Monedero> Monedero { get; set; }
        public DbSet<Inversion> Inversion { get; set; }
        public DbSet<InversionProyecto> InversionProyecto { get; set; }
        public DbSet<Inversor> Inversor { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }


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
