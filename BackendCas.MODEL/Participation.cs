using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.MODEL
{
    public partial class Participation
    {
        public int IdParticipation { get; set; }

        public int? IdEvent { get; set; }

        public string Dni { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? StudyCenter { get; set; }

        public string? Career { get; set; }

        public string? IeeemembershipCode { get; set; }

        public bool HasCertificate { get; set; }

        public bool HasAttended { get; set; }

        public virtual EventsCa? IdEventNavigation { get; set; }
    }
}
