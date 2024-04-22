using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface ICertificateService
{
    Task<List<CertificateDTO>> List();
    
    Task<CertificateDTO> Create(CertificateDTO model);
}