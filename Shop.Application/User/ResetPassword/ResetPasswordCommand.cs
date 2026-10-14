using Common.Aplication;
using Common.Aplication.SecurityUtil;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.User.ResetPassword
{
    public record ResetPasswordCommand(
        string Email,
        string Token,
        string NewPassword,
        string ConfirmNewPassword
    ) : IBaseCommand;

    public class ResetPasswordCommandHandler : IBaseCommandHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
                return OperationResult.Error("کلمه‌های عبور یکسان نیستند");

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("کاربری با این ایمیل یافت نشد");

            if (!user.IsPasswordResetTokenValid(request.Token))
                return OperationResult.Error("لینک بازیابی نامعتبر یا منقضی شده است");

            var hashedPassword = Sha256Hasher.Hash(request.NewPassword);

            user.ResetPassword(hashedPassword, request.Token);

            _userRepository.Update(user);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}