namespace BackendCas.MODEL;

public class EventPlatform
{
    public int IdEvent { get; set; }

    public int? IdAdministrator { get; set; }

    public string EventTitle { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? Modality { get; set; }

    public string? InstitutionInCharge { get; set; }

    public int? Vacancy { get; set; }

    public string? Address { get; set; }

    public string? Speaker { get; set; }

    public string? EventDateAndTime { get; set; }

    public int? EventDuration { get; set; }

    public virtual WebAdministrator? IdAdministratorNavigation { get; set; }

    public virtual ICollection<Participation> Participations { get; } = new List<Participation>();
}