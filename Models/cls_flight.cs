using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_flight
    {
        [Key]
        public string flight_identifier { get; set; }
        [Required]
        public string departure_country { get; set; }
        [Required]
        public string departure_airport { get; set; }
        [Required]
        public string landing_country { get; set; }
        [Required]
        public string landing_airport { get; set; }
        [Required]
        public string airline { get; set; }
        [Required]
        public string gate_identifier { get; set; }
        [Required]
        public DateTime departure_time { get; set; }
        [Required]
        public int airplane_identifier { get; set; }
        [Required]
        public int economy_seat_price { get; set; }
        [Required]
        public int remain_economy_seats { get; set; }

        [Required]
        public int business_seat_price { get; set; }
        [Required]
        public int remain_business_seats { get; set; }
        [Required]
        public int premium_seat_price { get; set; }
        [Required]
        public int remain_premium_seats { get; set; }

        public string get_date() { return departure_time.ToString("g"); }
        public bool is_sold_out()
        {
            return remain_economy_seats == 0 &&
                remain_business_seats == 0 &&
                remain_premium_seats == 0;
        }
        public bool is_left() { return departure_time < DateTime.Now; }

        public string generate_seat(string seat_type)
        {
            int current_row = 1;
            char current_column = 'A';
            switch (seat_type)
            {
                case "E":
                    current_row = (int)Math.Ceiling((double)remain_economy_seats / (double)8);
                    current_column = (char)(65 + ((8 - (remain_economy_seats % 8)) % 8));
                    return "ECO:" + current_column  + current_row;
                case "B":
                    current_row = (int)Math.Ceiling((double)remain_business_seats / (double)6);
                    current_column = (char)(65 + ((6 - (remain_business_seats % 6)) % 6));
                    return "BSN:" + current_column + current_row;
                case "P":
                    current_row = (int)Math.Ceiling((double)remain_premium_seats / (double)4);
                    current_column = (char)(65 + ((4 - (remain_premium_seats % 4)) % 4));
                    return "PRM:" + current_column + current_row;
                default:
                    return "UNKNOWN";
            }
        }

        public string get_departure_airport_code()
        {
            return departure_airport.Substring(0, departure_airport.IndexOf('-'));
        }
        public string get_landing_airport_code()
        {
            return landing_airport.Substring(0, landing_airport.IndexOf('-'));
        }
    }
}