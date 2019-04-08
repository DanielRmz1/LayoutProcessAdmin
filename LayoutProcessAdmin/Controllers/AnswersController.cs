using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Checking;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class AnswersController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Answers
        public ActionResult Index()
        {
            return View(db.Answers.ToList());
        }

        // POST: Answers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(string description, string variable, int id_question, string lowerLimit, string upperLimit)
        {
            try
            {
                var answer = db.Answers.Add(new Answer()
                {
                    chr_Description = description,
                    chr_Variable = variable,
                    int_Question = db.Questions.Find(id_question),
                    dbl_LowerLimit = double.Parse(lowerLimit),
                    dbl_UpperLimit = double.Parse(upperLimit)
                });
                db.SaveChanges();
                return Json(new { Success = true, id = answer.int_IdAnswer }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Answers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "int_IdAnswer,chr_Description,chr_Entry,bit_Selected")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(answer);
        }
        
        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
