using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.DBContext;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformEventController : ControllerBase
{
    private readonly IPlatformEventService _platformEventService;
    private readonly BackendCasContext _context; // Agrega esta línea para el contexto

    public PlatformEventController(IPlatformEventService platformEventService, BackendCasContext context)
    {
        _platformEventService = platformEventService;
        _context = context; // Inicializa el contexto
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
            // Ejecutar el procedimiento almacenado
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteEvent @IdEvent",
                new SqlParameter("@IdEvent", id)
            );

            // Verificar si la eliminación fue exitosa
            rsp.status = result > 0; // result es un int y se compara con 0 para determinar el estado
            rsp.Value = rsp.status;
            rsp.msg = rsp.status ? "Event deleted successfully" : "Event not found or could not be deleted";
        }
        catch (Exception ex)
        {
            rsp.status = false;
            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
}