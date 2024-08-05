using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.DBContext;
using BackendCas.MODEL;
using BackendCas.MODEL.Custom;
using BackendCas.UTILITY;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AuthorizationService : IAuthorizationService
{
    private readonly IConfiguration _configuration;
    private readonly BackendCasContext _context;

    public AuthorizationService(BackendCasContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthorizationResponse> ObtainToken(AuthorizationRequest authorization)
    {
        var foundAdmin = _context.AdministratorsCas.FirstOrDefault(x =>
            x.Email == authorization.Email);

        if (foundAdmin == null ||
            !PasswordHelper.VerifyPassword(authorization.Password, foundAdmin.Password))
            return await Task.FromResult<AuthorizationResponse>(null);

        var createdToken = GenerateToken(foundAdmin.IdAdministrator.ToString());
        var createdRefreshToken = GenerateRefreshToken();

        return await SaveTokenLog(foundAdmin.IdAdministrator, createdToken, createdRefreshToken);
    }

    public async Task<AuthorizationResponse> ObtainRefreshToken(RefreshTokenRequest refreshTokenRequest,
        int idAdministrator)
    {
        var foundRefreshToken = _context.TokenLogs.FirstOrDefault(x =>
            x.Token == refreshTokenRequest.ExpiredToken && x.RefreshToken == refreshTokenRequest.RefreshToken &&
            x.IdAdministrator == idAdministrator);

        if (foundRefreshToken == null)
            return new AuthorizationResponse { Result = false, Msg = "No existe RefreshToken" };

        var createdRefreshToken = GenerateRefreshToken();
        var createdToken = GenerateToken(idAdministrator.ToString());

        return await SaveTokenLog(idAdministrator, createdToken, createdRefreshToken);
    }

    private string GenerateToken(string idAdministrator)
    {
        var key = _configuration.GetValue<string>("JwtSettings:key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idAdministrator));

        var tokenCredentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature
        );

        var now = DateTime.Now;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = now.AddHours(3),
            NotBefore = now,
            IssuedAt = now,
            SigningCredentials = tokenCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        var tokenCreated = tokenHandler.WriteToken(tokenConfig);

        return tokenCreated;
    }

    private string GenerateRefreshToken()
    {
        var byteArray = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }
    }

    private async Task<AuthorizationResponse> SaveTokenLog(int idAdministrator, string token, string refreshToken)
    {
        var newTokenLog = new TokenLog
        {
            IdAdministrator = idAdministrator,
            Token = token,
            RefreshToken = refreshToken,
            CreatedAt = DateTime.Now,
            ExpiredAt = DateTime.Now.AddDays(1)
        };

        await _context.TokenLogs.AddAsync(newTokenLog);
        await _context.SaveChangesAsync();

        return new AuthorizationResponse
        {
            IdAdministrator = idAdministrator, Token = token, RefreshToken = refreshToken, Result = true, Msg = "Ok"
        };
    }
}