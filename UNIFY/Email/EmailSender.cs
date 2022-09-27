using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using static UNIFY.Email.EmailDto;

namespace UNIFY.Email
{
    public class EmailSender : IEmailSender
    {
        public Task<bool> SendEmail(EmailRequestModel email)
        {
            throw new NotImplementedException();
        }
    }
}
