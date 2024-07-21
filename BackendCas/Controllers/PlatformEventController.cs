using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformEventController : ControllerBase
{
    private readonly IPlatformEventService _platformEventService;

    public PlatformEventController(IPlatformEventService platformEventService)
    {
        _platformEventService = platformEventService;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<PlatformEventDTO>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _platformEventService.List();
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
        var rsp = new Response<PlatformEventDTO>();

        try
        {
            var eventCa = await _platformEventService.GetById(id);
            if (eventCa == null)
            {
                rsp.status = false;
                rsp.msg = "The event doesn't exist";
                return NotFound(rsp);
            }

            rsp.status = true;
            rsp.Value = eventCa;
            return Ok(rsp);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize]
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> Save([FromBody] PlatformEventDTO producto)
    {
        var rsp = new Response<PlatformEventDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _platformEventService.Create(producto);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }


    [Authorize]
    [HttpPut]
    [Route("Edit")]
    public async Task<IActionResult> Edit([FromBody] PlatformEventDTO producto)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _platformEventService.Edit(producto);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }

    [Authorize]
    [HttpDelete]
    [Route("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _platformEventService.Delete(id);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
}