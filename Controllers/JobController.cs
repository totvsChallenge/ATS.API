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
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly JobService _jobService;

        public JobController(ILogger<JobController> logger, JobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }


        [HttpGet]
        public IActionResult Get(long? candidateID = null)
        {
            List<Job> jobs;
            ObjectResult response;

            try
            {
                _logger.LogInformation("Starting Get()");

                jobs = _jobService.Get(candidateID: candidateID);

                response = Ok(jobs);

                _logger.LogInformation($"Finishing Get() with '{jobs.Count}' results");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Job/5
        [HttpGet("{id}", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long id)
        {
            Job job;
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Get( {id} )");

                job = _jobService.Get(id);

                if (job != null)
                {
                    response = Ok(job);
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
        public IActionResult Post([FromBody] Job job)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Post('{JsonConvert.SerializeObject(job, Formatting.None)}')");

                job = _jobService.Insert(job);

                response = Ok(job);

                _logger.LogInformation($"Finishing Post");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Job/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] Job job)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(job, Formatting.None)}')");

                job.ID = id;
                job = _jobService.Update(job);

                response = Ok(job);

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

                _jobService.Delete(id);

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
