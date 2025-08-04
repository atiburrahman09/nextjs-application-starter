using MedicineInventory.Domain.Entities;

namespace MedicineInventory.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Medicine> Medicines { get; }
        IRepository<Location> Locations { get; }
        IRepository<Inventory> Inventories { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<TransactionType> TransactionTypes { get; }
        
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
