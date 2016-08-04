﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampAPI.Controllers;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Web.Http.Results;

namespace CampAPI.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private IUserService userService;

        public AccountControllerTest(IUserService userService)
        {
            this.userService = userService;
        }

        [TestMethod]
        public async void Register_SuccessfulRegistration()
        {
            var userDTO = new UserDTO
            {
                Email = "test@ukr.net",
                UserName = "testUser",
                Password = "123456&qwertY"
            };

            var controller = new AccountController(userService);

            var result = await controller.Register(userDTO);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));

            await userService.Delete(userDTO.UserName);
        }
    }
}
