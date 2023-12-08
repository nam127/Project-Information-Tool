using AutoMapper;
using PIMTool.Core.Contracts.Response;
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
        public GroupResponse GetAllGroup()
        {
            var groups = _groupRepository.Get();
            var response = new GroupResponse
            {
                groups = groups
            };
            return response;
        }
    }
}
