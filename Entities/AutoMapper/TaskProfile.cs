using AutoMapper;
using Core.DTOS;
using Entities.Models;
using static Core.Constants.GeneralConsts;

namespace Core.AutoMapper
{
    public class TaskProfile: Profile
    {
        public TaskProfile()
        {
            CreateMap<ProjectTask, GetTaskDTO>().ForMember(des => des.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                                                .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title))
                                                .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                                                .ForMember(des => des.Status, opt => opt.MapFrom(src => Enum.GetName<ProjectTaskStatus>(src.Status)))
                                                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));


            CreateMap<AddUpdateTaskDTO, ProjectTask>().ForMember(des => des.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                                                     .ForMember(des => des.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                                                     .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                                                     .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title))
                                                     .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                                                     .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<AddUpdateTaskDTOWithNoAttachment, ProjectTask>().ForMember(des => des.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                                                        .ForMember(des => des.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                                                        .ForMember(des => des.Status, opt => opt.MapFrom(src => src.Status))
                                                        .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title))
                                                        .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                                                        .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap();

        }

    }
}
