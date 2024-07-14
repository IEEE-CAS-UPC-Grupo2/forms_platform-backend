using BackendCas.BLL.Services.Contrat;
using BackendCas.MODEL.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;


namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly IAutorizacionService _autorizacionService;

    public SecurityController(IAutorizacionService autorizacionService)
    {
        _autorizacionService = autorizacionService;
    }


    [HttpPost]
    [Route("Autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] AuthorizationRequest autorizacion)
    {
        var resultado_autorizacion = await _autorizacionService.ObtainToken(autorizacion);
        if (resultado_autorizacion == null)
            return Unauthorized();

        return Ok(resultado_autorizacion);
    }


    [HttpPost]
    [Route("ObtenerRefreshToken")]
    public async Task<IActionResult> ObtenerRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(request.TokenExpirado);

        if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
            return BadRequest(new AuthorizationResponse { Resultado = false, Msg = "Token no ha expirado" });

        var idUsuario = tokenExpiradoSupuestamente.Claims.First(x =>
            x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();


        var autorizacionResponse = await _autorizacionService.ObtainRefreshOken(request, int.Parse(idUsuario));

        if (autorizacionResponse.Resultado)
            return Ok(autorizacionResponse);
        else
            return BadRequest(autorizacionResponse);
    }
}