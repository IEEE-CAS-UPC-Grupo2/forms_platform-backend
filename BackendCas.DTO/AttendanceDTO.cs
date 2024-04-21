namespace BackendCas.DTO;

public class AttendanceDTO
{
    public int IdAttendance { get; set; }

    public int? IdParticipant { get; set; }

    public int? IdEvent { get; set; }
    
    public string Dni { get; set; }
    
    public string Email { get; set; }
}