using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Entities;
using AutoMapper;

namespace AdminDashboard.src.Configs
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}