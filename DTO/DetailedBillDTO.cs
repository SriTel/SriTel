namespace SriTel.DTO;

public class DetailedBillDto
{
    public long BillId { get; set; }
    
    public long ServiceId { get; set; }
    public double DataServiceCharge { get; set; }
    
    public double DataRoamingCharge { get; set; }
    public double VoiceRoamingCharge { get; set; }
    public double VoiceServiceCharge { get; set; }
    public double RingingToneCharge { get; set; }
    public double packageCharge { get; set; }
    public string packageName { get; set; } = string.Empty;
    public double DataAddOnCharge { get; set; }
    public double ExtraGbCharge { get; set; }
    public double DueAmount { get; set; }
    public double TaxAmount { get; set; }
    public double PaidAmount { get; set; }
    public double TotalAmount { get; set; }
    
    
    
}