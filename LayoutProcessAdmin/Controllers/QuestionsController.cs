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
            var questions = db.Questions.Where(x => x.int_Checklist.int_IdList == id).ToList();
            return Json(questions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestion(int id)
        {
            try
            {
                var question = db.Questions.Find(id);
                var answers = await db.Answers.Where(x => x.int_Question.int_IdQuestion == question.int_IdQuestion).Select(i => new { i.chr_Description, i.chr_Variable, i.int_IdAnswer, i.dbl_LowerLimit, i.dbl_UpperLimit }).ToListAsync();
                return Json(new { Success = true, Quest = question, Answs = answers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestions(int id)
        {
            var questions = await db.Questions.Include(x => x.Answers).Where(x => x.int_Checklist.int_IdList == id).ToListAsync();

            foreach (var question in questions)
                foreach (var answer in question.Answers)
                    answer.int_Question = null;

            return Json(new { success = true, data = questions }, JsonRequestBehavior.AllowGet);
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
        public JsonResult Create(string description, string type, bool singleAnswer, string formula, int id_checklist, int id_question)
        {
            try
            {
                if(id_question == -1)
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
                else
                {
                    var editQuestion = db.Questions.Include(x => x.Answers).SingleOrDefault(x => x.int_IdQuestion == id_question);
                    editQuestion.chr_Description = description;
                    editQuestion.chr_Type = type;
                    editQuestion.bit_SingleAnswer = singleAnswer;
                    editQuestion.chr_Formula = formula;

                    while (editQuestion.Answers.Count > 0)
                        db.Answers.Remove(editQuestion.Answers[0]);

                    db.Entry(editQuestion).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Success = true, id = id_question }, JsonRequestBehavior.AllowGet);
                }
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
