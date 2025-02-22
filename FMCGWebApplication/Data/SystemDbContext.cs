
using FMCGWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FMCGWebApplication.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }


        public DbSet<Company> TISCompanyMsts { get; set; }

        public DbSet<Area> TISAreaMsts { get; set; }

        public DbSet<Groups> TISGroupMsts { get; set; }

        public DbSet<States> TISStates { get; set; }
        public DbSet<Accounts> TISAccountMsts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               modelBuilder.Entity<Company>()
                .HasKey(a => new { a.CompanyCode });
          

            //modelBuilder.Entity<Area>()
            //   .HasKey(a => new { a.AreaCode });

            modelBuilder.Entity<Area>()
        .HasKey(a => new { a.CompanyCode, a.AreaCode }); // ✅ Composite Primary Key
                                                     //modelBuilder.Entity<Area>()
                                                     //     //.HasKey(a => new { a.CompanyCode, a.AreaCode });
                                                     //     .HasOne(a => a.Companys)
                                                     //.WithMany()
                                                     //.HasForeignKey(a => a.CompanyCode);

            modelBuilder.Entity<Groups>()
            .HasKey(g => new { g.CompanyCode, g.GroupCode });

            modelBuilder.Entity<States>()
                .HasKey(a => new { a.StateName });

            modelBuilder.Entity<Accounts>()
            .HasKey(a => new { a.CompanyCode, a.GroupCode, a.AcCode  });
            


            base.OnModelCreating(modelBuilder);
        }

        

       
    }
}
