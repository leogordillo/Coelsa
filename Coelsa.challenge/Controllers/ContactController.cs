using Coelsa.Challenge.Api.Aplication;
using Coelsa.Challenge.Api.Aplication.Command;
using Coelsa.Challenge.Api.Aplication.Dto;
using Coelsa.Challenge.Api.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContactAdd> _logger;
        public ContactController(IMediator mediator, ILogger<ContactAdd> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(ContactAdd.ParamAdd data)
        {
            try
            {
                var result = await _mediator.Send(data);
                return Ok("Los datos se guardaron con éxito");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       $"Ooops! Algo no salió bien al registrar los datos.");
            }

        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Unit>> Delete(ContactDelete.ParamDel data)
        {
            try
            {
                return await _mediator.Send(data);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       $"Ooops! Algo no salió bien al eliminar los datos.");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Unit>> Update(ContactUpdate.ParamUpd data)
        {
            try
            {
                return await _mediator.Send(data);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       $"Ooops! Algo no salió bien al actulizar los datos.");
            }
        }

        [HttpGet("Dummy")]
        public ActionResult<Contact> Dummy ()
        {
            try {
                var dummy = new Contact { FirstName = "Juan", LastName = "Perez", Email = "jperez@coelsa.com.ar", Company = "COELSA S.A." };
                return Ok(dummy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(int.Parse(HttpStatusCode.InternalServerError.ToString()),
                    "Ooops! ocurrió un error al intentar realizar la consulta de datos. ");
            }
        }

        [HttpGet("GetByCompany")]
        public async Task<ActionResult> GetByCompany([FromQuery]CompanyFilter.ContactList data)
        {
            try
            {
                IEnumerable<ContactDto> contactDtos = await _mediator.Send(data);
                if (contactDtos == null || contactDtos.Count() == 0) return NotFound("No se obtuvieron resultados con esos parámetros de búsqueda.");
                else  return Ok(contactDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError,
                       $"Ooops! Algo no salió bien al eliminar los datos.");
            }
        }
    }
}