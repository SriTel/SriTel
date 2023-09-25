using System.ComponentModel.DataAnnotations;
using SriTel.Models;

namespace SriTel.DTO;

public class ServiceDTO
{
    [Required]public long Id { get; set; }
        // public long PackageId { get; set; } //package->packageId
        public required string Name { get; set; }
        public required float Charge { get; set; }
        public required string State { get; set; }
        public required string Type { get; set; }


    public static ServiceDTO FromService(Service service)
    {
        return new ServiceDTO
        {
            Id = service.Id,
            Name = service.Name,
            Charge = service.Charge,
            State = service.State,
            Type = service.Type
        };
    }

    public Service ToService()
    {
        return new Service
        {
            Id = Id,
            Name = Name,
            Charge = Charge,
            State = State,
            Type = Type
        };
    }
}