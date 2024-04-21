namespace BackendCas.MODEL;

public partial class Attendance
{
    public int IdAttendance { get; set; }

    public int? IdParticipant { get; set; }

    public int? IdEvent { get; set; }
    
    public string Dni { get; set; }
    
    public string Email { get; set; }
    
    public virtual EventsCa? IdEventNavigation { get; set; }

    public virtual Participant? IdParticipantNavigation { get; set; }
}