using RollsApi.Models;
using System.Diagnostics.Metrics;

namespace RollsApi.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
      #region Country
      modelbuilder.Entity<Country>()
          .HasIndex(c => c.country_name)
          .IsUnique();
      modelbuilder.Entity<Country>()
          .HasIndex(c => c.country_code)
          .IsUnique();
      modelbuilder.Entity<Country>()
          .HasData(new Country()
          {
            country_id = 1,
            country_name = "INDIA",
            country_code = "IND",
            record_status = "ACTIVE"
          });
      #endregion

      #region State
      modelbuilder.Entity<State>()
          .HasIndex(c => c.state_name)
          .IsUnique();
      modelbuilder.Entity<State>()
          .HasIndex(c => c.state_code)
          .IsUnique();
      modelbuilder.Entity<State>()
          .HasOne(s => s.country)
          .WithMany(c => c.states)
          .HasForeignKey(s => s.country_id);
      modelbuilder.Entity<State>()
          .HasData(new State()
          {
            state_id = 1,
            state_name = "KERALA",
            state_code = "KRL",
            country_id = 1,
            record_status = "ACTIVE"
          });
      #endregion

      #region District
      modelbuilder.Entity<District>()
          .HasIndex(c => c.district_name)
          .IsUnique();
      modelbuilder.Entity<District>()
          .HasIndex(c => c.district_code)
          .IsUnique();
      modelbuilder.Entity<District>()
          .HasOne(d => d.state)
          .WithMany(s => s.districts)
          .HasForeignKey(d => d.state_id);
      #endregion

      #region Bank
      modelbuilder.Entity<Bank>()
        .HasIndex(bn => bn.bank_name)
        .IsUnique();

      modelbuilder.Entity<Bank>()
          .HasData(new Bank()
          {
            bank_id = 1,
            bank_name = "STATE BANK OF INDIA",
            record_status = "ACTIVE"
          },
          new Bank()
          {
            bank_id = 2,
            bank_name = "ICICI",
            record_status = "ACTIVE"
          },
          new Bank()
          {
            bank_id = 3,
            bank_name = "HDFC BANK",
            record_status = "ACTIVE"
          },
          new Bank()
          {
            bank_id = 4,
            bank_name = "AXIS BANK",
            record_status = "ACTIVE"
          },
          new Bank()
          {
            bank_id = 5,
            bank_name = "UNION BANK OF INDIA",
            record_status = "ACTIVE"
          });
      #endregion
    }

    public DbSet<Country> countries { get; set; }
    public DbSet<State> states { get; set; }
    public DbSet<District> districts { get; set; }
    public DbSet<Bank> banks { get; set; }

  }
}
