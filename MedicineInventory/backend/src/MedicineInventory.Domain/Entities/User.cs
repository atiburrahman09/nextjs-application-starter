using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Role { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual ICollection<Medicine> CreatedMedicines { get; set; } = new List<Medicine>();
        public virtual ICollection<Transaction> PerformedTransactions { get; set; } = new List<Transaction>();
    }
}
