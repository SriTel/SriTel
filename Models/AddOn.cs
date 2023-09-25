using System.ComponentModel.DataAnnotations;
namespace SriTel.Models
{
    public class AddOn
    {
        [Key]public long Id { get; set; } 
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required string Description { get; set; }
        public required int ValidNoOfDays { get; set; }
        public required float ChargePerGb { get; set; }
        public required float DataAmount { get; set; }
    }
}