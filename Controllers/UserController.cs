using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Controllers
{
    /// <summary>
    /// Controller for User related actions.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Static list of users.
        /// </summary>
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        /// <summary>
        /// GET: User
        /// Displays the list of users.
        /// </summary>
        /// <returns>A view that displays the list of users.</returns>
        public ActionResult Index()
        {
            //initilize the userlist with some data for the first time
            if (userlist.Count == 0)
            {
                userlist.Add(new User { Id = 1, Name = "John", Email = "John@github.com" });
                userlist.Add(new User { Id = 2, Name = "Smith", Email = "Smith@github.com" });
                userlist.Add(new User { Id = 3, Name = "David", Email = "David@github.com" });
            }
            return View(userlist);
        }

        /// <summary>
        /// GET: User/Details/5
        /// Displays the details of a specific user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A view that displays the details of the user.</returns>
        public ActionResult Details(int id)
        {
            var selectedUser = userlist.FirstOrDefault(u => u.Id == id);
            if (selectedUser == null)
            {
                return HttpNotFound();
            }
            return View(selectedUser);
        }

        /// <summary>
        /// GET: User/Create
        /// Displays a form for creating a new user.
        /// </summary>
        /// <returns>A view that displays the form.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: User/Create
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A redirect to the details view of the created user.</returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            userlist.Add(user);
            return RedirectToAction("Details", new { id = user.Id });
        }

        /// <summary>
        /// GET: User/Edit/5
        /// Displays a form for editing a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>A view that displays the form.</returns>
        public ActionResult Edit(int id)
        {
            var userToEdit = userlist.FirstOrDefault(u => u.Id == id);
            if (userToEdit == null)
            {
                return HttpNotFound();
            }
            return View(userToEdit);
        }

        /// <summary>
        /// POST: User/Edit/5
        /// Edits a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <param name="user">The user data to update.</param>
        /// <returns>A redirect to the details view of the edited user.</returns>
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var userToUpdate = userlist.FirstOrDefault(u => u.Id == id);
            if (userToUpdate == null)
            {
                return HttpNotFound();
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            // Update other properties as needed

            return RedirectToAction("Details", new { id = user.Id });
        }

        /// <summary>
        /// GET: User/Delete/5
        /// Displays a confirmation prompt for deleting a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A view that displays the confirmation prompt.</returns>
        public ActionResult Delete(int id)
        {
            var userToDelete = userlist.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }
            return View("Index");
        }

        /// <summary>
        /// POST: User/Delete/5
        /// Deletes a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="collection">The form collection.</param>
        /// <returns>A redirect to the index view.</returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var userToDelete = userlist.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }

            userlist.Remove(userToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string searchString)
        {
            var searchResult = userlist.Where(u => u.Name.Contains(searchString)).ToList();
            return View("Index", searchResult);
        }
    }
}
