using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    public class Bill
    {
        [Key]public long Id { get; set; }
        public long UserId { get; set; }    //user->userId 
        public long ServiceId { get; set; }    //service->serviceId
        public required int Month { get; set; }
        public required int Year { get; set; }
        public required float TaxAmount { get; set; }
        public required float TotalAmount { get; set; }
        public required float DueAmount { get; set; }
    }
}