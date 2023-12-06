using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services;

public interface IProjectEmployeeService
{
    Task AddProjectEmployees(string[] Visas, Project project);
}