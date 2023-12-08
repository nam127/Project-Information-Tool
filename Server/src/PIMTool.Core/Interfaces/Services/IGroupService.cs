using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IGroupService
    {
        GroupResponse GetAllGroup();
    }
}
