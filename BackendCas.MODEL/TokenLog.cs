using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.MODEL
{
    public partial class TokenLog
    {
        public int IdTokenLog { get; set; }

        public int? IdAdministrator { get; set; }

        public string Token { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }

        public virtual AdministratorsCa? IdAdministratorNavigation { get; set; }
    }
}
