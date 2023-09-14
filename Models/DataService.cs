using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    public class DataService
    {
        // public long Id { get; set; }
        [Key]public long ServiceId { get; set; } //Service->serviceId
        public required int IsDataRoaming { get; set; }
        public required float DataRoamingCharge { get; set; }
    }
}