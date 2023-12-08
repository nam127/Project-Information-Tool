using Microsoft.EntityFrameworkCore;
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
        return await _set.AsNoTracking().Where(x => visas.Contains(x.Visa)).ToListAsync();
    }

    public IEnumerable<Employee> FindEmployeeByVisas(string visa)
    {
        return Get().Where(x => x.Visa.Contains(visa)).ToList();
    }
}