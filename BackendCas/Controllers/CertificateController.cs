using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CertificateController : ControllerBase
{
    private readonly ICertificateService _certificateService;

    public CertificateController(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<CertificateDTO>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _certificateService.List();
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
    public async Task<IActionResult> Save([FromBody] CertificateDTO certificate)
    {
        var rsp = new Response<CertificateDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _certificateService.Create(certificate);
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
        var rsp = new Response<CertificateDTO>();
        try
        {
            var certificate = await _certificateService.GetById(id);
            if (certificate == null)
            {
                rsp.status = false;
                rsp.msg = "The certificate doesn't exist";
                return NotFound(rsp);
            }
            else
            {
                rsp.status = true;
                rsp.Value = certificate;
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
    public async Task<IActionResult> Update(int id, [FromBody] CertificateDTO certificate)
    {
        var rsp = new Response<CertificateDTO>();
        try
        {
            if (id != certificate.IdCertificate)
            {
                rsp.status = false;
                rsp.msg = "ID mismatch";
                return BadRequest(rsp);
            }
                rsp.status = true;
                rsp.Value = await _certificateService.Update(certificate);
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
        var rsp = new Response<CertificateDTO>();
        try
        {
            var certificate = await _certificateService.GetById(id);
            if (certificate == null)
            {
                rsp.status = false;
                rsp.msg = "Certificate not found";
                return NotFound(rsp);
            }
            rsp.Value = await _certificateService.Delete(certificate);
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