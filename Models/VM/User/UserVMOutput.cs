using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Models.VM.User
{
    public class UserVMOutput
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
