using Common.Aplication;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.User.AddToken
{
    // Handler
    public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult> Handle(
            AddUserTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return OperationResult.NotFound("User not found.");

            var userToken = new UserToken(
                request.UserId,
                request.HashJwtToken,
                request.HashRefreshToken,
                request.TokenExpireDate,
                request.RefreshTokenExpireDate,
                request.Device
            );

            await _userRepository.AddTokenAsync(userToken, cancellationToken);
            await _userRepository.SaveAsync(cancellationToken);

            return OperationResult.Success();
        }
    }
}