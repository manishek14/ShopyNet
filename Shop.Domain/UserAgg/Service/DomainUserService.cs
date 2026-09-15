using Shop.Domain.UserAgg.Repository;

namespace Shop.Domain.UserAgg.Service
{
    public class DomainUserService : IDomainUserService
    {
        private readonly IUserRepository _userRepository;

        public DomainUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool IsEmailExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _userRepository.Exists(u => u.Email == email);
        }

        public bool IsPhoneNumberExist(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            return _userRepository.Exists(u => u.PhoneNumber == phoneNumber);
        }
    }
}