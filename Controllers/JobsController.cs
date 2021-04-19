using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using contractor.Models;
using contractor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace job.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {

        private readonly JobsService _jservice;
        public JobsController(JobsService jservice)
        {
            _jservice = jservice;
        }

        [HttpGet]
        public ActionResult<Job> Get()
        {
            try
            {
                return Ok(_jservice.GetAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Job> Get(int id)
        {
            try
            {
                return Ok(_jservice.GetById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Job>> CreateAsync([FromBody] Job newJob)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newJob.CreatorId = userInfo.Id;
                Job created = _jservice.Create(newJob);

                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> EditAsync([FromBody] Job editData, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editData.Id = id;
                editData.CreatorId = userInfo.Id;
                Job editedJob = _jservice.Edit(editData);
                return Ok(editedJob);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> DeleteAsync(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_jservice.Delete(id, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}