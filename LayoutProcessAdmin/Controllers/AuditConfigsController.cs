using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Auditing;

namespace LayoutProcessAdmin.Controllers
{
    public class AuditConfigsController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: AuditConfigs
        public async Task<ActionResult> Index()
        {
            return View(await db.AuditConfigs.ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            var table = new List<LayersTable>();
            var configs = await db.AuditConfigs.Include(x => x.Area).Include(x => x.int_Period).Include(x => x.Checklist).Where(x => x.Audit.int_IdAudit == id).ToListAsync();

            foreach (var config in configs)
                table.Add(new LayersTable() {
                    Area = config.Area.chr_Clave,
                    Checklist = config.Checklist.chr_Name,
                    Id = config.int_IdAuditConfig,
                    Level = config.int_Level,
                    Period = config.int_Period.chr_RepeatPeriod
                });
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        // GET: AuditConfigs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditConfig auditConfig = await db.AuditConfigs.FindAsync(id);
            if (auditConfig == null)
            {
                return HttpNotFound();
            }
            return View(auditConfig);
        }

        // POST: AuditConfigs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(int? checklist, string period, string[] days, int[] users, int? area, int level, int audit)
        {
            var auditConfig = new AuditConfig();

            List<string> errors = new List<string>();

            if (checklist == 0)
                errors.Add("The checklist cannot be blank");

            if (period == "")
                errors.Add("The period cannot be blank");

            if (days[0] == "")
                errors.Add("It is necessary to select at least a one day");

            if (users.Length == 0)
                errors.Add("It is necessary to select at least a one user");

            if (area == null)
                errors.Add("The area cannot be blank");

            if (errors.Count == 0)
            {
                var newPeriod = new Period()
                {
                    bit_Sun = FindSelectedDay(days, "su"),
                    bit_Mon = FindSelectedDay(days, "mo"),
                    bit_Tue = FindSelectedDay(days, "tu"),
                    bit_Wed = FindSelectedDay(days, "we"),
                    bit_Thu = FindSelectedDay(days, "th"),
                    bit_Fri = FindSelectedDay(days, "fr"),
                    bit_Sat = FindSelectedDay(days, "sa"),
                    chr_RepeatPeriod = period
                };

                db.Periods.Add(newPeriod);

                var area1 = db.Areas.Find(area);
                var audit1 = db.Audits.Find(audit);
                var checklist1 = db.Checklists.Find(checklist);

                var config = new AuditConfig()
                {
                    Area = area1,
                    Audit = audit1,
                    int_Level = level,
                    int_Period = newPeriod,
                    Checklist = checklist1,
                    dte_LastDateCreated = new DateTime(2000, 1, 1)
                };

                db.AuditConfigs.Add(config);

                for (var i = 0; i < users.Length; i++)
                {
                    var thisUser = db.Users.Find(users[i]);
                    db.UsersAudits.Add(new UsersAudits()
                    {
                        AuditConfig = config,
                        User = thisUser
                    });
                }
                    
                await db.SaveChangesAsync();

                return Json(new { success = true });

            }
            
            return Json(new { success = false, messagge = errors });
        }

        // GET: AuditConfigs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditConfig auditConfig = await db.AuditConfigs.FindAsync(id);
            if (auditConfig == null)
            {
                return HttpNotFound();
            }
            return View(auditConfig);
        }

        // POST: AuditConfigs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "int_IdAuditConfig,int_Level")] AuditConfig auditConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditConfig).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(auditConfig);
        }

        // GET: AuditConfigs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditConfig auditConfig = await db.AuditConfigs.FindAsync(id);
            if (auditConfig == null)
            {
                return HttpNotFound();
            }
            return View(auditConfig);
        }

        // POST: AuditConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AuditConfig auditConfig = await db.AuditConfigs.FindAsync(id);
            db.AuditConfigs.Remove(auditConfig);
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
        
        bool FindSelectedDay(string[] days, string current)
        {
            foreach (var day in days)
                if (day == current)
                    return true;

            return false;
        }
    }
}
