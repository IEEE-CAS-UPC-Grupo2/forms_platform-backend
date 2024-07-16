using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.MODEL
{
    public partial class AdministratorsCa
    {
        public int IdAdministrator { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public virtual ICollection<EventsCa> EventsCas { get; } = new List<EventsCa>();

        public virtual ICollection<TokenLog> TokenLogs { get; } = new List<TokenLog>();
    }
}
