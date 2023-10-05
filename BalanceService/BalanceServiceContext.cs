using System.ComponentModel.DataAnnotations.Schema;
using BalanceService.Models;
using Microsoft.EntityFrameworkCore;

namespace BalanceService;
public class BalanceServiceContext : DbContext
{
    public BalanceServiceContext(DbContextOptions<BalanceServiceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseSerialColumns();
    }

    public DbSet<AddOn> AddOn { get; set; } = null!;
    public DbSet<AddOnActivation> AddOnActivation { get; set; } = null!;
    // public DbSet<Bill> Bill { get; set; } = null!;
    public DbSet<DataService> DataService { get; set; } = null!;
    // public DbSet<Notification> Notification { get; set; } = null!;
    public DbSet<Package> Package { get; set; } = null!;
    public DbSet<PackageUsage> PackageUsage { get; set; } = null!;
    // public DbSet<Payment> Payment { get; set; } = null!;
    public DbSet<Service> Service { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<VoiceService> VoiceService { get; set; } = null!;
}