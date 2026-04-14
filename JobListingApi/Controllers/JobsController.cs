using JobListingsAPI.Filters;
using JobListingsAPI.Models;
using JobListingsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobListingsAPI.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // GET /api/jobs 
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobs = _jobService.GetAllActive();
            return Ok(jobs);
        }

        // GET /api/jobs/{id} 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var job = _jobService.GetById(id);
            if (job is null)
                return NotFound($"Job with ID {id} not found.");

            return Ok(job);
        }

        // POST /api/jobs
        [HttpPost]
        [ServiceFilter(typeof(ValidateJobFilter))]
        public IActionResult Create([FromBody] JobListing job)
        {
            _jobService.Create(job);
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        // PUT /api/jobs/{id} 
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateJobFilter))]
        public IActionResult Update(int id, [FromBody] JobListing job)
        {
            try
            {
                _jobService.Update(id, job);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE /api/jobs/{id} 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _jobService.SoftDelete(id);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
    }
