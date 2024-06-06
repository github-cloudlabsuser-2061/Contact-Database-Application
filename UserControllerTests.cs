using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            var expectedUserList = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };

            // Act
            var result = controller.Index() as ViewResult;
            var actualUserList = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUserList.Count, actualUserList.Count);
            for (int i = 0; i < expectedUserList.Count; i++)
            {
                Assert.AreEqual(expectedUserList[i].Id, actualUserList[i].Id);
                Assert.AreEqual(expectedUserList[i].Name, actualUserList[i].Name);
                Assert.AreEqual(expectedUserList[i].Email, actualUserList[i].Email);
            }
        }

        [TestMethod]
        public void Details_ValidId_ReturnsViewWithSelectedUser()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Details(2) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Smith", model.Name);
            Assert.AreEqual("Smith@github.com", model.Email);
        }

        [TestMethod]
        public void Details_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Details(4) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_Get_ReturnsView()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_Post_AddsUserToListAndRedirectsToDetails()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 4, Name = "Test", Email = "test@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(user.Id, result.RouteValues["id"]);

            var userlist = UserController.userlist;
            Assert.IsTrue(userlist.Contains(user));
        }

        [TestMethod]
        public void Edit_ValidId_ReturnsDetailsView()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            var id = 2;
            var user = new User { Id = 2, Name = "Updated Name", Email = "updatedemail@github.com" };

            // Act
            var result = controller.Edit(id, user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual(user.Id, result.RouteValues["id"]);
        }

        [TestMethod]
        public void Edit_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            var id = 4;
            var user = new User { Id = 4, Name = "New User", Email = "newuser@github.com" };

            // Act
            var result = controller.Edit(id, user) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_ValidId_ReturnsIndexView()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            var id = 2;

            // Act
            var result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Delete_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "John@github.com" },
                new User { Id = 2, Name = "Smith", Email = "Smith@github.com" },
                new User { Id = 3, Name = "David", Email = "David@github.com" }
            };
            UserController.userlist = userlist;

            var id = 4;

            // Act
            var result = controller.Delete(id) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search_WithValidName_ReturnsFilteredUsers()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Smith", Email = "smith@example.com" };
            var user3 = new User { Id = 3, Name = "David", Email = "david@example.com" };
            UserController.userlist = new List<User> { user1, user2, user3 };

            // Act
            var result = controller.Search("John") as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(user1, model.First());
        }
    }
}
