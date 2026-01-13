using KiiBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        #region [MODEL]
        public DbSet<PLAYER> PLAYER { get; set; }
        public DbSet<FLEX> FLEX { get; set; }
        public DbSet<FLEX_ITEM> FLEX_ITEM { get; set; }
        #endregion [MODEL]

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
