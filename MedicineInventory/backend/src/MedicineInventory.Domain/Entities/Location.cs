using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        
        [MaxLength(100)]
        public string? LocationName { get; set; }
        
        [MaxLength(255)]
        public string? Description { get; set; }
        
        // Navigation properties
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<Transaction> FromTransactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transaction> ToTransactions { get; set; } = new List<Transaction>();
    }
}
