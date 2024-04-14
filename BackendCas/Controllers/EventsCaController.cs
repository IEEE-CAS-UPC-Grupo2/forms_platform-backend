using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using BackendCas.MODEL;

namespace BackendCas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsCaController : ControllerBase
    {
        private readonly BackendCas.BLL.Services.Contrat.IEventsCa _eventsCa;

        public EventsCaController(IEventsCa eventsCa)
        {
            _eventsCa=eventsCa;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Response<List<EventsCaDTO>>();

            try
            {
                rsp.status=true;
                rsp.Value=await _eventsCa.List();
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
        public async Task<IActionResult> Save([FromBody] EventsCaDTO producto)
        {
            var rsp = new Response<EventsCaDTO>();
            try
            {
                rsp.status=true;
                rsp.Value=await _eventsCa.Create(producto);
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
        public async Task<IActionResult> Edit([FromBody] EventsCaDTO producto)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status=true;
                rsp.Value=await _eventsCa.Edit(producto);
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
                rsp.Value=await _eventsCa.Delete(id);
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
