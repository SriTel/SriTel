using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    public class AddOn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public required string Name { get; set; }
        
        public required AddOnType Type { get; set; }
        public required string Image { get; set; }
        public required string Description { get; set; }
        public required int ValidNoOfDays { get; set; }
        public required float ChargePerGb { get; set; }
        public required float DataAmount { get; set; }
    }
    
    public enum AddOnType
    {
        ExtraGb,
        Other
    }
}