using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CampBusinessLogic.DTO;
using CampDataAccess.Entities;

namespace CampBusinessLogic.AutoMapper
{
    public static class AutoMapper
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<CampPlaceDTO, CampPlace>();

                cfg.CreateMap<CampPlace, CampPlaceDTO>()
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.UserProfile.User.UserName))
                .ForMember(dest => dest.PostsCount, opts => opts.MapFrom(src => src.Posts.Count))
                .ForMember(dest => dest.AuthorFirstName, opts => opts.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dest => dest.AuthorLastName, opts => opts.MapFrom(src => src.UserProfile.LastName));

                cfg.CreateMap<GroupDTO, Group>()
                .ForMember("Creator", c => c.Ignore());

                cfg.CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.Creator, opts => opts.MapFrom(src => src.Creator.User.UserName))
                .ForMember(dest => dest.CreatorFirstName, opts => opts.MapFrom(src => src.Creator.FirstName))
                .ForMember(dest => dest.CreatorLastName, opts => opts.MapFrom(src => src.Creator.LastName))
                .ForMember(dest => dest.MembersCount, opts => opts.MapFrom(src => src.Members.Count));

                cfg.CreateMap<MessageDTO, Message>()
                .ForMember("Id", c => c.Ignore());

                cfg.CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.UserProfile.User.UserName))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.UserProfile.LastName));

                cfg.CreateMap<PostDTO, Post>();

                cfg.CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.CampPlaceName, opts => opts.MapFrom(src => src.CampPlace.Name))
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.CampPlace.UserProfile.User.UserName))
                .ForMember(dest => dest.CampPlaceId, opts => opts.MapFrom(src => src.CampPlace.Id))
                .ForMember(dest => dest.AuthorFirstName, opts => opts.MapFrom(src => src.CampPlace.UserProfile.FirstName))
                .ForMember(dest => dest.AuthorLastName, opts => opts.MapFrom(src => src.CampPlace.UserProfile.LastName));

                cfg.CreateMap<ProfileDTO, UserProfile>()
                .ForMember("Id", c => c.Ignore());

                cfg.CreateMap<UserProfile, ProfileDTO>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName));
            });

        }
    }
}
