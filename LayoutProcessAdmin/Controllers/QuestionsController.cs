using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Checking;

namespace LayoutProcessAdmin.Controllers
{
    public class QuestionsController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        // GET: Questions
        [HttpGet]
        public JsonResult Get(int id)
        {
            return Json(db.Questions.Where(x => x.int_Checklist.int_IdList == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(string description, string type, bool singleAnswer, string formula, int id_checklist)
        {
            try
            {
                var newQuestion = db.Questions.Add(new Question()
                {
                    bit_SingleAnswer = singleAnswer,
                    chr_Description = description,
                    chr_Type = type,
                    chr_Formula = formula,
                    int_Checklist = db.Checklists.Find(id_checklist)
                });

                db.SaveChanges();

                return Json(new { Success = true, id = newQuestion.int_IdQuestion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.ToString() });
            }
            
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "int_IdQuestion,chr_Description,chr_Type,bit_SingleAnswer")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            try
            {
                Question question = await db.Questions.Include(v => v.Answers).SingleOrDefaultAsync(v => v.int_IdQuestion == id);

                while (question.Answers.Count > 0)
                    db.Answers.Remove(question.Answers[0]);

                db.Questions.Remove(question);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.ToString() });
            }
            
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
