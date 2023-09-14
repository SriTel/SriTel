using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    [PrimaryKey(nameof(UserId), nameof(ServiceId),nameof(Year),nameof(Month))]
    public class PackageUsage
    {
        // public long Id { get; set; }
        public long UserId { get; set; } // p
        public long ServiceId { get; set; } // p
        public int Year { get; set; } // p
        public int Month { get; set; } // p
        public long PackageId { get; set; } 
        public required DateTime UpdateDateTime { get; set; }  // package change date
        public required float OffPeekDataUsage { get; set; }
        public required float PeekDataUsage { get; set; }
        public required float AnytimeDateUsage { get; set; }
        public required int S2SCallMinsUsage { get; set; }
        public required int S2SSmsCountUsage { get; set; }
        public required int AnyNetCallMinsUsage { get; set; }
        public required int AnyNetSmsCountUsage { get; set; }
        public required int State { get; set; } // 0-deactivate, 1-activate
    }
}