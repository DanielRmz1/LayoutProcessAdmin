﻿using LayoutProcessAdmin.Helpers;
using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class UsersController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Users
        public ActionResult Index()
        {
            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {

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

        List<SelectListItem> GetSelectedRoleDropDown(Role role)
        {
            var list = db.LpaRoles.ToList();
            var listado = new List<SelectListItem>();

            var group = new SelectListGroup()
            {
                Name = "Roles",
                Disabled = false
            };

            foreach (var item in list)
            {
                listado.Add(new SelectListItem()
                {
                    Text = item.chr_Name,
                    Value = item.int_IdRole.ToString(),
                    Selected = (role.int_IdRole == item.int_IdRole) ? false : true,
                    Group = group,
                    Disabled = false
                });
            }

            return listado;
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "int_IdUser,chr_Clave,chr_Name,chr_LastName,chr_Password,chr_Email,chr_Phone,UserRole")] User user)
        {
            var findUser = db.Users.Where(x => x.chr_Clave == user.chr_Clave).ToList();
               
            if (findUser.Count > 0)
            {
                ModelState.AddModelError("Chr_Clave", "El nombre de usuario ya ha sido registrado anteriormente.");
            }

            if (ModelState.IsValid)
            {
                user.chr_Password = Security.Encrypt(user.chr_Password);
                db.Users.Add(user);
                db.SaveChanges();

                var userRoles = new UserRoles();
                userRoles.int_LpaRole = db.LpaRoles.Find(user.UserRole);
                userRoles.int_User = db.Users.Find(user.int_IdUser);

                db.UserRoles.Add(userRoles);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            user.Roles = GetRolesDropDown();

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == id).ToList();

            if(users.Count > 0)
            {
                users[0].Roles = GetSelectedRoleDropDown(users[0].UserRoles[0].int_LpaRole);
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
        public ActionResult Edit([Bind(Include = "int_IdUser,chr_Clave,chr_Name,chr_LastName,chr_Password,chr_Email,chr_Phone,UserRole,UserRoles")] User user)
        {
            var editUser = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == user.int_IdUser).ToList();
            var role = db.LpaRoles.Find(user.UserRole);
            if (ModelState.IsValid)
            {
                editUser[0].UserRoles[0].int_LpaRole = role;
                db.Entry(editUser[0]).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var roles = db.LpaRoles.ToList();
            var users = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == user.int_IdUser).ToList();
            users[0].Roles = GetSelectedRoleDropDown(users[0].UserRoles[0].int_LpaRole);
            users[0].chr_Password = Security.Decrypt(users[0].chr_Password);
            return View(users[0]);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Include(x => x.UserRoles).Where(x => x.int_IdUser == id).ToList();
            db.UserRoles.Remove(user[0].UserRoles[0]);
            db.Users.Remove(user[0]);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
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