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
}