using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriTel.Models
{
    // [PrimaryKey(nameof(BillId),nameof(PayDateTime))]
    public class Payment
    {
        [Key]public long Id { get; set; }
        public long BillId { get; set; }    //bill->billId   p
        public required DateTime PayDateTime { get; set; }  // p
        public long UserId { get; set; }    //user->userId   
        public long ServiceId { get; set; }    //service->serviceId   
        public required string PayMethod { get; set; }
        public required float PayAmount { get; set; }


        [ForeignKey("BillId")] public List<Bill> Payment_Bill { get; set; } = null!;
        [ForeignKey("UserId")] public List<User> Payment_user { get; set; } = null!;
        [ForeignKey("ServiceId")] public List<Service> Payment_Service { get; set; } = null!;

    }
}