using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using contractor.Models;
using contractor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contractor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorJobsController : ControllerBase
    {
        private readonly ContractorJobsService _cjservice;
        public ContractorJobsController(ContractorJobsService cjservice)
        {
            _cjservice = cjservice;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ContractorJob>> CreateAsync([FromBody] ContractorJob newCJ)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newCJ.CreatorId = userInfo.Id;
                return Ok(_cjservice.Create(newCJ));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _cjservice.Delete(id);
                return Ok("deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}