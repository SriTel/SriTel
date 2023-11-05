using System.ComponentModel.DataAnnotations;
using TelcoService.Models;

namespace TelcoService.DTO;

public class DataServiceDTO
{
    [Required]public long UserId { get; set; } //Service->serviceId
    public int IsDataRoaming { get; set; }
    public float DataRoamingCharge { get; set; }


    public static DataServiceDTO FromDataService(DataService dataService)
    {
        return new DataServiceDTO
        {
            UserId = dataService.UserId,
            IsDataRoaming = dataService.IsDataRoaming,
            DataRoamingCharge = dataService.DataRoamingCharge
        };
    }

    public DataService ToDataService()
    {
        return new DataService
        {
            UserId = UserId,
            IsDataRoaming = IsDataRoaming,
            DataRoamingCharge = DataRoamingCharge
        };
    }
}