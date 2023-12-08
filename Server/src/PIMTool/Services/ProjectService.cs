using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Constants;
using PIMTool.Core.Contracts.Requests;
using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Enums;
using PIMTool.Core.Domain.Objects;
using PIMTool.Core.Exceptions;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper,
            IEmployeeRepository employeeRepository, IProjectEmployeeRepository projectEmployeeRepository,
            IGroupRepository groupRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _groupRepository = groupRepository;
        }

        public ProjectResponse GetAllProjectAsync(BaseParameters pagingParameters)
        {
            var projects = _projectRepository.GetAllProjects(pagingParameters);
            var count = _projectRepository.TotalProjects();
            var response = new ProjectResponse
            {
                Projects = projects,
                pageNumber = pagingParameters.PageNumber,
                pageSize = pagingParameters.PageSize,
                totalPages = count
            };
            return response;
        }

        public ProjectResponse GetProjectsByFilter(FilterParameters filterParameters)
        {
            var projects = _projectRepository.GetProjectsFiltered(filterParameters);
            var response = new ProjectResponse
            {
                Projects = projects,
                pageNumber = filterParameters.PageNumber,
                pageSize = filterParameters.PageSize,
                totalPages = projects.Count()
            };
            return response;
        }

        public async Task<Project?> GetProjectAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.GetAsync(id, cancellationToken);
        }
        public async Task<CreateProjectRequest> AddProjectAsync(CreateProjectRequest projectRequest, CancellationToken cancellationToken = default)
        {
            var projectsExist = await _projectRepository.GetProjectAsync((int)projectRequest.ProjectNumber).ConfigureAwait(false);
            if (projectsExist != null)
            {
                throw new DupplicateProjectNumberException(ExceptionMessageConstantsException.DUPPLICATE_NUMBER_MESSAGE);
            }
            var employeeIds = await _employeeRepository.GetEmployeeByVisas(projectRequest.Visas);
            if (employeeIds.Count() == 0)
            {
                throw new ProjectNotFoundException("Not found member");
            }
            var project = _mapper.Map<Project>(projectRequest);
            project.Version = Guid.NewGuid();
            var peList = employeeIds.Select(emp =>
                new ProjectEmployee { Project = project, EmployeeId = emp.Id });
            await _projectRepository.AddAsync(project, cancellationToken);
            await _projectEmployeeRepository.AddRangeAsync(peList.ToArray(), cancellationToken);
            await _projectEmployeeRepository.SaveChangesAsync(cancellationToken);
            await _projectRepository.SaveChangesAsync(cancellationToken);
            return projectRequest;
        }

        public async Task DeleteProjectAsync(int id, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetAsync(id, cancellationToken) ?? throw new ProjectNotFoundException("The project is not existed");
            if (project.Status != ProjectStatus.EnumStatus.NEW)
            {
                throw new BusinessException(ExceptionMessageConstantsException.BUSINESS_MESSAGE);
            }

            _projectRepository.DeleteAsync(project);
            await _projectRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<UpdateProjectRequest> UpdateProjectAsync(int id, UpdateProjectRequest updateProject, CancellationToken cancellationToken = default)
        {
            try
            {
                var projectExist = await _projectRepository.GetAsync((int)updateProject.Id, cancellationToken);

                if (projectExist is null)
                {
                    throw new ProjectNotFoundException(ExceptionMessageConstantsException.PROJECT_NOT_FOUND);
                }
                var oldProjectRequest = _mapper.Map<UpdateProjectRequest>(projectExist);
                var employeeIds = await _employeeRepository.GetEmployeeListByVisas(updateProject.Visas);

                if (employeeIds.Count() == 0)
                {
                    throw new Exception("Not found member");
                }

                if (oldProjectRequest.Visas.Length > 0)
                {
                    //var employeeFound = await _employeeRepository.GetByVisa(oldProjectRequest.Visas);
                    var projEmpFoundByVisas = _projectEmployeeRepository.GetProjectEmployeeList((int)projectExist.Id, (int)projectExist.Id);
                    //projectExist.ProjectEmployees.Clear();
                    _projectEmployeeRepository.Delete(projEmpFoundByVisas.ToArray());
                }

                if (updateProject.Visas != null)
                {
                    var employeeListFound = await _employeeRepository.GetEmployeeByVisas(updateProject.Visas);
                    var updatedEmployees = employeeListFound.Select(employee =>
                    new ProjectEmployee { ProjectId = projectExist.Id, EmployeeId = employee.Id });
                    await _projectEmployeeRepository.AddRangeAsync(updatedEmployees.ToArray());
                }
                
                var project = _mapper.Map(updateProject, projectExist);
                _projectRepository.Update(project);
                await _projectRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConcurrencyUpdateException(ExceptionMessageConstantsException.CONCURRENCY_UPDATE_MESSAGE);
            }
            return updateProject;
        }

        public async Task DeleteMultipleProjectsAsync(IList<int> Ids, CancellationToken cancellationToken = default)
        {
            foreach (int id in Ids)
            {
                var project = await _projectRepository.GetAsync(id);
                if (project is null)
                {
                    throw new ProjectNotFoundException(ExceptionMessageConstantsException.PROJECT_NOT_FOUND);
                }

                if (project.Status != ProjectStatus.EnumStatus.NEW)
                {
                    throw new BusinessException(ExceptionMessageConstantsException.BUSINESS_MESSAGE);
                }

                _projectRepository.DeleteAsync(project);
            }
            await _projectRepository.SaveChangesAsync(cancellationToken);
        }
    }
}