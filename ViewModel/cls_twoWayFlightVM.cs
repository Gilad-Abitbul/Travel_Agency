﻿using Project___Intro_To_Computer_Networking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.ViewModel
{
    public class cls_twoWayFlightVM : cls_flightVM
    {
        public List<cls_twoWayFlight> flights { get; set; }

        public override bool IsOneWayFlight()
        {
            return false;
        }
    }
}