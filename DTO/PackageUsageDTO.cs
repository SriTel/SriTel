using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class PackageUsageDTO
{
    [Required]public long Id { get; set; }
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


    public static PackageUsageDTO FromPackageUsage(PackageUsage packageUsage)
    {
        return new PackageUsageDTO
        {
            Id = packageUsage.Id,
            UserId = packageUsage.UserId,
            ServiceId = packageUsage.ServiceId,
            Year = packageUsage.Year,
            Month = packageUsage.Month,
            PackageId = packageUsage.PackageId,
            UpdateDateTime = packageUsage.UpdateDateTime,
            OffPeekDataUsage = packageUsage.OffPeekDataUsage,
            PeekDataUsage = packageUsage.PeekDataUsage,
            AnytimeDateUsage = packageUsage.AnytimeDateUsage,
            S2SCallMinsUsage = packageUsage.S2SCallMinsUsage,
            S2SSmsCountUsage = packageUsage.S2SSmsCountUsage,
            AnyNetCallMinsUsage = packageUsage.AnyNetCallMinsUsage,
            AnyNetSmsCountUsage = packageUsage.AnyNetSmsCountUsage,
            State = packageUsage.State
        };
    }

    public PackageUsage ToPackageUsage()
    {
        return new PackageUsage
        {
            Id = Id,
            UserId = UserId,
            ServiceId = ServiceId,
            Year = Year,
            Month = Month,
            PackageId = PackageId,
            UpdateDateTime = UpdateDateTime,
            OffPeekDataUsage = OffPeekDataUsage,
            PeekDataUsage = PeekDataUsage,
            AnytimeDateUsage = AnytimeDateUsage,
            S2SCallMinsUsage = S2SCallMinsUsage,
            S2SSmsCountUsage = S2SSmsCountUsage,
            AnyNetCallMinsUsage = AnyNetCallMinsUsage,
            AnyNetSmsCountUsage = AnyNetSmsCountUsage,
            State = State
        };
    }
}