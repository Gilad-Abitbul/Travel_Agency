using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Models
{
    public class cls_user
    {
        [Required(ErrorMessage = "Required field")]
        [RegularExpression("^[A-Z]((?<= )[A-Z]|[a-z ]){2,20}$",
            ErrorMessage = "First name must start with an uppercase letter and be at least 3 and up to 30 alphabetic characters long")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Required field")]
        [RegularExpression("^[A-Z]((?<= )[A-Z]|[a-z ]){2,20}$",
            ErrorMessage = "The lastName must start with an uppercase letter and be at least 3\nand up to 30 alphabetic characters long")]
        public string lastName { get; set; }

        [Key]
        [MaxLength(30, ErrorMessage = "The maximum length of an email address is 30 characters"), MinLength(10, ErrorMessage = "The minimum length of an email address is 10 characters")]
        [Required(ErrorMessage = "Required field")]
        [RegularExpression(
            "(\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.(com|co\\.il|org))$",
            ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,20}$",
            ErrorMessage = "password must contain at least eight characters, at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string password { get; set; }

        public bool is_admin { get; set; }
    }
}