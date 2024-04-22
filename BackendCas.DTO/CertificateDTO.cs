namespace BackendCas.DTO;

public class CertificateDTO
{
    public int IdCertificate { get; set; }

    public int? IdParticipant { get; set; }

    public int? IdEvent { get; set; }

    public bool? IsDelivered { get; set; }
}