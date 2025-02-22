﻿using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebAdministratorController : ControllerBase
{
    private readonly IWebAdministratorService _webAdministratorService;

    public WebAdministratorController(IWebAdministratorService webAdministratorService)
    {
        _webAdministratorService = webAdministratorService;
    }

    [Authorize]
    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> List()
    {
        var rsp = new Response<List<AdministratorDTO>>();

        try
        {
            rsp.status = true;
            rsp.Value = await _webAdministratorService.List();
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
    public async Task<IActionResult> Save([FromBody] AdministratorDTO admin)
    {
        var rsp = new Response<AdministratorDTO>();
        try
        {
            rsp.status = true;
            rsp.Value = await _webAdministratorService.Create(admin);
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
    public async Task<IActionResult> Edit([FromBody] AdministratorDTO admin)
    {
        var rsp = new Response<bool>();
        try
        {
            rsp.status = true;
            rsp.Value = await _webAdministratorService.Edit(admin);
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
            rsp.Value = await _webAdministratorService.Delete(id);
        }
        catch (Exception ex)
        {
            rsp.status = false;

            rsp.msg = ex.Message;
        }

        return Ok(rsp);
    }
}