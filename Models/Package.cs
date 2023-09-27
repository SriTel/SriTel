using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    public class Package
    {
        [Key]public long Id { get; set; }
        public required string Name { get; set; }
        public required string? Renewal { get; set; }
        // public required string Status { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required float Charge { get; set; } = 0;
        public required float OffPeekData { get; set; } = 0;
        public required float PeekData { get; set; } = 0;
        public required float AnytimeData { get; set; } = 0;
        public required int S2SCallMins { get; set; } = 0;
        public required int S2SSmsCount { get; set; } = 0;
        public required int AnyNetCallMins { get; set; } = 0;
        public required int AnyNetSmsCount { get; set; } = 0;
    }
}