using Microsoft.EntityFrameworkCore.Storage;
using MedicineInventory.Domain.Entities;
using MedicineInventory.Domain.Interfaces;
using MedicineInventory.Infrastructure.Data;

namespace MedicineInventory.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MedicineInventoryDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(MedicineInventoryDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Medicines = new Repository<Medicine>(_context);
            Locations = new Repository<Location>(_context);
            Inventories = new Repository<Inventory>(_context);
            Transactions = new Repository<Transaction>(_context);
            TransactionTypes = new Repository<TransactionType>(_context);
        }

        public IRepository<User> Users { get; private set; }
        public IRepository<Medicine> Medicines { get; private set; }
        public IRepository<Location> Locations { get; private set; }
        public IRepository<Inventory> Inventories { get; private set; }
        public IRepository<Transaction> Transactions { get; private set; }
        public IRepository<TransactionType> TransactionTypes { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
