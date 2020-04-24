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

        public async Task<bool> CheckCode(int code, int sessionId)
        {
            return await usersRepository.CheckCode(code, sessionId); 
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await usersRepository.GetUserByEmail(email);
        }

        public async Task<int> PutAuthorizationCode(int code)
        {
            return await usersRepository.PutAuthorizationCode(code);
        }
    }
}
