using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<decimal>> GetEmployeeListByVisas(string[] visas);
    Task<IEnumerable<Employee>> GetEmployeeByVisas(string[] visas);
    IEnumerable<Employee> FindEmployeeByVisas(string visas);
    IEnumerable<Employee> FindEmployeeVisaByProject(int projectId);
}