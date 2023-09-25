using Microsoft.Build.Framework;
using SriTel.Models;

namespace SriTel.DTO;

public class VoiceServiceDTO
{
    [Required]public long ServiceId { get; set; } //Service->serviceId
        public bool IsRinginngTone { get; set; }
        public string? RingingToneName { get; set; }
        public float RingingToneCharge { get; set; }
        public bool IsVoiceRoaming { get; set; }
        public float VoiceRoamingCharge { get; set; }


    public static VoiceServiceDTO FromVoiceService(VoiceService voiceService)
    {
        return new VoiceServiceDTO
        {
            ServiceId = voiceService.ServiceId,
            IsRinginngTone = voiceService.IsRinginngTone,
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
            ServiceId = ServiceId,
            IsRinginngTone = IsRinginngTone,
            RingingToneName = RingingToneName,
            RingingToneCharge = RingingToneCharge,
            IsVoiceRoaming = IsVoiceRoaming,
            VoiceRoamingCharge = VoiceRoamingCharge
        };
    }
}