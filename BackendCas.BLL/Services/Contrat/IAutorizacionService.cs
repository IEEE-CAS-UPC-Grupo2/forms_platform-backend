using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendCas.MODEL.Custom;

namespace BackendCas.BLL.Services.Contrat;

public interface IAutorizacionService
{
    Task<AuthorizationResponse> ObtainToken(AuthorizationRequest autorizacion);
    Task<AuthorizationResponse> ObtainRefreshOken(RefreshTokenRequest refreshTokenRequest, int idUsuario);

}