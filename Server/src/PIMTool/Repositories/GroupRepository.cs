using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Helpers;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;

namespace PIMTool.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(PimContext pimContext, ISortHelper<Group> sortHelper) : base(pimContext)
        {
        }
    }
}
