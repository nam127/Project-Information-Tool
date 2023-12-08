using PIMTool.Core.Contracts.Response;

namespace PIMTool.Core.Interfaces.Services;

public interface IEmployeeService
{
    EmployeeResponse GetEmployee(string visa);   
}