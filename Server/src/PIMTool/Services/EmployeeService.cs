using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public EmployeeResponse GetEmployee(string visa)
    {
        var employees = _employeeRepository.FindEmployeeByVisas(visa);
        var response = new EmployeeResponse
        {
            Employees = employees
        };
        return response;
    }

    public EmployeeResponse GetEmployeeVisas(int projectId)
    {
        var employees = _employeeRepository.FindEmployeeVisaByProject(projectId);
        var response = new EmployeeResponse { Employees = employees };
        return response;
    }
}