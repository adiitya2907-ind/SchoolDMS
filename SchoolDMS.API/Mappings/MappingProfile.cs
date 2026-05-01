using AutoMapper;
using SchoolDMS.API.Models.DTOs.Auth;
using SchoolDMS.API.Models.DTOs.Documents;
using SchoolDMS.API.Models.DTOs.Projects;
using SchoolDMS.API.Models.DTOs.Schools;
using SchoolDMS.API.Models.DTOs.Users;
using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Models.Entities;

namespace SchoolDMS.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : src.RoleId.ToString()));
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<RegisterDTO, User>();

            // Schools
            CreateMap<School, SchoolDTO>();
            CreateMap<CreateSchoolDTO, School>();
            CreateMap<UpdateSchoolDTO, School>();

            // Projects
            CreateMap<Project, ProjectDTO>();
            CreateMap<CreateProjectDTO, Project>();

            // Visits
            CreateMap<Visit, VisitDTO>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School != null ? src.School.SchoolName : string.Empty))
                .ForMember(dest => dest.UdiseCode, opt => opt.MapFrom(src => src.School != null ? src.School.UdiseCode : string.Empty))
                .ForMember(dest => dest.EngineerName, opt => opt.MapFrom(src => src.Engineer != null ? $"{src.Engineer.FirstName} {src.Engineer.LastName}" : string.Empty))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : string.Empty))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Visit, VisitListDTO>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School != null ? src.School.SchoolName : string.Empty))
                .ForMember(dest => dest.UdiseCode, opt => opt.MapFrom(src => src.School != null ? src.School.UdiseCode : string.Empty))
                .ForMember(dest => dest.EngineerName, opt => opt.MapFrom(src => src.Engineer != null ? $"{src.Engineer.FirstName} {src.Engineer.LastName}" : string.Empty))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateVisitDTO, Visit>();

            // Documents
            CreateMap<Document, DocumentDTO>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()))
                .ForMember(dest => dest.DocumentStatus, opt => opt.MapFrom(src => src.DocumentStatus.ToString()))
                .ForMember(dest => dest.UploadedBy, opt => opt.MapFrom(src => src.UploadedBy != null ? $"{src.UploadedBy.FirstName} {src.UploadedBy.LastName}" : string.Empty));
        }
    }
}
