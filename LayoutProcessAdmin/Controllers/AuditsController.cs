using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Auditing;
using LayoutProcessAdmin.Models.Checking;
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
            ViewBag.ListUsers = GetUsersDropDown(null);
            ViewBag.AuditTypes = GetAuditTypes();
            ViewBag.Areas = GetAreas(-1);
            ViewBag.MyChecklists = GetMyChecklists(null);
            return View();
        }

        // POST: Audits/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind(Include = "int_IdAudit,chr_Name,chr_Description,int_Type,bit_Activo")] Audit audit)
        {
            var existAudit = await db.Audits.SingleOrDefaultAsync(x => x.chr_Name == audit.chr_Name);

            if (existAudit != null)
                ModelState.AddModelError(string.Empty, "The audit Name already exists.");

            try
            {
                if (ModelState.IsValid)
                {
                    db.Audits.Add(audit);
                    await db.SaveChangesAsync();
                    return Json(new { success = true, data = new { auditId = audit.int_IdAudit } });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.ToString());
            }
            
            return Json(new { success = false, messagge = ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => x.Errors[0].ErrorMessage).ToList() });
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

        // POST: Audits/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            Audit audit = await db.Audits.FindAsync(id);
            db.Audits.Remove(audit);
            await db.SaveChangesAsync();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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

        List<SelectListItem> GetUsersDropDown(List<UsersAudits> users)
        {
            var list = db.Users.ToList();
            var listado = new List<SelectListItem>();

            foreach (var item in list)
            {
                listado.Add(new SelectListItem()
                {
                    Text = item.chr_Name + " " + item.chr_LastName,
                    Value = item.int_IdUser.ToString(),
                    Selected = IsUserSelected(item.int_IdUser, users)
                });
            }

            return listado;
        }

        bool IsUserSelected(int idUser, List<UsersAudits> users)
        {
            if (users == null)
                return false;

            foreach (var user in users)
                if (user.User.int_IdUser == idUser)
                    return true;

            return false;
        }

        List<SelectListItem> GetAreas(int idArea)
        {
            var areas = db.Areas;
            var areasList = new List<SelectListItem>();

            foreach (var area in areas)
                areasList.Add(new SelectListItem()
                {
                    Text = area.Name,
                    Value = area.int_IdArea.ToString(),
                    Selected = (area.int_IdArea == idArea) ? true : false
                });

            return areasList;
        }

        List<SelectListItem> GetMyChecklists(List<Checklist> checklistsSelected)
        {
            var checkLists = new List<SelectListItem>();
            var user = (User)Session["User"];

            var myChecklists = db.Checklists.Where(x => x.int_Owner.int_IdUser == user.int_IdUser).ToList();

            foreach (var checklist in myChecklists)
                checkLists.Add(new SelectListItem() {
                    Text = checklist.chr_Name,
                    Value = checklist.int_IdList.ToString(),
                    Selected = ChecklistsIsSelected(checklist, checklistsSelected)
                });
            
            return checkLists;
        }

        bool ChecklistsIsSelected(Checklist checklist, List<Checklist> checklistsSelected)
        {
            if (checklistsSelected == null)
                return false;

            foreach(var check in checklistsSelected)
                if (check.int_IdList == checklist.int_IdList)
                    return true;

            return false;
        }


        [HttpGet]
        public JsonResult GetSelectedDays(int period)
        {
            try
            {
                var result = db.Periods.Find(period);

                string[] days = new string[10];

                days[0] = (result.bit_Sun) ? "su" : "";
                days[1] = (result.bit_Mon) ? "mo" : "";
                days[2] = (result.bit_Tue) ? "tu" : "";
                days[3] = (result.bit_Wed) ? "we" : "";
                days[4] = (result.bit_Thu) ? "th" : "";
                days[5] = (result.bit_Sun) ? "fr" : "";
                days[6] = (result.bit_Sun) ? "su" : "";

                return Json(new { Success = true, Result = days }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Messagge = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

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
