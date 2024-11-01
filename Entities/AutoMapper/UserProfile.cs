using AutoMapper;
using Core.DTOS;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO,AppUser>().ForMember(des=>des.UserName,opt=>opt.MapFrom(src=>src.UserName))
                                                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                                                 .ForMember(des=>des.Address,opt=>opt.MapFrom(src=>src.Address))
                                                 ;
        }
    }
}
