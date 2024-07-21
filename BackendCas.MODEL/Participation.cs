namespace BackendCas.MODEL;

public class Participation
{
    public int IdParticipation { get; set; }

    public int IdEvent { get; set; }

    public string Dni { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string StudyCenter { get; set; } = null!;

    public string Career { get; set; } = null!;

    public string? IeeeMembershipCode { get; set; }

    public bool HasCertificate { get; set; } = false;

    public bool HasAttended { get; set; } = false;

    public virtual PlatformEvent? IdEventNavigation { get; set; }
}