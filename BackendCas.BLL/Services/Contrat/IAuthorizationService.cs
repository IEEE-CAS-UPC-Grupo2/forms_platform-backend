using BackendCas.MODEL.Custom;

namespace BackendCas.BLL.Services.Contrat;

public interface IAuthorizationService
{
    Task<AuthorizationResponse> ObtainToken(AuthorizationRequest autorizacion);
    Task<AuthorizationResponse> ObtainRefreshOken(RefreshTokenRequest refreshTokenRequest, int idUsuario);
}