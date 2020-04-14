using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string GoogleEmail { get; set; }
        public IEnumerable<TagStats> TagStats { get; set; }
        public IEnumerable<QuizResult> QuizResults { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public User()
        {

        }
        public User(int id, string email, string googleEmail):this(email,googleEmail)
        {
            ID = id;
        }
        public User(string email, string googleEmail)
        {
            Email = email;
            GoogleEmail = googleEmail;
        }
    }
}
