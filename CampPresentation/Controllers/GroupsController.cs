using System.Web.Mvc;
using System.Threading.Tasks;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System;
using System.Collections.Generic;

namespace CampAuth.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupService groupService;
        private IPostService postService;
        private IMessageService messageService;

        public GroupsController(IGroupService groupService, IPostService postService, IMessageService messageService)
        {
            this.groupService = groupService;
            this.postService = postService;
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var groupIdList = await groupService.GetAllGroups();
            var groupDTOList = new List<GroupDTO>();
            foreach(var groupId in groupIdList)
            {
                var groupDTO = await groupService.GetGroupData(User.Identity.Name, group.Id);
                groupList.Add(groupDTO);
            }
            ViewBag.Groups = groupList;

            return View();
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Open(int groupId)
        {
            var group = await groupService.GetGroupData(User.Identity.Name, groupId);
            ViewBag.Group = group;

            var posts = postService.GetAllGroupPosts(groupId);
            ViewBag.Posts = posts;

            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> Open(int groupId, int postId) // Имя метода не отражает сути
        {
            await groupService.AddPostToGroup(groupId, postId);

            return Redirect($"/Groups/Open/{groupId}");
        }

        [HttpPost]
        public async Task<RedirectResult> CreateGroup(GroupDTO groupDTO)
        {
            await groupService.CreateGroup(User.Identity.Name, groupDTO);

            return Redirect("/Groups/Index");
        }

        [HttpPost]
        public async Task<RedirectResult> Comment(int postId, string Text) //Менял имя первого параметра
        {
            await messageService.CreateUsersMessage(User.Identity.Name, new MessageDTO
            {
                PostId = postId,
                Text = Text,
                Date = DateTime.Now
            });

            return Redirect($"/Groups/Open/{postId}");
        }

        [HttpGet]
        public async Task<RedirectResult> DeleteComment(int postId, int messageId) //Менял имена
        {
            await messageService.DeleteUsersMessage(messageId, postId);

            return Redirect($"/Groups/Open/{postId}");
        }
    }
}