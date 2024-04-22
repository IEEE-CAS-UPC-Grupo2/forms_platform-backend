namespace BackendCas.MODEL;

public partial class Certificate
{
    public int IdCertificate { get; set; }

    public int? IdParticipant { get; set; }

    public int? IdEvent { get; set; }
    
    public bool? IsDelivered { get; set; }

    public virtual EventsCa? IdEventNavigation { get; set; }

    public virtual Participant? IdParticipantNavigation { get; set; }
}