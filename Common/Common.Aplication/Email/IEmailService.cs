using System.Threading;
using System.Threading.Tasks;

namespace Common.Application.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string body,
            CancellationToken cancellationToken = default);
    }
}
