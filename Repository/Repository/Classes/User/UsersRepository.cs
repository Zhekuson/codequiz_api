using Domain.Models;
using Domain.Models.Users;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes
{
    public class UsersRepository:EntityRepository ,IUsersRepository
    {
        public UsersRepository()
        {
        }

        public async Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
