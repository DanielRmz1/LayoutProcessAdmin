using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Auditing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class AuditsController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Audits
        public async Task<ActionResult> Index()
        {
            return View(await db.Audits.ToListAsync());
        }

        // GET: Audits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = await db.Audits.Include(x => x.AuditConfigs).SingleOrDefaultAsync(x => x.int_IdAudit == id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // GET: Audits/Create
        public ActionResult Create()
        {
            ViewBag.AuditTypes = GetAuditTypes();
            return View();
        }

        // POST: Audits/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "int_IdAudit,chr_Name,chr_Description,int_Type")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                db.Audits.Add(audit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(audit);
        }

        // GET: Audits/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = await db.Audits.FindAsync(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // POST: Audits/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "int_IdAudit,chr_Name,chr_Description,int_Type")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(audit);
        }

        // GET: Audits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = await db.Audits.FindAsync(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // POST: Audits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Audit audit = await db.Audits.FindAsync(id);
            db.Audits.Remove(audit);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        List<SelectListItem> GetAuditTypes(string selectedType = "-1")
        {
            return new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Auditoria",
                    Value = "0",
                    Selected = (selectedType == "0") ? true : false
                },
                new SelectListItem()
                {
                    Text = "Encuesta",
                    Value = "1",
                    Selected = (selectedType == "0") ? true : false
                }
            };
        }

        //List<SelectListItem> GetUsersDropDown(List<UsersAudits> users)
        //{
        //    var list = db.Users.ToList();
        //    var listado = new List<SelectListItem>();

        //    foreach (var item in list)
        //    {
        //        listado.Add(new SelectListItem()
        //        {
        //            Text = item.chr_Name + " " + item.chr_LastName,
        //            Value = item.int_IdUser.ToString(),
        //            Selected = IsUserSelected(item.int_IdUser, users)
        //        });
        //    }

        //    return listado;
        //}

        //bool IsUserSelected(int idUser, List<UsersAudits> users)
        //{
        //    if (users == null)
        //        return false;

        //    foreach (var user in users)
        //        if (user.User.int_IdUser == idUser)
        //            return true;

        //    return false;
        //}

        //[HttpGet]
        //public JsonResult GetSelectedArea(int checklist)
        //{
        //    try
        //    {
        //        var check = db.Audits.Include(x => x.Area.SingleOrDefault(x => x.int_IdList == checklist);
        //        return Json(new { Success = true, Result = check.Area.int_IdArea }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Success = false, Messagge = e.ToString() }, JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}
