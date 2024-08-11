namespace BackendCas.MODEL;

public class PlatformEvent
{
    public int IdEvent { get; set; }

    public int IdAdministrator { get; set; }

    public string EventTitle { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    // Online || InPerson
    public string Modality { get; set; } = null!;

    public string InstitutionInCharge { get; set; } = null!;

    public int Vacancy { get; set; }

    public string? Address { get; set; }

    public string Speaker { get; set; } = null!;

    public string EventDateAndTime { get; set; } = null!;

    public int EventDuration { get; set; }

    public virtual WebAdministrator? IdAdministratorNavigation { get; set; }

    public virtual ICollection<Participation> Participations { get; } = new List<Participation>();
}