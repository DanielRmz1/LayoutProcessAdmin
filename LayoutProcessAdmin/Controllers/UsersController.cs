using LayoutProcessAdmin.Helpers;
using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class UsersController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Users
        public ActionResult Index()
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.LpaRoles.ToList();
            var users = db.Users.Include(x=>x.UserRoles).Where(x=>x.int_IdUser == id).ToList();
            if (users == null || users.Count == 0)
            {
                return HttpNotFound();
            }
            return View(users[0]);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            User principal = new User();
            principal.Roles = GetRolesDropDown();

            return View(principal);
        }

        List<SelectListItem> GetRolesDropDown()
        {
            var list = db.LpaRoles.ToList();
            var listado = new List<SelectListItem>();

            foreach(var item in list)
            {
                listado.Add(new SelectListItem()
                {
                    Text = item.chr_Name,
                    Value = item.int_IdRole.ToString()                   
                });
            }

            return listado;
        }
       

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsyn([Bind(Include = "int_IdUser,chr_Clave,chr_Name,chr_LastName,chr_Password,chr_Email,chr_Phone,UserRole, chr_ConfirmPassword")] User user)
        {
            User loggedUSer = (User)Session["User"];

            if (loggedUSer == null)
                return RedirectToAction("Login", "Users");

            if (!loggedUSer.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            var findUser = db.Users.Where(x => x.chr_Clave == user.chr_Clave).ToList();
               
            if (findUser.Count > 0)
                ModelState.AddModelError("Chr_Clave", "This username already exists, try with another one.");
            

            if(user.chr_Password != user.chr_ConfirmPassword)
                ModelState.AddModelError("Chr_ConfirmPassword", "The passwords you have written does not match.");

            if (ModelState.IsValid)
            {
                user.chr_Password = Security.Encrypt(user.chr_Password);
                db.Users.Add(user);
                db.SaveChanges();

                var userRoles = new UserRoles();
                userRoles.int_LpaRole = db.LpaRoles.Find(user.UserRole);
                userRoles.int_User = db.Users.Find(user.int_IdUser);

                db.UserRoles.Add(userRoles);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            user.Roles = GetRolesDropDown();

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            User loggedUSer = (User)Session["User"];

            if (loggedUSer == null)
                return RedirectToAction("Login", "Users");

            if (!loggedUSer.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == id).ToList();

            if(users.Count > 0)
            {
                users[0].Roles = GetRolesDropDown();

                if (users == null)
                {
                    return HttpNotFound();
                }

                users[0].chr_Password = Security.Decrypt(users[0].chr_Password);

                return View(users[0]);
            }

            return RedirectToAction("Index");
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "int_IdUser,chr_Clave,chr_Name,chr_LastName,chr_Password,chr_ConfirmPassword,chr_Email,chr_Phone,UserRole,UserRoles")] User user)
        {
            User loggedUSer = (User)Session["User"];

            if (loggedUSer == null)
                return RedirectToAction("Login", "Users");

            if (!loggedUSer.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            if (user.chr_Password != user.chr_ConfirmPassword)
                ModelState.AddModelError("Chr_ConfirmPassword", "The passwords you have written does not match.");

            if (ModelState.IsValid)
            {
                try
                {
                    var editUser = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == user.int_IdUser).ToList();
                    var role = db.LpaRoles.Find(user.UserRole);

                    if (editUser[0].UserRoles.Count > 0)
                        editUser[0].UserRoles[0].int_LpaRole = role;
                    else
                    {
                        var userRoles = new UserRoles();
                        userRoles.int_LpaRole = role;
                        userRoles.int_User = editUser[0];
                        db.UserRoles.Add(userRoles);
                    }

                    editUser[0].chr_Password = Security.Encrypt(user.chr_Password);
                    editUser[0].chr_ConfirmPassword = editUser[0].chr_Password;
                    editUser[0].chr_Clave = user.chr_Clave;
                    editUser[0].chr_Email = user.chr_Email;
                    editUser[0].chr_LastName = user.chr_LastName;
                    editUser[0].chr_Name = user.chr_Name;
                    editUser[0].chr_Phone = user.chr_Phone;

                    db.Entry(editUser[0]).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in e.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    var _oroles = db.LpaRoles.ToList();
                    var _ousers = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == user.int_IdUser).ToList();
                    _ousers[0].Roles = GetRolesDropDown();
                    _ousers[0].chr_Password = Security.Decrypt(_ousers[0].chr_Password);
                    ModelState.AddModelError(string.Empty, sb.ToString());
                    return View(_ousers[0]);
                }

            }

            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == user.int_IdUser).ToList();
            users[0].Roles = GetRolesDropDown();
            users[0].chr_Password = Security.Decrypt(users[0].chr_Password);
            return View(users[0]);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User loggedUSer = (User)Session["User"];

            if (loggedUSer == null)
                return RedirectToAction("Login", "Users");

            if (!loggedUSer.UserRoles[0].int_LpaRole.bit_ManageUsers)
                return RedirectToAction("NoPermission", "Home", new { module = "Users Managment" });

            var user = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == id).ToList();
            if (user[0].UserRoles.Count > 0)
                db.UserRoles.Remove(user[0].UserRoles[0]);
            db.Users.Remove(user[0]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                db.LpaRoles.ToList();
                var user = db.Users.Include(x => x.UserRoles).Where(x => x.chr_Clave == login.Chr_Clave).ToList();

                if (user.Count > 0)
                {
                    if (Security.Decrypt(user[0].chr_Password) == login.Chr_Password)
                    {
                        Session["User"] = user[0];
                        return RedirectToAction("Index", "Home");
                    }else
                        ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
                }
                else
                    ModelState.AddModelError(string.Empty, "El usuario no existe.");
            }

            return View(login);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("User");

            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
