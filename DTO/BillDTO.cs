using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class BillDTO
{
    [Required]public long Id { get; set; }
    public long UserId { get; set; }    //user->userId 
    public long ServiceId { get; set; }    //service->serviceId
    public required int Month { get; set; }
    public required int Year { get; set; }
    public required float TaxAmount { get; set; }
    public required float TotalAmount { get; set; }
    public required float DueAmount { get; set; }


    public static BillDTO FromBill(Bill bill)
    {
        return new BillDTO
        {
            Id = bill.Id,
            UserId = bill.UserId,
            ServiceId = bill.ServiceId,
            Month = bill.Month,
            Year = bill.Year,
            TaxAmount = bill.TaxAmount,
            TotalAmount = bill.TotalAmount,
            DueAmount = bill.DueAmount
        };
    }

    public Bill ToBill()
    {
        return new Bill
        {
            Id = Id,
            UserId = UserId,
            ServiceId = ServiceId,
            Month = Month,
            Year = Year,
            TaxAmount = TaxAmount,
            TotalAmount = TotalAmount,
            DueAmount = DueAmount
        };
    }
}