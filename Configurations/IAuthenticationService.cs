using product_categoryApi.Models.VM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserVMOutput loginVMOutput);

    }
}
