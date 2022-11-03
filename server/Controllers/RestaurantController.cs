using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace server.Controllers
{
    public class RestaurantController : Controller
    {

        [HttpGet]
        public ActionResult All(Zero_hungerEntities1 db)
        {
            var restaurants = db.Restaurants.ToList();
            return View(restaurants);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Zero_hungerEntities1 db, Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            TempData["addMsg"] = "Restaurant added successfully";
            return RedirectToAction("Add");
        }

        [HttpGet]
        public ActionResult Delete(Zero_hungerEntities1 db, int id)
        {
            var restaurant = (
                    from row in db.Restaurants
                    where row.Id == id
                    select row
                ).SingleOrDefault();

            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
                return RedirectToAction("All");
            }

            return RedirectToAction("All");
        }
    }
}