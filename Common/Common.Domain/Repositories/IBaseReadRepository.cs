using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Arch.Query.Shared.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> GetByIdTrackingAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default);

        bool Exists(Expression<Func<TEntity, bool>> expression);

        TEntity Get(Guid id);

        Task<List<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}