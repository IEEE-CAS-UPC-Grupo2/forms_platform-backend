namespace BackendCas.MODEL;

public class WebAdministrator
{
    public int IdAdministrator { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual ICollection<PlatformEvent> EventsCas { get; } = new List<PlatformEvent>();

    public virtual ICollection<TokenLog> TokenLogs { get; } = new List<TokenLog>();
}