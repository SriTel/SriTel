using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class AddOnActivationDTO
{
    [Required]public long Id { get; set; }
    public long DataServiceId { get; set; } //Dataservice->dataserviceId    
    public long UserId { get; set; } //user->userId    p
    public long AddOnId { get; set; } //addon->addonId    p
    
    public required AddOnType Type { get; set; }
    public DateTime ActivatedDateTime { get; set; }   // p
    
    public DateTime ExpireDateTime { get; set; }
    public double DataUsage { get; set; }
    
    public double TotalData { get; set; }


    public static AddOnActivationDTO FromAddOnActivation(AddOnActivation addOnActivation)
    {
        return new AddOnActivationDTO
        {
            Id = addOnActivation.Id,
            DataServiceId = addOnActivation.DataServiceId,
            UserId = addOnActivation.UserId,
            AddOnId = addOnActivation.AddOnId,
            Type = addOnActivation.Type,
            ActivatedDateTime = addOnActivation.ActivatedDateTime,
            ExpireDateTime = addOnActivation.ExpireDateTime,
            DataUsage = addOnActivation.DataUsage,
            TotalData = addOnActivation.TotalData
        };
    }

    public AddOnActivation ToAddOnActivation()
    {
        return new AddOnActivation
        {
            Id = Id,
            DataServiceId = DataServiceId,
            UserId = UserId,
            AddOnId = AddOnId,
            Type = Type,
            ActivatedDateTime = ActivatedDateTime,
            ExpireDateTime = ExpireDateTime,
            DataUsage = DataUsage,
            TotalData = TotalData
        };
    }
}