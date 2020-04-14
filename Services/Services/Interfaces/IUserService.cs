using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
