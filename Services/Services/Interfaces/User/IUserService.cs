using Domain.Models;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        /// <summary>
        /// puts authorization 
        /// </summary>
        /// <returns>session id for client</returns>
        Task<int> PutAuthorizationCode(int code);
        /// <summary>
        /// Checks code 
        /// </summary>
        /// <returns>true if code is valid</returns>
        Task<bool> CheckCode(int code, int sessionId);
    }
}
