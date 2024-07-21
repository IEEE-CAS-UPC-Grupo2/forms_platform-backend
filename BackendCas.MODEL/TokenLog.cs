namespace BackendCas.MODEL;

public class TokenLog
{
    public int IdTokenLog { get; set; }

    public int IdAdministrator { get; set; }

    public string Token { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiredAt { get; set; }

    public virtual WebAdministrator? IdAdministratorNavigation { get; set; }
}