using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    [PrimaryKey(nameof(BillId),nameof(PayDateTime))]
    public class Payment
    {
        // public long Id { get; set; }
        public long BillId { get; set; }    //bill->billId   p
        public required DateTime PayDateTime { get; set; }  // p
        public long UserId { get; set; }    //user->userId   
        public long ServiceId { get; set; }    //service->serviceId   
        public required string PayMethod { get; set; }
        public required float PayAmount { get; set; }
    }
}