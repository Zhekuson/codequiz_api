﻿using Domain.Models.Users;

using Repository;
using Repository.Repository.Exceptions;
using Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes
{
    public class UsersRepository: EntityRepository, IUsersRepository
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
            User user = new User();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM [dbo].[User] u WHERE u.email = {email}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Email = reader.GetString(reader.GetOrdinal("email"));
                        user.ID = reader.GetInt32(reader.GetOrdinal("id")); 
                        user.GoogleUser = new GoogleUser();
                    }
                    else
                    {
                        throw new UserNotFoundException("User was not found");
                    }
                }
                command = CreateCommand($"SELECT * FROM [dbo].[GoogleUser] gu WHERE gu.user_id = {user.ID}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.GoogleUser.Email = reader.GetString(reader.GetOrdinal("email"));
                        user.GoogleUser.Name = reader.GetString(reader.GetOrdinal("name"));
                        user.GoogleUser.Surname = reader.GetString(reader.GetOrdinal("surname"));
                    }
                }
            }
            return user;
        }
        [QueryExecutor]
        private async Task<User> ExecuteQueryGetUserByID(int id)
        {
            User user = new User();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand($"SELECT * FROM [dbo].[User] u WHERE u.id = {id}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Email = reader.GetString(reader.GetOrdinal("email"));
                        user.ID = id;
                        user.GoogleUser = new GoogleUser();
                    }
                    else
                    {
                        throw new UserNotFoundException("User was not found");
                    }
                }
                command = CreateCommand($"SELECT * FROM [dbo].[GoogleUser] gu WHERE gu.user_id = {id}", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.GoogleUser.Email = reader.GetString(reader.GetOrdinal("email"));
                        user.GoogleUser.Name = reader.GetString(reader.GetOrdinal("name"));
                        user.GoogleUser.Surname = reader.GetString(reader.GetOrdinal("surname"));
                    }
                }
            }
            return user;
        }
        [QueryExecutor]
        private async Task InsertUser(User user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand(
                    $"INSERT INTO [dbo].[User](email) VALUES ({user.Email})", connection);
                command.ExecuteNonQuery();
            }
            await InsertGoogleUser(user.GoogleUser);
        }

        private async Task InsertGoogleUser(GoogleUser googleUser)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = CreateCommand(
                    $"INSERT INTO[dbo].[GoogleUser](user_id, email, name, surname)" +
                    $" VALUES({googleUser.UserID}, {googleUser.Email}," +
                    $" {googleUser.Name}, {googleUser.Surname})", connection);
                command.ExecuteNonQuery();
            }
            
        }
    }
}
