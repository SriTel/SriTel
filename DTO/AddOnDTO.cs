using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class AddOnDTO
{
    [Required]public long Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ValidNoOfDays { get; set; } = 0;
    public float ChargePerGb { get; set; } = 0;
    public float DataAmount { get; set; } = 0;


    public static AddOnDTO FromAddOn(AddOn addOn)
    {
        return new AddOnDTO
        {
            Id = addOn.Id,
            Name = addOn.Name,
            Image = addOn.Image,
            Description = addOn.Description,
            ValidNoOfDays = addOn.ValidNoOfDays,
            ChargePerGb = addOn.ChargePerGb,
            DataAmount =addOn.DataAmount
        };
    }

    public AddOn ToAddOn()
    {
        return new AddOn
        {
            Id = Id,
            Name = Name,
            Image = Image,
            Description = Description,
            ValidNoOfDays = ValidNoOfDays,
            ChargePerGb = ChargePerGb,
            DataAmount =DataAmount
        };
    }
}