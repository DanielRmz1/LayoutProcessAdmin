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

        // GET: AuditConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditConfigs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "int_IdAuditConfig,int_Level")] AuditConfig auditConfig)
        {
            if (ModelState.IsValid)
            {
                db.AuditConfigs.Add(auditConfig);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(auditConfig);
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
    }
}
