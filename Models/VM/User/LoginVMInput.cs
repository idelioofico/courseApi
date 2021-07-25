using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Models.VM
{
    public class LoginVMInput
    {
        [Required(ErrorMessage ="Username field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password field is required")]
        public string Password { get; set; }
    }
}
