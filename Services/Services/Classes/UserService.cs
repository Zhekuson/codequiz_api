using Domain.Models;
using Repository.Repository.Classes;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.Classes
{
    public class UserService : IUserService
    {
        public readonly IUsersRepository usersRepository;
        public UserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public void AddUser(User user)
        {
            usersRepository.AddUser(user);
        }

        public User GetUserByEmail(string email)
        {
            return usersRepository.GetUserByEmail(email);
        }

        public User GetUserById(int id)
        {
            return usersRepository.GetUserByID(id);
        }
    }
}
