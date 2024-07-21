namespace BackendCas.MODEL.Custom;

public class AuthorizationResponse
{
    public int IdAdministrator { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Result { get; set; }
    public string Msg { get; set; }
}