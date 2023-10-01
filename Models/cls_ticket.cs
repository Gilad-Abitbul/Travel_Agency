using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_ticket
    {
        [Required]
        public string passenger_name { get; set; }
        [Key, Column(Order = 0)]
        [Required]
        public string flight_identifier { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public string seat_identifier { get; set; }
        public string user_email { get; set; }
    }
}