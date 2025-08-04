using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Domain.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        
        public int? MedicineId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public int? LocationId { get; set; }
        
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual Medicine? Medicine { get; set; }
        public virtual Location? Location { get; set; }
    }
}
