using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [MaxLength(255)]
        public string? Manufacturer { get; set; }
        
        public DateTime? ExpiryDate { get; set; }
        
        public int? CreatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual User? Creator { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
