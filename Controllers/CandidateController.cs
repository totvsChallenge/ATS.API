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
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;
        private readonly CandidateService _candidateService;

        public CandidateController(ILogger<CandidateController> logger, CandidateService candidateService)
        {
            _logger = logger;
            _candidateService = candidateService;
        }


        [HttpGet]
        public IActionResult Get(string name = null, long? jobID = null)
        {
            List<Candidate> candidates;
            ObjectResult response;

            try
            {
                _logger.LogInformation("Starting Get()");

                candidates = _candidateService.Get(name: name, jobID: jobID);

                response = Ok(candidates);

                _logger.LogInformation($"Finishing Get() with '{candidates.Count}' results");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Candidate/5
        [HttpGet("{id}", Name = "GetCandidate")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long id)
        {
            Candidate candidate;
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Get( {id} )");

                candidate = _candidateService.Get(id);

                if (candidate != null)
                {
                    response = Ok(candidate);
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
        public IActionResult Post([FromBody] Candidate candidate)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Post('{JsonConvert.SerializeObject(candidate, Formatting.None)}')");

                candidate = _candidateService.Insert(candidate);

                response = Ok(candidate);

                _logger.LogInformation($"Finishing Post");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Candidate/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] Candidate candidate)
        {
            ObjectResult response;

            try
            {
                _logger.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(candidate, Formatting.None)}')");

                candidate.ID = id;
                candidate = _candidateService.Update(candidate);

                response = Ok(candidate);

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

                _candidateService.Delete(id);

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
