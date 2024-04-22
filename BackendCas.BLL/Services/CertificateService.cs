using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class CertificateService : ICertificateService
{
    private readonly IGenericRepository<Certificate> _CertificateRepository;
    private readonly IMapper _mapper;

    public CertificateService(IGenericRepository<Certificate> certificateRepository, IMapper mapper)
    {
        _CertificateRepository = certificateRepository;
        _mapper = mapper;
    }

    async Task<List<CertificateDTO>> ICertificateService.List()
    {
        try
        {
            var listCertificate = await _CertificateRepository.Find();

            return _mapper.Map<List<CertificateDTO>>(listCertificate.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<CertificateDTO> ICertificateService.Create(CertificateDTO model)
    {
        try
        {
            var certificateCreate = await _CertificateRepository.Create(_mapper.Map<Certificate>(model));
            if (certificateCreate.IdCertificate == 0)
            {
                throw new TaskCanceledException("The certificate doesn't create");
            }

            return _mapper.Map<CertificateDTO>(certificateCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}