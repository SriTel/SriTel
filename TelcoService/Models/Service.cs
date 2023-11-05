using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TelcoService.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public required string? Name { get; set; }
        public required float Charge { get; set; }
        public required string? State { get; set; }
        public required ServiceType Type { get; set; }
    }
    
    public enum ServiceType
    {
        Data,
        Voice,
    }
    
}