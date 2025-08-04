using Microsoft.EntityFrameworkCore;
using MedicineInventory.Domain.Entities;

namespace MedicineInventory.Infrastructure.Data
{
    public class MedicineInventoryDbContext : DbContext
    {
        public MedicineInventoryDbContext(DbContextOptions<MedicineInventoryDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Medicine entity configuration
            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.MedicineId);
                entity.Property(e => e.MedicineId).ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                
                entity.HasOne(e => e.Creator)
                    .WithMany(u => u.CreatedMedicines)
                    .HasForeignKey(e => e.CreatedBy)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Location entity configuration
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationId);
                entity.Property(e => e.LocationId).ValueGeneratedOnAdd();
            });

            // TransactionType entity configuration
            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(e => e.TransactionTypeId);
                entity.Property(e => e.TransactionTypeId).ValueGeneratedOnAdd();
            });

            // Inventory entity configuration
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId);
                entity.Property(e => e.InventoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.LastUpdated).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Medicine)
                    .WithMany(m => m.Inventories)
                    .HasForeignKey(e => e.MedicineId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Location)
                    .WithMany(l => l.Inventories)
                    .HasForeignKey(e => e.LocationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Transaction entity configuration
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.TransactionId).ValueGeneratedOnAdd();
                entity.Property(e => e.TransactionDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Medicine)
                    .WithMany(m => m.Transactions)
                    .HasForeignKey(e => e.MedicineId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.FromLocation)
                    .WithMany(l => l.FromTransactions)
                    .HasForeignKey(e => e.FromLocationId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.ToLocation)
                    .WithMany(l => l.ToTransactions)
                    .HasForeignKey(e => e.ToLocationId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.TransactionType)
                    .WithMany(tt => tt.Transactions)
                    .HasForeignKey(e => e.TransactionTypeId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.PerformedByUser)
                    .WithMany(u => u.PerformedTransactions)
                    .HasForeignKey(e => e.PerformedBy)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed data
            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType { TransactionTypeId = 1, TypeName = "IN" },
                new TransactionType { TransactionTypeId = 2, TypeName = "OUT" },
                new TransactionType { TransactionTypeId = 3, TypeName = "TRANSFER" },
                new TransactionType { TransactionTypeId = 4, TypeName = "DISPOSE" }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, LocationName = "Main Warehouse", Description = "Primary storage location" },
                new Location { LocationId = 2, LocationName = "Pharmacy Counter", Description = "Front desk pharmacy" },
                new Location { LocationId = 3, LocationName = "Cold Storage", Description = "Temperature controlled storage" }
            );
        }
    }
}
