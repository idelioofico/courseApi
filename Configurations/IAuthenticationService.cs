using courseApi.Models.VM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserVMOutput loginVMOutput);

    }
}
