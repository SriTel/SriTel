namespace SriTel.DTO;

public class OtherServicesStatusDTO
{
    public int DataRoamingStatus { get; set; }
    public int VoiceRoamingStatus { get; set; }
    public int RingingToneStatus { get; set; }
    public string RingingTone { get; set; } = string.Empty;
}