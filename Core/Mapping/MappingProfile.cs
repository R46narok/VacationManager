using System;
using AutoMapper;
using Core.Data.Entities;
using Core.Dto;
using Core.ViewModels;
using Models;
using Repositories.Helpers;
using ViewModels.DTO;
using ViewModels.Input;

namespace Repositories.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Models -> DTOs
        CreateMap<Project, ProjectDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<Team, TeamDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Vacation, VacationDTO>().ReverseMap();

        //ViewModels -> Models
        CreateMap<ProjectViewModel, Project>().ReverseMap();
        CreateMap<RoleViewModel, Role>().ReverseMap();
        CreateMap<TeamViewModel, Team>().ReverseMap();
        CreateMap<RegisterUserViewModel, User>()
            .ForMember(user => user.PasswordHash, opt => opt.MapFrom(src => Hasher.Hash(src.Password))).ReverseMap();
        CreateMap<VacationViewModel, Vacation>()
            .ForMember(vacation => vacation.CreationDate, opt => opt.MapFrom(src => DateTime.Now)).ReverseMap();
    }
}