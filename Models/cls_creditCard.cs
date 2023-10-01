using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_creditCard
    {
        [Key]
        [MaxLength(30, ErrorMessage = "The maximum length of an email address is 30 characters"), MinLength(10, ErrorMessage = "The minimum length of an email address is 10 characters")]
        [Required(ErrorMessage = "Required field")]
        [RegularExpression(
                    "(\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.(com|co\\.il|org))$",
                    ErrorMessage = "Invalid email address")]
        public string email { set; get; }

        [Required]
        [RegularExpression("^[0-9]{8,9}$")]
        public string card_holder_id { set; get; }
        [Required]
        [MaxLength(45), MinLength(5)]
        [RegularExpression("^(?<firstchar>[A-Za-z])((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-][A-Za-z])|(?<spaces> [A-Za-z]))*$")]
        public string card_holder_name { set; get; }
        public DateTime expiration_date { set; get; }
        [Required]
        [RegularExpression(@"^(4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)")]
        public string card_number { set; get; }
        [Required]
        [RegularExpression("^[0-9]{3,4}$")]
        public string card_CVC { set; get; }

    }
}