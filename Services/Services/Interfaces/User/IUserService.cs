﻿using Domain.Models;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}
