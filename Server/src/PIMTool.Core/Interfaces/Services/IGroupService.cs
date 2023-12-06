using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IGroupService
    {
        IEnumerable<Group?> GetAllGroup();
    }
}
