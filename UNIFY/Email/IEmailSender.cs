using System.Threading.Tasks;
using static UNIFY.Email.EmailDto;

namespace UNIFY.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailRequestModel email);
    }
}
