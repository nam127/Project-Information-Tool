using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;
using PIMTool.Core.Interfaces.Helpers;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;
using PIMTool.Helpers;


namespace PIMTool.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    private ISortHelper<Project> _sortHelper;
    public ProjectRepository(PimContext pimContext, ISortHelper<Project> sortHelper) : base(pimContext)
    {
        _sortHelper = sortHelper;
    }

    public int TotalProjects()
    {
        var projects = _set.ToList();
        return projects.Count;
    }

    public IEnumerable<Project?> GetAllProjects(BaseParameters baseParameters)
    {
        var projects = _set.Include(p => p.ProjectEmployees).ThenInclude(e => e.Employee)
            .AsNoTracking();
        var sortedParams = _sortHelper.ApplySort(projects, baseParameters.OrderBy);

        return PagedListHelper<Project>.ToPagedList(
            sortedParams,
            baseParameters.PageNumber,
            baseParameters.PageSize);
    }

    public async Task<Project?> GetProjectAsync(int projNum)
    {
        return await Get().SingleOrDefaultAsync(x => x.ProjectNumber == projNum);
    }

    public IEnumerable<Project?> GetProjectsFiltered(FilterParameters filterParameters)
    {
        var projects = _set.Include(p => p.ProjectEmployees).ThenInclude(e => e.Employee)
           .AsNoTracking();
        if (filterParameters != null)
        {
            if(filterParameters.SearchTerm != null)
            {
                projects = projects.Where(s => s.Name.Contains(filterParameters.SearchTerm) ||
                s.ProjectNumber.ToString().Contains(filterParameters.SearchTerm) ||
                s.Customer.Contains(filterParameters.SearchTerm))
                    .OrderBy(p => p.ProjectNumber);
            }
            if(filterParameters.Status != Core.Domain.Enums.ProjectStatus.EnumStatus.None)
            {
                projects = projects.Where(p => p.Status == filterParameters.Status)
                .OrderBy(o => o.ProjectNumber);
            }
        }
            
        return PagedListHelper<Project>.ToPagedList(
            projects,
            filterParameters.PageNumber,
            filterParameters.PageSize);
    }
}



