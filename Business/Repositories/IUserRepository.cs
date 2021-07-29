﻿using product_categoryApi.Business.Entities;
using product_categoryApi.Models.VM;
using product_categoryApi.Models.VM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Business.Repositories
{

    public interface IUserRepository
    {
        void Store(User user);
        void Commit();
        User GetUser(LoginVMInput loginVMInput);
    }
}
