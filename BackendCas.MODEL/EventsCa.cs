using System;
using System.Collections.Generic;

namespace BackendCas.MODEL;

public partial class EventsCa
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

    public virtual Administrator? IdAdministratorNavigation { get; set; }
    
    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();
    
    public virtual ICollection<Certificate> Certificates { get; } = new List<Certificate>();
}
