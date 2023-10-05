using System.ComponentModel.DataAnnotations;
using TelcoService.Models;

namespace TelcoService.DTO;

public class VoiceServiceDTO
{
    [Required]public long UserId { get; set; } //Service->serviceId
        public int IsRinginngTone { get; set; }
        public string? RingingToneName { get; set; }
        public float RingingToneCharge { get; set; }
        public int IsVoiceRoaming { get; set; }
        public float VoiceRoamingCharge { get; set; }


    public static VoiceServiceDTO FromVoiceService(VoiceService voiceService)
    {
        return new VoiceServiceDTO
        {
            UserId = voiceService.UserId,
            IsRinginngTone = voiceService.IsRingingTone,
            RingingToneName = voiceService.RingingToneName,
            RingingToneCharge = voiceService.RingingToneCharge,
            IsVoiceRoaming = voiceService.IsVoiceRoaming,
            VoiceRoamingCharge = voiceService.VoiceRoamingCharge
        };
    }

    public VoiceService ToVoiceService()
    {
        return new VoiceService
        {
            UserId = UserId,
            IsRingingTone = IsRinginngTone,
            RingingToneName = RingingToneName!,
            RingingToneCharge = RingingToneCharge,
            IsVoiceRoaming = IsVoiceRoaming,
            VoiceRoamingCharge = VoiceRoamingCharge
        };
    }
}