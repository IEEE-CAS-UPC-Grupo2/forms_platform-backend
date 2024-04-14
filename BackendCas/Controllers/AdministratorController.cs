using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;

namespace BackendCas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService=administratorService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Response<List<AdministratorDTO>>();

            try
            {
                rsp.status=true;
                rsp.Value=await _administratorService.List();
            }
            catch (Exception ex)
            {
                rsp.status=false;

                rsp.msg=ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] AdministratorDTO producto)
        {
            var rsp = new Response<AdministratorDTO>();
            try
            {
                rsp.status=true;
                rsp.Value=await _administratorService.Create(producto);
            }
            catch (Exception ex)
            {
                rsp.status=false;

                rsp.msg=ex.Message;
            }

            return Ok(rsp);
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] AdministratorDTO producto)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status=true;
                rsp.Value=await _administratorService.Edit(producto);
            }
            catch (Exception ex)
            {
                rsp.status=false;

                rsp.msg=ex.Message;
            }

            return Ok(rsp);
        }
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status=true;
                rsp.Value=await _administratorService.Delete(id);
            }
            catch (Exception ex)
            {
                rsp.status=false;

                rsp.msg=ex.Message;
            }

            return Ok(rsp);
        }
    }
}
