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
            if (!s_runTime.IsAdmin) {
                s_runTime.IsAdmin = Request.QueryString.ToString() == "admin";
            }

            if (s_runTime.IsAdmin)
            {
                return View("Index_admin", db);
            }
            else
            {
                return View(db);
            }
        }

        public ActionResult InsertCoin(string id)
        {
            return Content((s_runTime.Coins += db.Coins.First(o => o.Id.ToString() == id).Value).ToString());
        }

        public ActionResult GetChange()
        {
            int res = db.RunTime.Coins;
            db.RunTime.Coins = 0;
            return Content(res.ToString());
        }

        public ActionResult GetAvailableDrinks()
        {
            ArrayList list = new ArrayList();
            foreach (var o in db.Drinks.ToArray())
            {
                list.Add(new { Id = o.Id, Available = o.Price <= s_runTime.Coins && GetDrink(o.Id).Count > 0 });
            }

            return Json(
                new
                {
                    List = list
                },
                JsonRequestBehavior.AllowGet
            );
        }

        public ActionResult DisableCoin(int id)
        {
            var obj = db.Coins.FirstOrDefault(o => o.Id == id);

            if (obj == null)
            {
                throw new Exception("Object not found!");
            }

            obj.Disabled = !obj.Disabled;
            db.SaveChanges();

            return Content(String.Empty);
        }

        public ActionResult GetDisabledCoins()
        {
            ArrayList list = new ArrayList();
            foreach (var o in db.Coins.ToArray())
            {
                list.Add(new { Id = o.Id, Disabled = o.Disabled });
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
            db.RunTime.Coins -= obj.Price;

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

        // GET: Drinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Count")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                db.Drinks.Add(drink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drink);
        }

        // GET: Drinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Count")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drink drink = db.Drinks.Find(id);
            db.Drinks.Remove(drink);
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
