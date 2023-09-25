using System.ComponentModel.DataAnnotations;
using SriTel.Models;

namespace SriTel.DTO;

public class DataServiceDTO
{
    [Required]public long ServiceId { get; set; } //Service->serviceId
    public required int IsDataRoaming { get; set; }
    public required float DataRoamingCharge { get; set; }


    public static DataServiceDTO FromDataService(DataService dataService)
    {
        return new DataServiceDTO
        {
            ServiceId = dataService.ServiceId,
            IsDataRoaming = dataService.IsDataRoaming,
            DataRoamingCharge = dataService.DataRoamingCharge
        };
    }

    public DataService ToDataService()
    {
        return new DataService
        {
            ServiceId = ServiceId,
            IsDataRoaming = IsDataRoaming,
            DataRoamingCharge = DataRoamingCharge
        };
    }
}