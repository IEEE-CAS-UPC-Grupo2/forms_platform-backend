using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface ICertificateService
{
    Task<List<CertificateDTO>> List();
    
    Task<CertificateDTO> Create(CertificateDTO model);
    
    Task<CertificateDTO> Update(CertificateDTO model);
    
    Task<CertificateDTO> Delete(CertificateDTO model);
    
    Task<CertificateDTO> GetById(int id);
}