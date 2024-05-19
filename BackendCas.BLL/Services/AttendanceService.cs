using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IGenericRepository<Attendance> _AttendanceRepository;
    private readonly IMapper _mapper;

    public AttendanceService(IGenericRepository<Attendance> attendanceRepository, IMapper mapper)
    {
        _AttendanceRepository = attendanceRepository;
        _mapper = mapper;
    }

    async Task<List<AttendanceDTO>> IAttendanceService.List()
    {
        try
        {
            var listAttendance = await _AttendanceRepository.Find();

            return _mapper.Map<List<AttendanceDTO>>(listAttendance.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<AttendanceDTO> IAttendanceService.Create(AttendanceDTO model)
    {
        try
        {
            var attendanceCreate = await _AttendanceRepository.Create(_mapper.Map<Attendance>(model));
            if (attendanceCreate.IdAttendance == 0)
            {
                throw new TaskCanceledException("The attendance doesn't create");
            }

            return _mapper.Map<AttendanceDTO>(attendanceCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    async Task<AttendanceDTO> IAttendanceService.GetById(int id)
    {
        try
        {
            var attendance = await _AttendanceRepository.Obtain(a => a.IdAttendance == id);
            if (attendance == null)
            {
                throw new KeyNotFoundException("Attendance not found");
            }

            return _mapper.Map<AttendanceDTO>(attendance);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    async Task<AttendanceDTO> IAttendanceService.Update(AttendanceDTO model)
    {
        try
        {
            var attendance = await _AttendanceRepository.Obtain(a => a.IdAttendance == model.IdAttendance);
            if (attendance == null)
            {
                throw new KeyNotFoundException("Attendance not found");
            }

            // Map updated values to the existing entity
            _mapper.Map(model, attendance);
            var updated = await _AttendanceRepository.Edit(attendance);

            if (!updated)
            {
                throw new Exception("Update failed");
            }

            return _mapper.Map<AttendanceDTO>(attendance);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    async Task<AttendanceDTO> IAttendanceService.Delete(AttendanceDTO model)
    {
        try
        {
            var attendance = await _AttendanceRepository.Obtain(a => a.IdAttendance == model.IdAttendance);
            if (attendance == null)
            {
                throw new KeyNotFoundException("Attendance not found");
            }

            var deleted = await _AttendanceRepository.Delete(attendance);

            if (!deleted)
            {
                throw new Exception("Delete failed");
            }

            return _mapper.Map<AttendanceDTO>(attendance);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}