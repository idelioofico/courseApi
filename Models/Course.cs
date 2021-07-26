using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
