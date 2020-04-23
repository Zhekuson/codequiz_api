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
            await InsertUser(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await ExecuteQueryGetUserByEmail(email);
        }

        public async Task<User> GetUserByID(int id)
        {
            return await ExecuteQueryGetUserByID(id);
        }
        [QueryExecutor]
        private async Task<User> ExecuteQueryGetUserByEmail(string email)
        {

        }
        [QueryExecutor]
        private async Task<User> ExecuteQueryGetUserByID(int id)
        {

        }
        [QueryExecutor]
        private async Task InsertUser(User user)
        {

        }
    }
}
