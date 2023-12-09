using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Helpers;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;

namespace PIMTool.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(PimContext pimContext, ISortHelper<Employee> sortHelper) : base(pimContext)
    {
    }

    public async Task<IEnumerable<decimal>> GetEmployeeListByVisas(string[] visas)
    {
        return await _set.AsNoTracking().Where(x => visas.Contains(x.Visa)).Select(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetEmployeeByVisas(string[] visas)
    {
        var employees = new List<Employee>();
        foreach (var visa in visas) {
            var employeesWithVisa = await _set.AsNoTracking().Where(x => x.Visa.Contains(visa)).ToListAsync();
            employees.AddRange(employeesWithVisa);
        }
        return employees;
    }

    public IEnumerable<Employee> FindEmployeeByVisas(string visa)
    {
        return Get().Where(x => x.Visa.Contains(visa)).ToList();
    }

    public IEnumerable<Employee> FindEmployeeVisaByProject(int projectId)
    {
        var employees = (from e in _pimContext.Employees
                     join pe in _pimContext.ProjectEmployees
                     on e.Id equals pe.EmployeeId
                     where pe.ProjectId == projectId
                     select e).ToList();
        return employees;
    }
}