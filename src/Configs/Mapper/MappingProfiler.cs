using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Category;
using AdminDashboard.src.Dtos.Role;
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

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<AssignRoleToUserDto, User>();
        }
    }
}