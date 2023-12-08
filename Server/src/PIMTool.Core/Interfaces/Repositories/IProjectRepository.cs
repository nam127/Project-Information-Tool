using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        int TotalProjects();
        Task<Project?> GetProjectAsync(int projNum);
        // Task<Project?> GetProjectWithEmployee(int empId);
        IEnumerable<Project?> GetAllProjects(BaseParameters pagingParameters);
        IEnumerable<Project?> GetProjectsFiltered(FilterParameters filterParameters);
    }
}
