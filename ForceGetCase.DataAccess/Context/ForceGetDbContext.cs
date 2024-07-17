using ForceGetCase.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.DataAccess.Context
{
    public class ForceGetDbContext : DbContext
    {
        public ForceGetDbContext(DbContextOptions<ForceGetDbContext> options) : base(options)
        {
        }

        public DbSet<Modes> Modes { get; set; }
        public DbSet<MovementTypes> MovementTypes { get; set; }
        public DbSet<Incoterms> Incoterms { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<PackageTypes> PackageTypes { get; set; }
        public DbSet<Unit1> Unit1 { get; set; }
        public DbSet<Unit2> Unit2 { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
