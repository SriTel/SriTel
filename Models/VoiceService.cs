using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace SriTel.Models
{
    public class VoiceService
    {
        // public long Id { get; set; }   
        [Key]public long ServiceId { get; set; } //Service->serviceId
        public required bool IsRinginngTone { get; set; }
        public required string RingingToneName { get; set; }
        public required float RingingToneCharge { get; set; }
        public required bool IsVoiceRoaming { get; set; }
        public required float VoiceRoamingCharge { get; set; }
    }
}