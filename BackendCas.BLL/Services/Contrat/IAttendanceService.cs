using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IAttendanceService
{
    Task<List<AttendanceDTO>> List();
    
    Task<AttendanceDTO> Create(AttendanceDTO model);
}