using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project___Intro_To_Computer_Networking.ViewModel;
using Project___Intro_To_Computer_Networking.Models;
using Project___Intro_To_Computer_Networking.Dal;
using System.Net;

namespace Project___Intro_To_Computer_Networking.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            return View("home_page", new cls_oneWayFlightVM
            {
                flights = new clsFlightsDal().flights.ToList<cls_flight>()
            });
        }

        public ActionResult details(string pk)
        {
            if (string.IsNullOrEmpty(pk))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            clsFlightsDal dal = new clsFlightsDal();

            string[] flights_id = pk.Split('&');

            if (flights_id.Length > 2)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            cls_flight departure_flight = dal.flights.ToList<cls_flight>().Find(x => x.flight_identifier.Equals(flights_id[0]));

            if (departure_flight == null)
                return HttpNotFound();

            if (flights_id.Length == 1)
                return View("details", departure_flight);

            cls_flight return_flight = dal.flights.ToList<cls_flight>().Find(x => x.flight_identifier.Equals(flights_id[1]));

            if (return_flight == null)
                return HttpNotFound();

            return View("details_two_way_flight", new cls_twoWayFlight(departure_flight,return_flight));
        }
    }
}