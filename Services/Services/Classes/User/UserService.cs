using Domain.Models;
using Domain.Models.Users;
using Repository.Repository.Classes;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class UserService : IUserService
    {
        public readonly IUsersRepository usersRepository;
        public UserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public async Task AddUser(User user)
        {
            await usersRepository.AddUser(user);
        }

        public async Task UpdateUser(User user)
        {
            throw new Exception();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await usersRepository.GetUserByEmail(email);
        }
    }
}
