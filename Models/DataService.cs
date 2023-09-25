using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriTel.Models
{
    public class DataService
    {
        // public long Id { get; set; }
        [Key]public long ServiceId { get; set; } //Service->serviceId
        public required int IsDataRoaming { get; set; }
        public required float DataRoamingCharge { get; set; }


        [ForeignKey("ServiceId")] public List<Service> DataService_Service { get; set; } = null!;
        
    }
}