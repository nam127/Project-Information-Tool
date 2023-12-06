using System.ComponentModel.DataAnnotations;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Contracts.Requests;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos;

namespace PIMTool.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ProjectDto> GetAll([FromQuery]BaseParameters baseParameters)
        {
            var projects = _projectService.GetAllProjectAsync(baseParameters);
            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
        }

        [HttpGet("search")]
        public ActionResult<ProjectDto> GetAllBySearch([FromQuery]FilterParameters filterParameters)
        {
            var projects = _projectService.GetProjectsByFilter(filterParameters);
            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _projectService.GetProjectAsync(id);
            return Ok(_mapper.Map<ProjectDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectAsync([FromBody] CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var projectAdd = await _projectService.AddProjectAsync(request);
            return Ok(projectAdd);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return Ok("Delete Successfully");
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteMultipleProjects(DeleteProjectsRequest request)
        {
            await _projectService.DeleteMultipleProjectsAsync(request.Ids.ToList());
            return Ok("Delete Successfully");
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateProjectAsync(int id, UpdateProjectRequest updateProjectRequest)
        {
            var projectUpdated = await _projectService.UpdateProjectAsync(id, updateProjectRequest);
            return Ok(projectUpdated);
        }

    }
}