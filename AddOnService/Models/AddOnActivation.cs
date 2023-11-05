using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AddOnService.Models
{
    // [PrimaryKey(nameof(UserId),nameof(AddOnId),nameof(ActivatedDateTime))]
    public class AddOnActivation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public required long DataServiceId { get; set; } //Dataservice->dataserviceId    
        public required long UserId { get; set; } //user->userId    p
        public required long AddOnId { get; set; } //addon->addonId    p
        
        public required AddOnType Type { get; set; }
        
        public required DateTime ActivatedDateTime { get; set; }
        public required DateTime ExpireDateTime { get; set; }   // p
        public required double DataUsage { get; set; }
        
        public required double TotalData { get; set; }

        [ForeignKey("UserId")] public List<User> AddOnActivation_User { get; set; } = null!;
        [ForeignKey("AddOnId")] public List<AddOn> AddOnActivation_AddOn { get; set; } = null!;
        [ForeignKey("DataServiceId")] public List<Service> AddOnActivation_Service { get; set; } = null!;
    }
}