using AutoMapper;
using PIMTool.Core.Contracts.Requests;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos;

namespace PIMTool.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MappingProject();
            MappingEmployee();
        }

        private void MappingProject()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(request => request.Visas,
                opt => opt.MapFrom(src => src.ProjectEmployees.Select(pe => pe.Employee.Visa)))
                .ReverseMap();

            CreateMap<CreateProjectRequest, Project>();
            CreateMap<UpdateProjectRequest, Project>()
                .ReverseMap()
                .ForMember(request => request.Visas,
                opt => opt.MapFrom(src => src.ProjectEmployees.Select(pe => pe.Employee.Visa)));
        }

        private void MappingEmployee()
        {
            CreateMap<Employee, EmployeeDto>();
            // .ForMember(x => x.Visa, opt => opt.MapFrom(src => ConvertToString(src.Visa)));
        }

        // private static string ConvertToString(char input)
        // {
        //     string output = input.ToString();
        //     output = output.PadRight(3,'0');
        //     return output;
        // }
    }
}