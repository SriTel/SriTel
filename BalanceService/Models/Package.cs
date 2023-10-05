using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BalanceService.Models
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public required string Name { get; set; }
        public required RenewalType? Renewal { get; set; }
        
        public required ServiceType Type { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required float Charge { get; set; }
        public required float OffPeekData { get; set; }
        public required float PeekData { get; set; }
        public required float AnytimeData { get; set; }
        public required int S2SCallMins { get; set; }
        public required int S2SSmsCount { get; set; }
        public required int AnyNetCallMins { get; set; }
        public required int AnyNetSmsCount { get; set; }
    }
    
    public enum ServiceType
    {
        Data,
        Voice,
    }

    public enum RenewalType
    {
        Daily,
        Weekly,
        Monthly,
    }
}
