using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Interfaces
{
    public interface IUsersRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        User GetUserByID(int id);
    }
}
