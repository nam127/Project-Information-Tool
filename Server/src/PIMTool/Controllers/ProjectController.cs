using System.ComponentModel.DataAnnotations;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Contracts.Requests;
using PIMTool.Core.Contracts.Response;
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
        public ActionResult<ProjectResponse> GetAll([FromQuery]BaseParameters baseParameters)
        {
            var projects = _projectService.GetAllProjectAsync(baseParameters);
            return Ok(projects);
        }

        [HttpGet("search")]
        public ActionResult<ProjectResponse> GetAllBySearch([FromQuery]FilterParameters filterParameters)
        {
            var projects = _projectService.GetProjectsByFilter(filterParameters);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _projectService.GetProjectAsync(id);
            var projectDto = _mapper.Map<Project, ProjectDto>(entity);
            return projectDto;
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteMultipleProjects(int[] Ids)
        {
            await _projectService.DeleteMultipleProjectsAsync(Ids);
            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateProjectAsync(int id, UpdateProjectRequest updateProjectRequest)
        {
            var projectUpdated = await _projectService.UpdateProjectAsync(id, updateProjectRequest);
            return Ok(projectUpdated);
        }

        [HttpGet("check-project-number")]
        public async Task<IActionResult> CheckProjectNumberExist([FromQuery] int projectNumber)
        {
            var project = await _projectService.CheckByProjectNumber(projectNumber);
            return Ok(project);
        }

    }
}