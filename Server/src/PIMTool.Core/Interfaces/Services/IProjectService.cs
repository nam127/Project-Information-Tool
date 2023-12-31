﻿using PIMTool.Core.Contracts.Requests;
using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectAsync(int id, CancellationToken cancellationToken = default);
        ProjectResponse GetAllProjectAsync(BaseParameters pagingParameters);
        Task<CreateProjectRequest> AddProjectAsync(CreateProjectRequest createProjectRequest, CancellationToken cancellationToken = default);
        Task DeleteProjectAsync(int id ,CancellationToken cancellationToken = default);
        Task<UpdateProjectRequest> UpdateProjectAsync(int id, UpdateProjectRequest updateProjectRequest, CancellationToken cancellationToken = default);
        Task DeleteMultipleProjectsAsync(IList<int> Ids, CancellationToken cancellationToken = default);
        ProjectResponse GetProjectsByFilter(FilterParameters filterParameters);
        Task<bool> CheckByProjectNumber(int projectNumber);
    }
}