using LayoutProcessAdmin.Models;
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
            User user = (User)Session["User"];

            if (user == null)
                return RedirectToAction("Login", "Users");

            if (!user.UserRoles[0].int_LpaRole.bit_ManageChecklist)
                return RedirectToAction("NoPermission", "Home", new { module = "Checklists Managment" });

            var myChecklists = db.Checklists.Where(x => x.int_Owner.int_IdUser == user.int_IdUser).ToList();

            return View(myChecklists);
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

            //ViewBag.CurrentUser = user;
            //ViewBag.Users = GetUsersDropDown(null);
            //ViewBag.Areas = GetAreas(-1);

            return View();
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
                //if (checklist.SelectedUsers == null)
                //    ModelState.AddModelError(string.Empty, "You have to choose at least one user to answer this checklist.");

                //if(checklist.Days == null)
                //    ModelState.AddModelError(string.Empty, "Select at least one day from the week.");

                //if(checklist.SelectedPeriod == null)
                //    ModelState.AddModelError(string.Empty, "Select the period for the checklist.");


                if (ModelState.IsValid)
                {
                    var existChl = db.Checklists.Where(x => x.chr_Clave == checklist.chr_Clave).ToList();

                    if (existChl.Count > 0)
                        return Json(-10);
                    else
                    {
                        //foreach (var item in checklist.SelectedUsers)
                        //{
                        //    db.UsersChecklists.Add(new UsersChecklist()
                        //    {
                        //        Checklist = checklist,
                        //        User = db.Users.Find(item)
                        //    });
                        //}

                        //checklist.Area = db.Areas.Find(checklist.int_Area);

                        User user = (User)Session["User"];
                        checklist.int_Owner = db.Users.Find(user.int_IdUser);

                        //var period = new Period()
                        //{
                        //    bit_Sun = FindSelectedDay(checklist.Days, "su"),
                        //    bit_Mon = FindSelectedDay(checklist.Days, "mo"),
                        //    bit_Tue = FindSelectedDay(checklist.Days, "tu"),
                        //    bit_Wed = FindSelectedDay(checklist.Days, "we"),
                        //    bit_Thu = FindSelectedDay(checklist.Days, "th"),
                        //    bit_Fri = FindSelectedDay(checklist.Days, "fr"),
                        //    bit_Sat = FindSelectedDay(checklist.Days, "sa"),
                        //    chr_RepeatPeriod = checklist.SelectedPeriod
                        //};

                        //checklist.int_Period = period;
                    
                        //db.Periods.Add(period);
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
            var users = db.Users.Find(user.int_IdUser);
            Checklist checklist = db.Checklists.SingleOrDefault(x => x.int_IdList == id);

            if (checklist == null)
            {
                return HttpNotFound();
            }
            //checklist.id_Period = checklist.int_Period.int_IdPeriod;
            //ViewBag.Periods = GetPeriods(checklist.int_Period.chr_RepeatPeriod);
            //ViewBag.Days = GetDays();
            //ViewBag.Users = GetUsersDropDown(checklist.UsersChecklists);
            //ViewBag.Areas = GetAreas(-1);
            return View(checklist);
        }

        // POST: Checklists/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "int_IdList,chr_Clave,chr_Name,chr_Description,bit_Activo, Days, SelectedPeriod, SelectedUsers,int_Area, id_Period")] Checklist checklist)
        {
            //if (checklist.SelectedUsers == null)
            //    ModelState.AddModelError(string.Empty, "You have to choose at least one user to answer this checklist.");

            //if (checklist.Days == null)
            //    ModelState.AddModelError(string.Empty, "Select at least one day from the week.");

            //if (checklist.SelectedPeriod == null)
            //    ModelState.AddModelError(string.Empty, "Select the period for the checklist.");

            if (ModelState.IsValid)
            {
                try
                {
                    //var result = db.UsersChecklists.Where(x => x.Checklist.int_IdList == checklist.int_IdList).ToList();

                    //foreach (var userChecklist in result)
                    //    db.UsersChecklists.Remove(userChecklist);

                    //foreach (var item in checklist.SelectedUsers)
                    //{
                    //    db.UsersChecklists.Add(new UsersChecklist()
                    //    {
                    //        Checklist = checklist,
                    //        User = db.Users.Find(item)
                    //    });
                    //}
                    //var period = db.Periods.Find(checklist.id_Period);

                    //period.bit_Sun = FindSelectedDay(checklist.Days, "su");
                    //period.bit_Mon = FindSelectedDay(checklist.Days, "mo");
                    //period.bit_Tue = FindSelectedDay(checklist.Days, "tu");
                    //period.bit_Wed = FindSelectedDay(checklist.Days, "we");
                    //period.bit_Thu = FindSelectedDay(checklist.Days, "th");
                    //period.bit_Fri = FindSelectedDay(checklist.Days, "fr");
                    //period.bit_Sat = FindSelectedDay(checklist.Days, "sa");
                    //period.chr_RepeatPeriod = checklist.SelectedPeriod;
                    
                    //db.Entry(period).State = EntityState.Modified;
                    db.Entry(checklist).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Success = true});
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
            var errorMessagge = ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => x.Errors[0].ErrorMessage).ToList();
            return Json(new { Success = false, Message = errorMessagge});
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
            var checklist = await db.Checklists.Include(x => x.Questions).SingleOrDefaultAsync(p => p.int_IdList == id);
            
            if(checklist != null)
            {
                //foreach (var child in checklist.UsersChecklists.ToList())
                //    db.UsersChecklists.Remove(child);

                //db.Periods.Remove(checklist.int_Period);

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
