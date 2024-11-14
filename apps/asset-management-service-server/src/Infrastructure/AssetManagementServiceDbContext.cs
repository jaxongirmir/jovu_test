using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.Infrastructure;

public class AssetManagementServiceDbContext : DbContext
{
    public AssetManagementServiceDbContext(
        DbContextOptions<AssetManagementServiceDbContext> options
    )
        : base(options) { }

    public DbSet<PersonDbModel> People { get; set; }

    public DbSet<CarDbModel> Cars { get; set; }

    public DbSet<ChairDbModel> Chairs { get; set; }

    public DbSet<LaptopDbModel> Laptops { get; set; }

    public DbSet<PhoneDbModel> Phones { get; set; }
}
