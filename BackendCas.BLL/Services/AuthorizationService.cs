using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.DBContext;
using BackendCas.MODEL;
using BackendCas.MODEL.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.BLL.Services
{
    public class AuthorizationService: IAutorizacionService
    {
        private readonly BackendCasContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(BackendCasContext context, IConfiguration configuration)
        {
            _context=context;
            _configuration=configuration;
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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
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
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }


        private async Task<AuthorizationResponse> SaveHistorialTokens(
          int idUsuario,
          string token,
          string refreshToken
          )
        {

            var historialRefreshToken = new HistorialRefreshToken
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddDays(2)
            };


            await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _context.SaveChangesAsync();

            return new AuthorizationResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };

        }



        public async Task<AuthorizationResponse> ObtainToken(AuthorizationRequest authorization)
        {
            var usuario_encontrado = _context.Administrators.FirstOrDefault(x =>
                           x.NameAdministrator == authorization.NombreUsuario &&
                           x.PasswordAdministrator == authorization.Clave
                       );

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
                       x.Token == refreshTokenRequest.TokenExpirado &&
                       x.RefreshToken == refreshTokenRequest.RefreshToken &&
                       x.IdUsuario == idUsuario);

            if (refreshTokenEncontrado == null)
                return new AuthorizationResponse { Resultado = false, Msg = "No existe refreshToken" };

            var refreshTokenCreado = GenerateRefreshToken();
            var tokenCreado = GenerateToken(idUsuario.ToString());

            return await SaveHistorialTokens(idUsuario, tokenCreado, refreshTokenCreado);
        }

       
    }
}
