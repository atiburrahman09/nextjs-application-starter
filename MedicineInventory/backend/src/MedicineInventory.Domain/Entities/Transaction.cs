using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        
        public int? MedicineId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public int? FromLocationId { get; set; }
        
        public int? ToLocationId { get; set; }
        
        public int? TransactionTypeId { get; set; }
        
        public int? PerformedBy { get; set; }
        
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        
        [MaxLength(255)]
        public string? Remarks { get; set; }
        
        // Navigation properties
        public virtual Medicine? Medicine { get; set; }
        public virtual Location? FromLocation { get; set; }
        public virtual Location? ToLocation { get; set; }
        public virtual TransactionType? TransactionType { get; set; }
        public virtual User? PerformedByUser { get; set; }
    }
}
