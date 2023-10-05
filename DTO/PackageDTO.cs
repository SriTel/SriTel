using System.ComponentModel.DataAnnotations;
using SriTel.Models;

namespace SriTel.DTO;

public class PackageDTO
{
    [Required]public long Id { get; set; }
    public string? Name { get; set; }
    public RenewalType? Renewal { get; set; }
    
    public ServiceType Type { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public float Charge { get; set; }
    public float OffPeekData { get; set; }
    public float PeekData { get; set; }
    public float AnytimeData { get; set; }
    public int S2SCallMins { get; set; }
    public int S2SSmsCount { get; set; }
    public int AnyNetCallMins { get; set; }
    public int AnyNetSmsCount { get; set; }


    public static PackageDTO FromPackage(Package package)
    {
        return new PackageDTO
        {
            Id = package.Id,
            Name = package.Name,
            Renewal = package.Renewal,
            Type = package.Type,
            Description = package.Description,
            Image = package.Image,
            Charge = package.Charge,
            OffPeekData = package.OffPeekData,
            PeekData = package.PeekData,
            AnytimeData = package.AnytimeData,
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
            Name = Name!,
            Renewal = Renewal!,
            Type = Type,
            Description = Description!,
            Image = Image!,
            Charge = Charge,
            OffPeekData = OffPeekData,
            PeekData = PeekData,
            AnytimeData = AnytimeData,
            S2SCallMins = S2SCallMins,
            S2SSmsCount = S2SSmsCount,
            AnyNetCallMins = AnyNetCallMins,
            AnyNetSmsCount = AnyNetSmsCount
        };
    }
}