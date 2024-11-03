using AutoMapper;
using Core.DTOS;
using Core.Models;

namespace Core.AutoMapper
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<AppRole, GetRoleDTO>().ForMember(des => des.RoleId, opt => opt.MapFrom(src => src.Id))
                                            .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Name)).ReverseMap();

        }
    }
}
