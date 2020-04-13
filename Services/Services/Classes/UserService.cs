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
        public User GetUserByEmail(string email)
        {
            //User user = new User();
            // user.Email = email;
            // return user;
            return usersRepository.GetUserByEmail(email);
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
