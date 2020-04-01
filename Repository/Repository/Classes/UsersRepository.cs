using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Classes
{
    public class UsersRepository:IUsersRepository
    {
        User GetUserById(int id)
        {
            return null;
        }
        User GetUserByEmail(string email)
        {
            return null;
        }
    }
}
