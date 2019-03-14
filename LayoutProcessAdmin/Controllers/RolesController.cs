using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class RolesController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.LpaRoles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.LpaRoles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "int_IdRole,chr_Name,chr_Description,bit_ManageUsers,bit_ManageGlobalUsers,bit_ManageChecklist,bit_FillChecklists")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.LpaRoles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.LpaRoles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "int_IdRole,chr_Name,chr_Description,bit_ManageUsers,bit_ManageGlobalUsers,bit_ManageChecklist,bit_FillChecklists")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var roles = db.LpaRoles.Include(x => x.UserRoles).Where(x => x.int_IdRole == id).ToList();

            if(roles[0].UserRoles.Count > 0)
                db.UserRoles.Remove(roles[0].UserRoles[0]); 
            db.LpaRoles.Remove(roles[0]);
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
