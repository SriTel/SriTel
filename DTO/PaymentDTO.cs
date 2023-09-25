using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class PaymentDTO
{
    [Required]public long Id { get; set; }
        public long BillId { get; set; }    //bill->billId   p
        public DateTime PayDateTime { get; set; }  // p
        public long UserId { get; set; }    //user->userId   
        public long ServiceId { get; set; }    //service->serviceId   
        public string? PayMethod { get; set; }
        public float PayAmount { get; set; }


    public static PaymentDTO FromPayment(Payment payment)
    {
        return new PaymentDTO
        {
            Id = payment.Id,
            BillId = payment.BillId,
            PayDateTime = payment.PayDateTime,
            UserId = payment.UserId,
            ServiceId = payment.ServiceId,
            PayMethod = payment.PayMethod,
            PayAmount = payment.PayAmount
        };
    }

    public Payment ToPayment()
    {
        return new Payment
        {
            Id = Id,
            BillId = BillId,
            PayDateTime = PayDateTime,
            UserId = UserId,
            ServiceId = ServiceId,
            PayMethod = PayMethod,
            PayAmount = PayAmount
        };
    }
}