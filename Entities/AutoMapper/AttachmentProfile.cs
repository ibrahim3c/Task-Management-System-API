using AutoMapper;
using Core.DTOS;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AutoMapper
{
    public class AttachmentProfile:Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AddUpdateTaskAttachmentDTO, TaskAttachment>().ForMember(des => des.AttachmentId, opt => opt.MapFrom(src => src.AttachmentId))
                                                                  .ForMember(des => des.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                                                                  .ForMember(des => des.FileName, opt => opt.MapFrom(src => src.FileName)).ReverseMap();

            CreateMap<GetAttachmentDTO, TaskAttachment>().ForMember(des => des.AttachmentId, opt => opt.MapFrom(src => src.AttachmentId))
                                                                 .ForMember(des => des.FileName, opt => opt.MapFrom(src => src.FileName)).ReverseMap();
        }
    }
}
