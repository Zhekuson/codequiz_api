using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<int> SendVerificationEmail(string email);
    }
}
