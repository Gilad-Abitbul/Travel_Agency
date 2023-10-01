using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_twoWayFlight
    {
        public cls_flight DepartureFlight;
        public cls_flight ReturnFlight;

        public cls_twoWayFlight(cls_flight DepartureFlight, cls_flight ReturnFlight)
        {
            this.DepartureFlight = DepartureFlight;
            this.ReturnFlight = ReturnFlight;
        }
    }
}