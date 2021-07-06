using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using patback.Models;
using patback.Services;

using Newtonsoft.Json;
using patback.Utiltiy;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace patback.Controllers
{
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("patient/get/{page}/{pagesize}")]
        public async Task<IEnumerable<Patient>> Get(int page, int pagesize)
        {
            try
            {
                return await PatientService.Instance.GetAllAsync(page, pagesize);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }

        [HttpPost]
        [Route("patient/save")]
        public IActionResult savePatient([FromBody] Patient p)
        {
            Returned ReturnObj = new Returned();
            try
            {
                if (ModelState.IsValid)
                {
                    ReturnObj = PatientService.Instance.createPatient(p);
                    return Ok(JsonConvert.SerializeObject(ReturnObj));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}