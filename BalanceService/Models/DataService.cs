using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BalanceService.Models
{
    public class DataService
    {
        // public long Id { get; set; }
        [Key]public long UserId { get; set; } //Service->serviceId
        public required int IsDataRoaming { get; set; }
        public required float DataRoamingCharge { get; set; }


        [ForeignKey("UserId")] public List<User> DataServiceUser { get; set; } = null!;
        
    }
}