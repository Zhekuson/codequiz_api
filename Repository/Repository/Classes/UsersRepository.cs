using Domain.Models;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Classes
{
    public class UsersRepository:IUsersRepository
    {
        List<User> users = new List<User>()
        {
            new User(0,"edplyusch@edu.hse.ru","zhekuson@gmail.com"), new User(1,"ashalamov@edu.hse.ru","sasha@gmail.com")
        };
        User GetUserById(int id)
        {
            return users.Find(x => x.ID == id);
        }
        public User GetUserByEmail(string email)
        {
            return users.Find(x=>x.Email == email);
        }
    }
}
