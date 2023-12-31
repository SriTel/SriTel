using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PaymentService.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }    //user->userId 
        public long ServiceId { get; set; }    //service->serviceId
        public required int Month { get; set; }
        public required int Year { get; set; }
        public required double TaxAmount { get; set; }
        public required double TotalAmount { get; set; }
        public required double DueAmount { get; set; }

        
        [ForeignKey("UserId")] public List<User> Bill_User { get; set; } = null!;
        [ForeignKey("ServiceId")] public List<Service> Bill_Service { get; set; } = null!;

    }
}