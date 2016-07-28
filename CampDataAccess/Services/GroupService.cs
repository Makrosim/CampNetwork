using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.IO;
using System.Collections.Generic;

namespace CampBusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        IUnitOfWork Database { get; set; }

        public GroupService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> CreateGroup(string name, GroupDTO groupDTO)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<GroupDTO, Group>()
                .ForMember(dest => dest.Creator, opts => opts.MapFrom(src => profile));
            });

            var group = Mapper.Map<GroupDTO, Group>(groupDTO);

            group.Members.Add(profile);

            Database.GroupManager.Create(group);

            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public async List<int> GetAllGroupsId()
        {
            var groupList = Database.GroupManager.GetAll();
            var groupIdList = new List<int>();

            foreach(var group in groupList)
            {
                groupIdList.Add(group.Id);
            }

            return groupIdList;
        }

        public async Task<GroupDTO> GetGroupData(string name, int groupId)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);
            var group = Database.GroupManager.Get(groupId);

            Mapper.Initialize(cfg => { cfg.CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.CreatorFirstName, opts => opts.MapFrom(src => src.Creator.FirstName))
                .ForMember(dest => dest.CreatorLastName, opts => opts.MapFrom(src => src.Creator.LastName))
                .ForMember(dest => dest.IsCreator, opts => opts.MapFrom(src => src.Creator == profile))
                .ForMember(dest => dest.MembersCount, opts => opts.MapFrom(src => src.Members.Count)); });

            var groupDTO = Mapper.Map<Group, GroupDTO>(group);
            return groupDTO;
        }

        public async Task<OperationDetails> SetGroupData(string name, GroupDTO groupDTO)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<GroupDTO, Group>().ForMember("Avatar", c => c.Ignore()); });

            var group = Mapper.Map<GroupDTO, Group>(groupDTO);

            Database.GroupManager.Create(group);

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public async Task<OperationDetails> AddPostToGroup(int groupId, int postId)
        {
            var group = Database.GroupManager.Get(groupId);
            var post = Database.PostManager.Get(postId);

            group.Posts.Add(post);

            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
