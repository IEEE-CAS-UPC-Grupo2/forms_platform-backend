using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.DTO
{
    public class EventsCaDTO
    {
        public int IdEvent { get; set; }

        public string? EventTitle { get; set; }

        public string? EventDescription { get; set; }

        public string? ImageUrl { get; set; }

        public string? Modality { get; set; }

        public string? InstitutionInCharge { get; set; }

        public int? Vacancy { get; set; }

        public string? AddressEvent { get; set; }

        public string? Speaker { get; set; }

        public DateTime? EventDateTime { get; set; }

        public int? EventDuration { get; set; }

        public int? IdAdministrator { get; set; }

        //public virtual AdministratorDTO? IdAdministratorNavigation { get; set; }
    }
}
