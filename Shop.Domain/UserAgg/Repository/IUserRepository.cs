using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shop.Domain.UserAgg.Repository
{
    public interface IUserRepository
    {
        User GetById(Guid id);
        User GetByPhoneNumber(string phoneNumber);
        User GetByEmail(string email);
        List<User> GetAll();

        void Add(User user);
        void Update(User user);
        void Delete(User user);

        bool Exists(Expression<Func<User, bool>> predicate);
        void SaveChanges();
    }
}