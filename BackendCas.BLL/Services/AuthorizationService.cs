using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.DBContext;
using BackendCas.MODEL.Custom;
using BackendCas.MODEL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthorizationService : IAutorizacionService
{
    private readonly BackendCasContext _context;
    private readonly IConfiguration _configuration;

    public AuthorizationService(BackendCasContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private string GenerateToken(string idUsuario)
    {
        var key = _configuration.GetValue<string>("JwtSettings:key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

        var credencialesToken = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature
        );

        var now = DateTime.Now;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = now.AddMinutes(30),
            NotBefore = now,
            IssuedAt = now,
            SigningCredentials = credencialesToken
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        string tokenCreado = tokenHandler.WriteToken(tokenConfig);

        return tokenCreado;
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

    private async Task<AuthorizationResponse> SaveHistorialTokens(int idUsuario, string token, string refreshToken)
    {
        var historialRefreshToken = new HistorialRefreshToken
        {
            IdUsuario = idUsuario,
            Token = token,
            RefreshToken = refreshToken,
            FechaCreacion = DateTime.Now,
            FechaExpiracion = DateTime.Now.AddDays(1)
        };

        await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
        await _context.SaveChangesAsync();

        return new AuthorizationResponse { IdUsuarioAdm = idUsuario, Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };
    }

    public async Task<AuthorizationResponse> ObtainToken(AuthorizationRequest authorization)
    {
        var usuario_encontrado = _context.Administrators.FirstOrDefault(x =>
            x.EmailAdministrator == authorization.Email && x.PasswordAdministrator == authorization.Clave);

        if (usuario_encontrado == null)
        {
            return await Task.FromResult<AuthorizationResponse>(null);
        }

        string tokenCreado = GenerateToken(usuario_encontrado.IdAdministrator.ToString());
        string refreshTokenCreado = GenerateRefreshToken();

        return await SaveHistorialTokens(usuario_encontrado.IdAdministrator, tokenCreado, refreshTokenCreado);
    }

    public async Task<AuthorizationResponse> ObtainRefreshOken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
    {
        var refreshTokenEncontrado = _context.HistorialRefreshTokens.FirstOrDefault(x =>
            x.Token == refreshTokenRequest.TokenExpirado && x.RefreshToken == refreshTokenRequest.RefreshToken && x.IdUsuario == idUsuario);

        if (refreshTokenEncontrado == null)
        {
            return new AuthorizationResponse { Resultado = false, Msg = "No existe refreshToken" };
        }

        var refreshTokenCreado = GenerateRefreshToken();
        var tokenCreado = GenerateToken(idUsuario.ToString());

        return await SaveHistorialTokens(idUsuario, tokenCreado, refreshTokenCreado);
    }
}
