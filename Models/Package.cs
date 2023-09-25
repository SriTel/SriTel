using System.ComponentModel.DataAnnotations;
namespace SriTel.Models
{
    public class Package
    {
        [Key]public long Id { get; set; }
        public required string Name { get; set; }
        public required string Renewal { get; set; }
        // public required string Status { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required float Charge { get; set; }
        public required float OffPeekData { get; set; }
        public required float PeekData { get; set; }
        public required float AnytimeDate { get; set; }
        public required int S2SCallMins { get; set; }
        public required int S2SSmsCount { get; set; }
        public required int AnyNetCallMins { get; set; }
        public required int AnyNetSmsCount { get; set; }
    }
}