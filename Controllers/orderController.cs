using Project___Intro_To_Computer_Networking.Dal;
using Project___Intro_To_Computer_Networking.Models;
using Project___Intro_To_Computer_Networking.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Project___Intro_To_Computer_Networking.Controllers
{
    public class orderController : Controller
    {
        public ActionResult place_order(string pk)
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


            if (Session["BUG"] == null) { Session["BUG"] = new cls_bug(); }
            bool status = ((cls_bug)Session["BUG"]).add_order(get_departure_order(departure_flight));

            if (flights_id.Length == 2)
            {
                cls_flight return_flight = dal.flights.ToList<cls_flight>().Find(x => x.flight_identifier.Equals(flights_id[1]));
                if (return_flight == null)
                    return HttpNotFound();
                status = status && ((cls_bug)Session["BUG"]).add_order(get_return_order(return_flight));
            }

            if (status)
            {
                TempData["make-order-message"] = "Your order has been successfully added to the cart";
                TempData["make-order-message-color"] = "green";
            }
            else
            {
                TempData["make-order-message"] = "An error occurred while adding your order to the cart";
                TempData["make-order-message-color"] = "red";
            }
            return RedirectToAction("index", "home");
        }

        private cls_order get_departure_order(cls_flight current_flight)
        {
            return new cls_order
            {
                current_flight = current_flight,
                economy_seats = Request.Form["IV_DepartureEconomyAmount"] == null ? 0 : Request.Form["IV_DepartureEconomyAmount"].AsInt(),
                business_seats = Request.Form["IV_DepartureBusinessAmount"] == null ? 0 : Request.Form["IV_DepartureBusinessAmount"].AsInt(),
                premium_seats = Request.Form["IV_DeparturePremiumAmount"] == null ? 0 : Request.Form["IV_DeparturePremiumAmount"].AsInt(),
            }; 
        }

        private cls_order get_return_order(cls_flight current_flight)
        {
            return new cls_order
            {
                current_flight = current_flight,
                economy_seats = Request.Form["IV_ReturnEconomyAmount"] == null ? 0 : Request.Form["IV_ReturnEconomyAmount"].AsInt(),
                business_seats = Request.Form["IV_ReturnBusinessAmount"] == null ? 0 : Request.Form["IV_ReturnBusinessAmount"].AsInt(),
                premium_seats = Request.Form["IV_ReturnPremiumAmount"] == null ? 0 : Request.Form["IV_ReturnPremiumAmount"].AsInt(),
            };
        }


        public ActionResult check_out()
        {
            return View("check_out");
        }

        public ActionResult make_payment()
        {
            string m = Request.Form["UI_CardExpirationDateM"];
            string y = Request.Form["UI_CardExpirationDateY"];
            

            cls_bug current_bug = (cls_bug)Session["BUG"];
            if (current_bug == null)
            {
                return RedirectToAction("index", "home");
            }
            List<cls_ticketDetails> tickets = getTicket(current_bug);
            saveTicket(tickets);

            bool saveCard = Request.Form["CB_SaveCardDetails"] == null ? false : true;
            if (saveCard)
            {
                clsCreditCardsDal dal = new clsCreditCardsDal();
                cls_creditCard card = getCreditCard();
                dal.cards.Add(card);
                dal.SaveChanges();
            }
            Session["BUG"] = null;
            return View("show_tickets", new cls_ticketDetailsVM
            {
                tickets = tickets
            }) ;
        }

        private List<cls_ticketDetails> getTicket(cls_bug current_bug)
        { 
            clsFlightsDal flight_dal = new clsFlightsDal();
            List<cls_ticketDetails> ticket = new List<cls_ticketDetails>();
            cls_user currentUser = (cls_user)Session["currentUser"];

            string user_email = currentUser == null ? null : currentUser.email;
            
            foreach (cls_order order in current_bug.user_orders)
            {
                flight_dal.Entry(order.current_flight).State = EntityState.Modified;

                for (int i = 1; i <= order.economy_seats; i++)
                {
                    string passenger_name = Request.Form[order.current_flight.flight_identifier + "-E#" + i].ToString();
                    string seat_identifier = order.current_flight.generate_seat("E");
                    order.current_flight.remain_economy_seats--;
                    ticket.Add(new cls_ticketDetails
                    {
                        flight = order.current_flight,
                        seat_identifier = seat_identifier,
                        user_email = user_email,
                        passenger_name = passenger_name
                    }) ;
                }

                for (int i = 1; i <= order.business_seats; i++)
                {
                    string passenger_name = Request.Form[order.current_flight.flight_identifier + "-B#" + i].ToString();
                    string seat_identifier = order.current_flight.generate_seat("B");
                    order.current_flight.remain_business_seats--;
                    ticket.Add(new cls_ticketDetails
                    {
                        flight = order.current_flight,
                        seat_identifier = seat_identifier,
                        user_email = user_email,
                        passenger_name = passenger_name
                    });
                }

                for (int i = 1; i <= order.premium_seats; i++)
                {
                    string passenger_name = Request.Form[order.current_flight.flight_identifier + "-P#" + i].ToString();
                    string seat_identifier = order.current_flight.generate_seat("P");
                    order.current_flight.remain_premium_seats--;
                    ticket.Add(new cls_ticketDetails
                    {
                        flight = order.current_flight,
                        seat_identifier = seat_identifier,
                        user_email = user_email,
                        passenger_name = passenger_name
                    });
                }
                flight_dal.SaveChanges();
            }
            return ticket;
        }

        private void saveTicket(List<cls_ticketDetails> tickets)
        {
            clsTicketsDal dal = new clsTicketsDal();
            List<cls_ticket> new_tickets = new List<cls_ticket>();
            foreach(cls_ticketDetails ticket in tickets)
            {
                new_tickets.Add(new cls_ticket
                {
                    flight_identifier = ticket.flight.flight_identifier,
                    passenger_name = ticket.passenger_name,
                    seat_identifier = ticket.seat_identifier,
                    user_email = ticket.user_email
                });
            }
            dal.tickets.AddRange(new_tickets);
            dal.SaveChanges();
        }

        public ActionResult remove_item(string seq)
        {
            try
            {
                cls_bug current_bug = (cls_bug)Session["BUG"];
                current_bug.user_orders.Remove(current_bug.user_orders[int.Parse(seq)]);
            }
            catch { }
            return View("check_out");
        }

        private cls_creditCard getCreditCard()
        {
            return new cls_creditCard
            {
                email = Request.Form["UI_CardEmail"],
                card_holder_id = Request.Form["UI_CardHolderID"],
                card_holder_name = Request.Form["UI_CardHolderName"],
                expiration_date = new DateTime(int.Parse(Request.Form["UI_CardExpirationDateY"]), int.Parse(Request.Form["UI_CardExpirationDateM"]), 1),
                card_number = Request.Form["UI_CardNumber"],
                card_CVC = Request.Form["UI_CardCVC"]
            };
        }
    }
}