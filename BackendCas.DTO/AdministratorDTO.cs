using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCas.DTO;

public class AdministratorDTO
{
    public int IdAdministrator { get; set; }

    public string? NameAdministrator { get; set; }

    public string? EmailAdministrator { get; set; }

    public string? PasswordAdministrator { get; set; }
    //public virtual ICollection<EventsCaDTO> EventsCas { get; set; }
}