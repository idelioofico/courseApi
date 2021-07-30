using courseApi.Business.Entities;
using courseApi.Models.VM;
using courseApi.Models.VM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Business.Repositories
{

    public interface IUserRepository
    {
        void Store(User user);
        void Commit();
        User GetUser(LoginVMInput loginVMInput);
    }
}
