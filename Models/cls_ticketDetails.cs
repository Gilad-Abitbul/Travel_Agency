using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_ticketDetails
    {
        public string passenger_name { get; set; }
        public string seat_identifier { get; set; }
        public cls_flight flight { get; set; }
        public string user_email { get; set; }
    }
}