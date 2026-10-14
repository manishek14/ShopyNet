using Common.Aplication;
using Shop.Domain.UserAgg.Repository;
using System.Security.Cryptography;

namespace Shop.Application.User.GenerateEmailToken
{
    public class GenerateEmailConfirmationTokenCommandHandler
        : IBaseCommandHandler<GenerateEmailConfirmationTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public GenerateEmailConfirmationTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(
            GenerateEmailConfirmationTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("User not found.");

            var token = GenerateToken();
            var expireAt = DateTime.Now.AddHours(24);

            user.GenerateEmailConfirmationToken(token, expireAt);

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