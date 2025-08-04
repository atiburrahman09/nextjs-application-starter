using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }
        
        [MaxLength(50)]
        public string? TypeName { get; set; }
        
        // Navigation properties
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
