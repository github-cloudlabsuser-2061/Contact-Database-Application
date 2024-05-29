using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Razor;

namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            return View(userlist);

        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        // GET: User/Create
        public ActionResult Create()
        {
            User u = new User();
            return View(u);

        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            userlist.Add(user);
            user.Id = userlist.Select(u => u.Id).Max() + 1;

            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            User u = userlist.FirstOrDefault(x => x.Id == id);
            if (u == null)
            {
                return HttpNotFound();
            }
            u.Name = user.Name;
            u.Email = user.Email;
            return RedirectToAction("Index");
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            //get the user from the list
            User user = userlist.FirstOrDefault(u => u.Id == id);
            //if the user is not found, return HttpNotFound
            if (user == null)
            {
                return HttpNotFound();
            }
            // else delete the user from the list
            userlist.Remove(user);
            //redirect to the index page
            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            //find the user from the list
            User user = userlist.FirstOrDefault(u => u.Id == id);
            //if the user is not found, return HttpNotFound
            if (user == null)
            {
                return HttpNotFound();
            }
            // else delete the user from the list
            userlist.Remove(user);
            //redirect to the index page
            return RedirectToAction("Index");
        }
    }
}
