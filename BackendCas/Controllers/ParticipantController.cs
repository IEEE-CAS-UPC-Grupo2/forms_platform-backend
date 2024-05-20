using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<ParticipantDTO>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _participantService.List();
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
    public async Task<IActionResult> Save([FromBody] ParticipantDTO participant)
    {
        var rsp = new Response<ParticipantDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participantService.Create(participant);
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
        var rsp = new Response<ParticipantDTO>();
        try
        {
            var participant = await _participantService.GetById(id);
            if (participant == null)
            {
                rsp.status = false;
                rsp.msg = "Participant not found";
            }
            else
            {
                rsp.status = true;
                rsp.Value = participant;
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
    public async Task<IActionResult> Edit([FromBody] ParticipantDTO participant)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _participantService.Edit(participant);
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
            rsp.Value = await _participantService.Delete(id);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }
        return Ok(rsp);
    }
}