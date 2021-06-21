using ATS.API.Model;
using ATS.API.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ATS.API.Controllers
{
    [EnableCors("Policy1")]
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateJobController : ControllerBase
    {
        private readonly ILogger<CandidateJobController> _logger;
        private readonly CandidateJobService _candidateJobService;

        public CandidateJobController(ILogger<CandidateJobController> logger, CandidateJobService candidateJobService)
        {
            _logger = logger;
            _candidateJobService = candidateJobService;
        }


        [HttpGet]
        public IActionResult Get(long? candidateID = null, long? jobID = null)
        {
            List<CandidateJob> candidateJobs;
            ObjectResult response;

            try
            {
                _logger.LogInformation("Starting Get()");

                candidateJobs = _candidateJobService.Get(candidateID: candidateID, jobID: jobID);

                response = Ok(candidateJobs);

                _logger.LogInformation($"Finishing Get() with '{candidateJobs.Count}' results");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/CandidateJob/5
        [HttpGet("{id}", Name = "GetCandidateJob")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long id)
        {
            CandidateJob candidateJob;
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Get( {id} )");

                candidateJob = _candidateJobService.Get(id);

                if (candidateJob != null)
                {
                    response = Ok(candidateJob);
                }
                else
                {
                    response = NotFound(string.Empty);
                }

                _logger.LogInformation($"Finishing Get( {id} )");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CandidateJob candidateJob)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Post('{JsonConvert.SerializeObject(candidateJob, Formatting.None)}')");

                candidateJob = _candidateJobService.Insert(candidateJob);

                response = Ok(candidateJob);

                _logger.LogInformation($"Finishing Post");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/CandidateJob/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] CandidateJob candidateJob)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(candidateJob, Formatting.None)}')");

                candidateJob.ID = id;
                candidateJob = _candidateJobService.Update(candidateJob);

                response = Ok(candidateJob);

                _logger.LogInformation($"Finishing Put( {id} )");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long id)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Delete( {id} )");

                _candidateJobService.Delete(id);

                response = Ok(string.Empty);

                _logger.LogInformation($"Finishing Delete( {id} )");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
