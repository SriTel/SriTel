using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    [PrimaryKey(nameof(UserId),nameof(AddOnId),nameof(ActivatedDateTime))]
    public class AddOnActivation
    {
        // public long Id { get; set; }
        public long DataServiceId { get; set; } //Dataservice->dataserviceId    
        public long UserId { get; set; } //user->userId    p
        public long AddOnId { get; set; } //addon->addonId    p
        public required DateTime ActivatedDateTime { get; set; }   // p
        public required float DataUsage { get; set; }
    }
}