using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<AttendanceDTO>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _attendanceService.List();
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
    
    [Authorize]
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> Save([FromBody] AttendanceDTO attendance)
    {
        var rsp = new Response<AttendanceDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _attendanceService.Create(attendance);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
     {
         var rsp = new Response<AttendanceDTO>();
         try
         {
             var attendance = await _attendanceService.GetById(id);
             if (attendance == null)
             {
                 rsp.status = false;
                 rsp.msg = "Attendance not found";
             }
             else
             {
                 rsp.status = true;
                 rsp.Value = attendance;
             }
         }
         catch (Exception ex)
         {
             rsp.status = false;
             rsp.msg = ex.Message;
         }
         return Ok(rsp);
     }
    
     [Authorize]
     [HttpPut("{id}")]
     public async Task<IActionResult> Update(int id, [FromBody] AttendanceDTO attendance)
     {
         var rsp = new Response<AttendanceDTO>();
         try
         {
             if (id != attendance.IdAttendance)
             {
                 rsp.status = false;
                 rsp.msg = "ID mismatch";
                 return BadRequest(rsp);
             }
             rsp.Value = await _attendanceService.Update(attendance);
             rsp.status = true;
         }
         catch (Exception ex)
         {
             rsp.status = false;
             rsp.msg = ex.Message;
         }
         return Ok(rsp);
     }
     
     [Authorize]
     [HttpDelete("{id}")]
     public async Task<IActionResult> Delete(int id)
     {
         var rsp = new Response<AttendanceDTO>();
         try
         {
             var attendance = await _attendanceService.GetById(id);
             if (attendance == null)
             {
                 rsp.status = false;
                 rsp.msg = "Attendance not found";
                 return NotFound(rsp);
             }
             rsp.Value = await _attendanceService.Delete(attendance);
             rsp.status = true;
         }
         catch (Exception ex)
         {
             rsp.status = false;
             rsp.msg = ex.Message;
         }
         return Ok(rsp);
     }
}