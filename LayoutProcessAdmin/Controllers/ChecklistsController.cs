﻿using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Checking;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class ChecklistsController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Checklists
        public ActionResult Index()
        {
            return View(db.Checklists.ToList());
        }

        // GET: Checklists/Details/5
        public ActionResult Details(int? id)
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageChecklist)
                return RedirectToAction("NoPermission", "Home", new { module = "Checklists Managment" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = db.Checklists.Find(id);
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // GET: Checklists/Create
        public ActionResult Create()
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageChecklist)
                return RedirectToAction("NoPermission", "Home", new { module = "Checklists Managment" });

            ViewBag.CurrentUser = user;
            ViewBag.Users = GetUsersDropDown();
            ViewBag.Areas = GetAreas();

            return View();
        }

        List<SelectListItem> GetUsersDropDown()
        {
            var list = db.Users.ToList();
            var listado = new List<SelectListItem>();

            foreach (var item in list)
            {
                listado.Add(new SelectListItem()
                {
                    Text = item.chr_Name + " " + item.chr_LastName,
                    Value = item.int_IdUser.ToString()
                });
            }

            return listado;
        }

        // POST: Checklists/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "int_IdList,chr_Clave,chr_Name,chr_Description,bit_Activo, Days, SelectedPeriod, SelectedUsers,int_Area, int_Owner")] Checklist checklist)
        {
            try
            {
                if (checklist.SelectedUsers == null)
                    ModelState.AddModelError(string.Empty, "You have to choose at least one user to answer this checklist.");

                if(checklist.Days == null)
                    ModelState.AddModelError(string.Empty, "Select at least one day from the week.");

                if(checklist.SelectedPeriod == null)
                    ModelState.AddModelError(string.Empty, "Select the period for the checklist.");


                if (ModelState.IsValid)
                {
                    var existChl = db.Checklists.Where(x => x.chr_Clave == checklist.chr_Clave).ToList();

                    if (existChl.Count > 0)
                        return Json(-10);
                    else
                    {
                        foreach (var item in checklist.SelectedUsers)
                        {
                            db.UsersChecklists.Add(new UsersChecklist()
                            {
                                Checklist = checklist,
                                Users = db.Users.Find(item)
                            });
                        }

                        checklist.Area = db.Areas.Find(checklist.int_Area);

                        User user = (User)Session["User"];
                        checklist.int_Owner = db.Users.Find(user.int_IdUser);

                        var period = new Period()
                        {
                            bit_Sun = FindSelectedDay(checklist.Days, "su"),
                            bit_Mon = FindSelectedDay(checklist.Days, "mo"),
                            bit_Tue = FindSelectedDay(checklist.Days, "tu"),
                            bit_Wed = FindSelectedDay(checklist.Days, "we"),
                            bit_Thu = FindSelectedDay(checklist.Days, "th"),
                            bit_Fri = FindSelectedDay(checklist.Days, "fr"),
                            bit_Sat = FindSelectedDay(checklist.Days, "sa"),
                            chr_RepeatPeriod = checklist.SelectedPeriod
                        };

                        checklist.int_Period = period;
                    
                        db.Periods.Add(period);
                        db.Checklists.Add(checklist);
                        db.SaveChanges();
                    }

                    return Json(checklist.int_IdList);
                }
                else
                {
                    var errorMessagge = ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => x.Errors[0].ErrorMessage).ToList();
                    return Json(errorMessagge);
                }
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

                return Json(new { Success = false, Message = sb.ToString() });
            }

        }

        List<SelectListItem> GetAreas()
        {
            var areas = db.Areas;
            var areasList = new List<SelectListItem>();

            foreach (var area in areas)
                areasList.Add(new SelectListItem()
                {
                    Text = area.Name,
                    Value = area.int_IdArea.ToString()
                });

            return areasList;
        } 

        bool FindSelectedDay(string[] days, string current)
        {
            foreach (var day in days)
                if (day == current)
                    return true;

            return false;
        }

        // GET: Checklists/Edit/5
        public ActionResult Edit(int? id)
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageChecklist)
                return RedirectToAction("NoPermission", "Home", new { module = "Checklists Managment" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = db.Checklists.Find(id);
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // POST: Checklists/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "int_IdList,chr_Clave,chr_Name,chr_Description,bit_Activo")] Checklist checklist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(checklist).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Exception e)
                {
                    return View(e);
                }
                
            }
            return View(checklist);
        }

        // GET: Checklists/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageChecklist)
                return RedirectToAction("NoPermission", "Home", new { module = "Checklists Managment" });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checklist checklist = db.Checklists.Find(id);
            if (checklist == null)
            {
                return HttpNotFound();
            }
            return View(checklist);
        }

        // POST: Checklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var checklist = await db.Checklists.Include(x => x.UsersChecklists).Include(x => x.Questions).Include(x => x.int_Period).SingleOrDefaultAsync(p => p.int_IdList == id);
            
            if(checklist != null)
            {
                foreach (var child in checklist.UsersChecklists.ToList())
                    db.UsersChecklists.Remove(child);

                db.Periods.Remove(checklist.int_Period);

                foreach (var child in checklist.Questions.ToList())
                {
                    RemoveAnswers(child.int_IdQuestion);
                    db.Questions.Remove(child);
                }

                db.Checklists.Remove(checklist);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private void RemoveAnswers(int id)
        {
            var answers = db.Answers.Where(x => x.int_Question.int_IdQuestion == id).ToList();

            if (answers.Count > 0)
                foreach (var answer in answers)
                    db.Answers.Remove(answer);
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
