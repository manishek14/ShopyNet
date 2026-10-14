using Common.Aplication;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IBaseCommand;

    public class ForgotPasswordCommandHandler : IBaseCommandHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ForgotPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(
            ForgotPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
                return OperationResult.Success();

            var token = GenerateToken();
            var expireAt = DateTime.Now.AddHours(2);

            user.GeneratePasswordResetToken(token, expireAt);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }

        private static string GenerateToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Replace("=", "");
        }
    }
}