using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class DrinksController : Controller
    {
        public DrinksController()
        {

        }

        private static RunTime s_runTime = new RunTime();
        private VendingMachineContext db = new VendingMachineContext(s_runTime);

        // GET: Drinks
        public ActionResult Index()
        {
            s_runTime.IsAdmin = Request.QueryString.ToString() == "admin";
            return View(db);
        }

        public ActionResult InsertCoin(string id)
        {
            return Content((s_runTime.Coins += db.Coins.First(o => o.Id.ToString() == id).Value).ToString());
        }

        public ActionResult GetAvailableDrinks()
        {
            ArrayList list = new ArrayList();
            foreach (var o in db.Drinks.ToArray())
            {
                list.Add(new { Id = o.Id, Available = o.Value <= s_runTime.Coins && GetDrink(o.Id).Count > 0 });
            }

            return Json(
                new
                {
                    List = list
                },
                JsonRequestBehavior.AllowGet
            );
        }

        private Drink GetDrink(int id)
        {
            var obj = db.Drinks.FirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                throw new Exception("Object not found!");
            }

            return obj;
        }

        public ActionResult SelectDrink(int id)
        {
            var obj = GetDrink(id);
            obj.Count = Math.Max(0, --obj.Count);
            db.RunTime.Coins -= obj.Value;

            db.SaveChanges();

            return Json(
                new
                {
                    Id = obj.Id,
                    Count = obj.Count,
                    Coins = db.RunTime.Coins
                },
                JsonRequestBehavior.AllowGet
            );
        }
    }
}
