using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
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

        public async Task CreateGroup(string name, GroupDTO groupDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            var group = Mapper.Map<GroupDTO, Group>(groupDTO);

            group.Creator = Database.UserProfileManager.Get(name);
            group.Members.Add(profile);

            Database.GroupManager.Create(group);

            await Database.SaveAsync();
        }

        public async Task<List<GroupDTO>> GetAllGroups(string userName)
        {
            var groupList = Database.GroupManager.GetAll();
            var groupIdList = new List<int>();

            foreach(var group in groupList)
            {
                groupIdList.Add(group.Id);
            }

            var groupDTOList = new List<GroupDTO>();

            foreach (var Id in groupIdList)
            {
                var groupDTO = await GetGroupData(userName, Id);
                groupDTOList.Add(groupDTO);
            }

            return groupDTOList;
        }

        public async Task<GroupDTO> GetGroupData(string name, int groupId)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);

            var profile = Database.UserProfileManager.Get(user.Id);
            var group = Database.GroupManager.Get(groupId);

            var groupDTO = Mapper.Map<Group, GroupDTO>(group);
            return groupDTO;
        }

        public async Task SetGroupData(string name, GroupDTO groupDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            var group = Mapper.Map<GroupDTO, Group>(groupDTO);

            Database.GroupManager.Create(group);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
