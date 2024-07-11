using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BackendCas.BLL.Services.Contrat;
using BackendCas.DTO;
using BackendCas.Utils;
using BackendCas.MODEL;
using Microsoft.AspNetCore.Authorization;
using BackendCas.BLL.Services;

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

        [Authorize]
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
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rsp = new Response<EventsCaDTO>();

            try
            {
                var eventCa = await _eventsCa.GetById(id);
                if (eventCa == null)
                {
                    rsp.status=false;
                    rsp.msg="The event doesn't exist";
                    return NotFound(rsp);
                }
                else
                {
                    rsp.status=true;
                    rsp.Value=eventCa;
                    return Ok(rsp);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rsp = new Response<EventsCaDTO>();
            try
            {
                var events = await _eventsCa.GetById(id);
                if (events == null)
                {
                    rsp.status = false;
                    rsp.msg = "The certificate doesn't exist";
                    return NotFound(rsp);
                }
                else
                {
                    rsp.status = true;
                    rsp.Value = events;
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
        [Authorize]
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
        [Authorize]
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
