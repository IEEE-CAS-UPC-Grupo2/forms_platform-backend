using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipationController : ControllerBase
{
    private readonly IParticipationService _participationService;

    public ParticipationController(IParticipationService participationService)
    {
        _participationService = participationService;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<Participation>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _participationService.List();
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }

    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> Save([FromBody] ParticipationDTO participation)
    {
        var rsp = new Response<ParticipationDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participationService.Create(participation);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var rsp = new Response<Participation>();
        try
        {
            var participation = await _participationService.GetById(id);
            if (participation == null)
            {
                rsp.status = false;
                rsp.msg = "Participant not found";
            }
            else
            {
                rsp.status = true;
                rsp.Value = participation;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }

        return Ok(rsp);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromBody] Participation participation)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participationService.Edit(participation);
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
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participationService.Delete(id);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }

    [HttpPatch(template: "Attendance")]
    public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceDTO attendance)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participationService.UpdateAttendance(attendance);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
}