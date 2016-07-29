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
            var groupIdList = groupService.GetAllGroupsId();
            var groupDTOList = new List<GroupDTO>();
            foreach(var groupId in groupIdList)
            {
                var groupDTO = await groupService.GetGroupData(User.Identity.Name, groupId);
                groupDTOList.Add(groupDTO);
            }
            ViewBag.Groups = groupDTOList;

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

            var postsList = postService.GetAllGroupPosts(groupId);

            foreach (var post in postsList)
                post.Messages = messageService.GetAllPostMessages(post.Id);

            ViewBag.Posts = postsList;

            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> Open(int groupId, int postId, string Text) // Имя метода не отражает сути(Добавление кометрария)
        {
            await messageService.CreateUsersMessage(User.Identity.Name, new MessageDTO
            {
                PostId = postId,
                Text = Text,
                Date = DateTime.Now
            });


            return Redirect($"/Groups/Open/?groupId={groupId}");
        }

        [HttpPost]
        public async Task<RedirectResult> CreateGroup(GroupDTO groupDTO)
        {
            await groupService.CreateGroup(User.Identity.Name, groupDTO);

            return Redirect("/Groups/Index");
        }

        [HttpPost]
        public async Task<RedirectResult> AddPost(int groupId, int postId)
        {
            await groupService.AddPostToGroup(groupId, postId);

            return Redirect($"/Groups/Open/?groupId={groupId}");
        }

        [HttpGet]
        public async Task<RedirectResult> DeleteComment(int groupId, int postId, int messageId)
        {
            await messageService.DeleteUsersMessage(messageId, postId);

            return Redirect($"/Groups/Open/?groupId={groupId}");
        }
    }
}