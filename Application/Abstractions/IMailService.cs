using Application.Dtos;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IMailService
    {
         Task<string> EmailVerificationTemplate(EmailSenderDetails model);

    }
}

