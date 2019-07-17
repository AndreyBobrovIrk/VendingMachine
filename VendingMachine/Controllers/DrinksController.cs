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

        // GET: Drinks/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult InsertCoin(string id)
        {
            return Content((s_runTime.Coins += db.Coins.First(o => o.Id.ToString() == id).Value).ToString());
        }

        public ActionResult ConfirmOrder()
        {
            foreach (var o in db.Drinks)
            {
                o.Count = Math.Max(0, o.Count - db.RunTime.SelectedDrinks.GetSelected(o.Id));
            }

            db.RunTime.Coins = db.RunTime.CoinsLimit;
            db.RunTime.SelectedDrinks.Clear();
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult GetAvailableDrinks()
        {
            ArrayList list = new ArrayList();
            foreach(var o in db.Drinks.ToArray())
            {
                list.Add(new { Id = o.Id, Available = o.Value <= s_runTime.CoinsLimit && db.GetDrinkLimit(o.Id) > 0 });
            }

            return Json(
                new
                {
                    List = list
                },
                JsonRequestBehavior.AllowGet
            );
        }

        public ActionResult GetCoinsLimit()
        {
            return Content(s_runTime.CoinsLimit.ToString());
        }

        public ActionResult GetDrinkLimit(int id)
        {
            return Json(
                new
                {
                    Id = id,
                    Count = db.GetDrinkLimit(id)
                },
                JsonRequestBehavior.AllowGet
            );

        }

        public ActionResult SelectDrink(String id)
        {
            var count = s_runTime.SelectedDrinks.AddItem(db.Drinks.First(o => o.Id.ToString() == id));

            return Json(
                new
                {
                    Id = id,
                    Count = count
                },
                JsonRequestBehavior.AllowGet
            );
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Value,Count")] Drink drink)
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
