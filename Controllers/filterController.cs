using Project___Intro_To_Computer_Networking.Dal;
using Project___Intro_To_Computer_Networking.Models;
using Project___Intro_To_Computer_Networking.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project___Intro_To_Computer_Networking.Controllers
{
    public class filterController : Controller
    {
        // GET: filter
        [HttpPost]
        public ActionResult SearchFlight()
        {
            DateTime SV_DepartureDate;
            bool is_valid_departure_date = DateTime.TryParse(Request.Form["SV_DepartureDate"],
                out SV_DepartureDate);

            string SV_DepartureCountry = Request.Form["SV_DepartureCountry"];
            string SV_DestinationCountry = Request.Form["SV_DestinationCountry"];
            bool is_two_way = Request.Form["trip-type"] == "two_way";
            clsFlightsDal dal = new clsFlightsDal();

            List<cls_flight> DepartureFlight = dal.flights.ToList<cls_flight>();
            DepartureFlight = SearchFlights(DepartureFlight, SV_DepartureCountry, SV_DestinationCountry);
            if (is_valid_departure_date)
                DepartureFlight = SearchByDate(DepartureFlight, SV_DepartureDate);

            if (!is_two_way)
                return View("~/Views/home/home_page.cshtml", new cls_oneWayFlightVM
                {
                    flights = DepartureFlight
                });

            List<cls_flight> ReturnFlight = dal.flights.ToList<cls_flight>();
            DateTime SV_ReturnDate;
            bool is_valid_return_date = DateTime.TryParse(Request.Form["SV_ReturnDate"],
                out SV_ReturnDate);

            ReturnFlight = SearchFlights(ReturnFlight, SV_DestinationCountry, SV_DepartureCountry);
            if (is_valid_return_date)
                ReturnFlight = SearchByDate(ReturnFlight, SV_ReturnDate);

            List<cls_twoWayFlight> TWFL = new List<cls_twoWayFlight>();
            foreach (cls_flight DF in DepartureFlight)
            {
                foreach (cls_flight RF in ReturnFlight)
                {
                    if (DF.departure_time.Date < RF.departure_time.Date &&
                        DF.departure_country.Equals(RF.landing_country) &&
                        DF.landing_country.Equals(RF.departure_country))
                    {
                        TWFL.Add(new cls_twoWayFlight(DF, RF));
                    }
                }
            }
            return View("~/Views/home/home_page.cshtml", new cls_twoWayFlightVM
            {
                flights = TWFL
            });
        }

        private List<cls_flight> SearchFlights(List<cls_flight> filter_flights, string SV_DepartureCountry, string SV_DestinationCountry)
        {
            filter_flights = filter_flights.FindAll(x => x.departure_country.IndexOf(SV_DepartureCountry, StringComparison.OrdinalIgnoreCase) >= 0);
            filter_flights = filter_flights.FindAll(x => x.landing_country.IndexOf(SV_DestinationCountry, StringComparison.OrdinalIgnoreCase) >= 0);
            return filter_flights;
        }

        private List<cls_flight> SearchByDate(List<cls_flight> filter_flights, DateTime SV_DepartureDate)
        {
            return filter_flights.FindAll(x => SV_DepartureDate.Date == x.departure_time.Date);
        }
    }
}