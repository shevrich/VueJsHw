using People.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VueJsHw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            PeopleRepository repo = new PeopleRepository(Properties.Settings.Default.ConStr);
            repo.Add(person);
        }

        public ActionResult GetPeople()
        {
            PeopleRepository repo = new PeopleRepository(Properties.Settings.Default.ConStr);
            var result = repo.GetAll().Select(p => new
            {
                id = p.Id,
                firstName = p.FirstName,
                lastName = p.LastName,
                age = p.Age
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCars(int personId)
        {
            PeopleRepository repo = new PeopleRepository(Properties.Settings.Default.ConStr);
            var result = repo.GetCars(personId).Select(c => new
            {
                make = c.Make,
                model = c.Model,
                year = c.Year,
                personId = c.PersonId
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddCar(Car car, int PersonId)
        {
            PeopleRepository repo = new PeopleRepository(Properties.Settings.Default.ConStr);
            car.PersonId = PersonId;
            repo.AddCar(car);
        }
    }
}