using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_order
    {
        public int economy_seats { get; set; }
        public int business_seats { get; set; }
        public int premium_seats { get; set; }
        public cls_flight current_flight { get; set; }
        public int get_total_for_product(string identifier)
        {
            switch(identifier)
            {
                case "E":
                    return economy_seats * current_flight.economy_seat_price;
                case "B":
                    return business_seats * current_flight.business_seat_price;
                case "P":
                    return premium_seats * current_flight.premium_seat_price;
                default:
                    return 0;
            }
        }
        public int get_total()
        {
            return get_total_for_product("E") + get_total_for_product("B") + get_total_for_product("P");
        }
    }
}