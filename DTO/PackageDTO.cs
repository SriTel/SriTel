using System.ComponentModel.DataAnnotations;
using SriTel.Models;

namespace SriTel.DTO;

public class PackageDTO
{
    [Required]public long Id { get; set; }
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


    public static PackageDTO FromPackage(Package package)
    {
        return new PackageDTO
        {
            Id = package.Id,
            Name = package.Name,
            Renewal = package.Renewal,
            Description = package.Description,
            Image = package.Image,
            Charge = package.Charge,
            OffPeekData = package.OffPeekData,
            PeekData = package.PeekData,
            AnytimeDate = package.AnytimeDate,
            S2SCallMins = package.S2SCallMins,
            S2SSmsCount = package.S2SSmsCount,
            AnyNetCallMins = package.AnyNetCallMins,
            AnyNetSmsCount = package.AnyNetSmsCount
        };
    }

    public Package ToPackage()
    {
        return new Package
        {
            Id = Id,
            Name = Name,
            Renewal = Renewal,
            Description = Description,
            Image = Image,
            Charge = Charge,
            OffPeekData = OffPeekData,
            PeekData = PeekData,
            AnytimeDate = AnytimeDate,
            S2SCallMins = S2SCallMins,
            S2SSmsCount = S2SSmsCount,
            AnyNetCallMins = AnyNetCallMins,
            AnyNetSmsCount = AnyNetSmsCount
        };
    }
}