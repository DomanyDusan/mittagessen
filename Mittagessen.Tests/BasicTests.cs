using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Controllers;
using Mittagessen.Web.Models;
using NUnit.Framework;
using StructureMap;

namespace Mittagessen.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void RegisterUserTest()
        {
            var accountController = ObjectFactory.GetInstance<AccountController>();

            var newUser = new RegistrationModel()
                              {
                                  RegistrationName = "TestUser",
                                  Email = "testuser@mittagessen.com",
                                  NewPassword = "testpassword",
                                  ConfirmPassword = "testpassword"
                              };
            accountController.Registration(newUser);
        

            var userRepository = ObjectFactory.GetInstance<IUserRepository>();
            
            //User was created
            Assert.NotNull(userRepository.GetUserByName(newUser.RegistrationName));

            //User can log in
            var login = new LoginModel()
                            {
                                LoginName = newUser.RegistrationName,
                                LoginPassword = newUser.NewPassword
                            };
            accountController.Login(login);
            Assert.IsTrue(accountController.ModelState.IsValid);
        }
    }
}
