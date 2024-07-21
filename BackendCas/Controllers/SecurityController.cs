using System.IdentityModel.Tokens.Jwt;
using BackendCas.BLL.Services.Contrat;
using BackendCas.MODEL.Custom;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public SecurityController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }


    [HttpPost]
    [Route("Authenticate")]
    public async Task<IActionResult> Autenticar([FromBody] AuthorizationRequest authorizationRequest)
    {
        var authorizationResult = await _authorizationService.ObtainToken(authorizationRequest);
        if (authorizationResult == null)
            return Unauthorized();

        return Ok(authorizationResult);
    }


    [HttpPost]
    [Route("ObtainRefreshToken")]
    public async Task<IActionResult> ObtainRefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var expiredTokenToVerify = tokenHandler.ReadJwtToken(request.ExpiredToken);

        if (expiredTokenToVerify.ValidTo > DateTime.UtcNow)
            return BadRequest(new AuthorizationResponse { Result = false, Msg = "Token no ha expirado" });

        var adminId = expiredTokenToVerify.Claims.First(x =>
            x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();
        
        var authorizationResponse = await _authorizationService.ObtainRefreshToken(request, int.Parse(adminId));

        if (authorizationResponse.Result)
            return Ok(authorizationResponse);
        return BadRequest(authorizationResponse);
    }
}