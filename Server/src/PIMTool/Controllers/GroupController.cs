using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PIMTool.Controllers
{
    public class GroupController : ApiController
    {
        private IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: api/<GroupController>
        [HttpGet]
        public ActionResult<GroupResponse> Get()
        {
            var response = _groupService.GetAllGroup();
            return Ok(response);
        }
    }
}
