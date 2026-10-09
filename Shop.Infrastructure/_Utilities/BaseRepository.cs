using Clean_Arch.Query.Shared.Repository;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure._Utilities
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
    {
        private readonly ShopContext _shopContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ShopContext shopContext)
        {
            _shopContext = shopContext
                ?? throw new ArgumentNullException(nameof(shopContext));
            _dbSet = shopContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<TEntity> GetByIdTrackingAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public TEntity Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            return _dbSet.Find(id);
        }

        public async Task<List<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(expression, cancellationToken);
        }

        public async Task AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Delete();   
            _dbSet.Update(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                entity.Delete();
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task<int> SaveAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _shopContext.SaveChangesAsync(cancellationToken);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
            {
                try
                {
                    var entries = _shopContext.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                        .Select(e => new
                        {
                            Type = e.Entity?.GetType().Name,
                            PrimaryKey = e.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue
                        })
                        .ToList();

                    var details = entries.Count == 0
                        ? "(no tracked entries)"
                        : string.Join("; ", entries.Select(x => $"{x.Type}:{x.PrimaryKey}"));

                    // Minimal diagnostic output to help troubleshooting. Uses Console so it works without extra DI/config.
                    Console.WriteLine($"[Concurrency] SaveAsync failed. Tracked entries: {details}. Exception: {ex.Message}");

                    if (entries.Count > 0)
                    {
                        // If we have at least one entry, throw a domain ConcurrencyException for the first one to provide clearer message upstream.
                        var first = entries[0];
                        throw new Common.Domain.Exceptions.ConcurrencyException(first.Type ?? "Entity", first.PrimaryKey ?? "unknown");
                    }
                }
                catch (Exception wrapEx)
                {
                    // If enriching fails, throw a RepositoryException with original exception as inner.
                    throw new Common.Domain.Exceptions.RepositoryException("Concurrency conflict occurred while saving changes.", ex);
                }

                // If we reach here, rethrow original to satisfy compiler (shouldn't reach because we throw above)
                throw;
            }
        }
    }
}