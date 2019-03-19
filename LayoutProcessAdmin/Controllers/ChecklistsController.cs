using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Checking;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            return View();
        }

        // POST: Checklists/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "int_IdList,chr_Clave,chr_Name,chr_Description,bit_Activo, Days, SelectedPeriod")] Checklist checklist)
        {
            if (ModelState.IsValid)
            {
                var existChl = db.Checklists.Where(x => x.chr_Clave == checklist.chr_Clave).ToList();
                
                if(existChl.Count > 0)
                {
                    return Json(-10);
                }
                else
                {
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

        // GET: Checklists/Edit/5
        public ActionResult Edit(int? id)
        {
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
                db.Entry(checklist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checklist);
        }

        // GET: Checklists/Delete/5
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
            Checklist checklist = db.Checklists.Find(id);
            db.Checklists.Remove(checklist);
            db.SaveChanges();
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
