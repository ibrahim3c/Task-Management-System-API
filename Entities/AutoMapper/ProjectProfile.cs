using AutoMapper;
using Core.DTOS;
using Entities.Models;

namespace Core.AutoMapper
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, GetProjectDTO>().ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
                                               .ForMember(des => des.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                                               .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                                               .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.UserId))
                                               .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate)).ReverseMap();

            // Add this mapping for AddUpdateProjectDTO to Project
            CreateMap<AddUpdateProjectDTO, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
