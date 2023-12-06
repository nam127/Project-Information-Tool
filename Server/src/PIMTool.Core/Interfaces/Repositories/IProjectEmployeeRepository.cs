using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Repositories;

public interface IProjectEmployeeRepository : IRepository<ProjectEmployee>
{
    Task<ProjectEmployee?> GetProjectEmployee (int ProjectId, int EmpId);
    IEnumerable<ProjectEmployee?> GetByEmployeeVisas(string[] visas);
    IEnumerable<ProjectEmployee> GetProjectEmployeeList(int ProjectId, int EmpId);
}