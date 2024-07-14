using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.MODEL.Custom
{
    public class AuthorizationResponse
    {
        public int IdUsuarioAdm { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Resultado { get; set; }
        public string Msg { get; set; }
    }
}
