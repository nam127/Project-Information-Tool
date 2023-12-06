using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Helpers;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;

namespace PIMTool.Repositories;

public class ProjectEmployeeRepository : Repository<ProjectEmployee>, IProjectEmployeeRepository
{
    public ProjectEmployeeRepository(PimContext pimContext, ISortHelper<ProjectEmployee> sortHelper) : base(pimContext)
    {
    }

    public async Task<ProjectEmployee?> GetProjectEmployee(int ProjectId, int EmpId)
    {
        return await _set.AsNoTracking().SingleOrDefaultAsync(x => x.ProjectId == ProjectId && x.EmployeeId == EmpId);
    }

    public IEnumerable<ProjectEmployee?> GetByEmployeeVisas(string[] visas)
    {
        return _set.AsNoTracking().Where(x => visas.Contains(x.Employee.Visa)).ToList();
    }

    public IEnumerable<ProjectEmployee> GetProjectEmployeeList(int ProjectId, int EmpId)
    {
        return _set.AsNoTracking().Where(x => x.ProjectId == ProjectId && x.EmployeeId == EmpId).ToList();
    }
}