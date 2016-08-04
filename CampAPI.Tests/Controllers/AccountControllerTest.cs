using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampAPI.Controllers;
using CampBusinessLogic.Interfaces;

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
        public void RegisterNewUser()
        {
            var controller = new AccountController(userService);
        }
    }
}
