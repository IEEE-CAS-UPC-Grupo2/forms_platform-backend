namespace BackendCas.MODEL;

public partial class Participant
{
    public int IdParticipant { get; set; }
    
    public string? Dni { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public string? StudyCenter { get; set; }
    
    public string? Career { get; set; }
    
    public string? IeeeMembershipCode { get; set; }
    
    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();
    
}