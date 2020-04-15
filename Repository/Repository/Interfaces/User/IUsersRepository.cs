using Domain.Models;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        Task<User> GetUserByID(int id);
    }
}
