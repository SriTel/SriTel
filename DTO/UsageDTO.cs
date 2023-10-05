using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class Usage
{
    public string Title { get; set; }  = string.Empty;
    
    public UnitOfMeasure Unit { get; set; }
    public double TotalAmount { get; set; }  = 0;
    public double UsageAmount { get; set; }  = 0;
}

public enum UnitOfMeasure
{
    Gb,
    Minutes,
    Sms,
}