using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Models.VM
{
    public class RegisterVMInput
    {
        [Required(ErrorMessage ="Username field is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password field is required")]
        public string Password { get; set; }

    }
}
