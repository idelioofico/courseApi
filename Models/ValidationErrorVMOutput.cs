using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Models
{
    public class ValidationErrorVMOutput
    {

        public IEnumerable<string> Errors { get; set; }
        public ValidationErrorVMOutput(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }
    }
}
