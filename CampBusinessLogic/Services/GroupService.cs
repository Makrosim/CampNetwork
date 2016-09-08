using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

            group.Creator = profile;
            group.Members.Add(profile);

            Database.GroupManager.Create(group);
            profile.Groups.Add(group);

            await Database.SaveAsync();
        }

        public async Task<List<GroupDTO>> GetUsersGroups(string userName)
        {
            var user = await Database.UserManager.FindByNameAsync(userName);
            var profile = user.UserProfile;

            var groupList = profile.Groups.ToArray();
            var groupDTOList = new List<GroupDTO>();

            foreach (var group in groupList)
            {
                var groupDTO = GetGroupData(group.Id);
                groupDTOList.Add(groupDTO);
            }

            return groupDTOList;
        }

        public GroupDTO GetGroupData(int groupId)
        {
            var group = Database.GroupManager.Get(groupId);

            var groupDTO = Mapper.Map<Group, GroupDTO>(group);
            return groupDTO;
        }

        public async Task SetGroupData(string userName, GroupDTO groupDTO)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentNullException(userName);

            var user = await Database.UserManager.FindByNameAsync(userName);
            var profile = Database.UserProfileManager.Get(user.Id);

            var group = Mapper.Map<GroupDTO, Group>(groupDTO);

            Database.GroupManager.Create(group);
        }

        public async Task Delete(string userName, int groupId)
        {
            var user = Database.GroupManager.Get(groupId).Creator.User;

            if (user.UserName != userName)
                throw new UnauthorizedAccessException("У вас нет полномочий совершать это действие");

            Database.GroupManager.Delete(groupId);

            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
