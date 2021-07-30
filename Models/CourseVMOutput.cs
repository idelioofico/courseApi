using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Models
{
    public class CourseVMOutput
    {

        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Username { get; set; }

    }
}
