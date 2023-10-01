using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_bug
    {
        public List<cls_order> user_orders = new List<cls_order>();

        public bool add_order(cls_order current_order)
        {
            cls_order dup = user_orders.Find(x => x.current_flight.flight_identifier.Equals(current_order.current_flight.flight_identifier));
            if (dup != null)
            {
                dup.economy_seats += current_order.economy_seats;
                dup.business_seats += current_order.business_seats;
                dup.premium_seats += current_order.premium_seats;
                return true;
            }
            if(current_order.economy_seats + current_order.business_seats + current_order.premium_seats == 0)
            {
                return false;
            }
            try
            {
                user_orders.Add(current_order);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int get_totalPrice()
        {
            int total = 0;
            foreach (cls_order order in user_orders)
                total += order.get_total();
            return total;
        }
    }
}