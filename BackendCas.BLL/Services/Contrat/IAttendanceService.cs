using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IAttendanceService
{
    Task<List<AttendanceDTO>> List();
    
    Task<AttendanceDTO> Create(AttendanceDTO model);
    
    Task<AttendanceDTO> Update(AttendanceDTO model);
    
    Task<AttendanceDTO> Delete(AttendanceDTO model);
    
    Task<AttendanceDTO> GetById(int id);
}