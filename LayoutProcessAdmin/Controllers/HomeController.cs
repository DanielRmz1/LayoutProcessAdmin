using LayoutProcessAdmin.Models;
using LayoutProcessAdmin.Models.Account;
using LayoutProcessAdmin.Models.Calendar;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace LayoutProcessAdmin.Controllers
{
    public class HomeController : Controller
    {
        private LayoutProcessContext db = new LayoutProcessContext();

        public async Task<ActionResult> Index()
        {
            if(Session["User"] == null)
                return RedirectToAction("Login", "Users");

            var user = (User) Session["User"];
            
            var eventsTable = new List<EventsTable>();
            
            var events = await db.Events.Include(x => x.AuditConfig).Where(x => x.User.int_IdUser == user.int_IdUser).ToListAsync();
            
            foreach (var e in events)
            {
                var auditConfig = db.AuditConfigs.Include(x=>x.Area).Include(x=>x.Audit).Include(x=>x.Checklist).Where(x => x.int_IdAuditConfig == e.AuditConfig.int_IdAuditConfig).First();

                eventsTable.Add(new EventsTable()
                {
                    Audit = auditConfig.Audit.chr_Name,
                    Checklist = auditConfig.Checklist.chr_Name,
                    Date = e.dte_ScheduleDate,
                    EventId = e.int_IdEvent,
                    State = e.int_State,
                    Area = auditConfig.Area.chr_Clave
                });
            }
            
            return View(eventsTable);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoPermission(string module)
        {
            ViewBag.Module = module;

            return View();
        }
    }
}