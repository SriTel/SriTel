using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriTel.Models
{
    public class VoiceService
    {
        // public long Id { get; set; }   
        [Key]public long UserId { get; set; } //Service->serviceId
        public required int IsRingingTone { get; set; }
        public required string RingingToneName { get; set; }
        public required float RingingToneCharge { get; set; }
        public required int IsVoiceRoaming { get; set; }
        public required float VoiceRoamingCharge { get; set; }


        [ForeignKey("UserId")] public List<User> VoiceServiceUser { get; set; } = null!;

    }
}