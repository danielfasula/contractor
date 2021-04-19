using System.Collections.Generic;
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
    public class ContractorsController : ControllerBase
    {

        private readonly ContractorsService _cservice;
        private readonly JobsService _jservice;

        public ContractorsController(ContractorsService cservice, JobsService jservice)
        {
            _cservice = cservice;
            _jservice = jservice;
        }

        [HttpGet]
        public ActionResult<Contractor> Get()
        {
            try
            {
                return Ok(_cservice.GetAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Contractor> Get(int id)
        {
            try
            {
                return Ok(_cservice.GetById(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Contractor>> CreateAsync([FromBody] Contractor newContractor)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newContractor.CreatorId = userInfo.Id;
                Contractor created = _cservice.Create(newContractor);

                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> EditAsync([FromBody] Contractor editData, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editData.Id = id;
                editData.CreatorId = userInfo.Id;
                Contractor editedContractor = _cservice.Edit(editData);
                return Ok(editedContractor);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> DeleteAsync(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_cservice.Delete(id, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/jobs")]
        public ActionResult<IEnumerable<ContractorJobViewModel>> GetJobsByContractorId(int id)
        {
            try
            {
                return Ok(_jservice.GetJobsByContractorId(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}