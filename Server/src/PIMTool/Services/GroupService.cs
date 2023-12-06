using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public GroupService(IMapper mapper, IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public IEnumerable<Group?> GetAllGroup()
        {
            var groups = _groupRepository.Get();
            return _mapper.Map<IEnumerable<Group>>(groups);
        }
    }
}
