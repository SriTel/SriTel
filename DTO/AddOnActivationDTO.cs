using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class AddOnActivationDTO
{
    [Required]public long Id { get; set; }
    public long DataServiceId { get; set; } //Dataservice->dataserviceId    
    public long UserId { get; set; } //user->userId    p
    public long AddOnId { get; set; } //addon->addonId    p
    public DateTime ActivatedDateTime { get; set; }   // p
    public float DataUsage { get; set; }


    public static AddOnActivationDTO FromAddOnActivation(AddOnActivation addOnActivation)
    {
        return new AddOnActivationDTO
        {
            Id = addOnActivation.Id,
            DataServiceId = addOnActivation.DataServiceId,
            UserId = addOnActivation.UserId,
            AddOnId = addOnActivation.AddOnId,
            ActivatedDateTime = addOnActivation.ActivatedDateTime,
            DataUsage = addOnActivation.DataUsage
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
            ActivatedDateTime = ActivatedDateTime,
            DataUsage = DataUsage
        };
    }
}