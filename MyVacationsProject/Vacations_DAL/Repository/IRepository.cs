using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models;

namespace Vacations_DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Create(TEntity entity);

        // insert or update to be added

        /// <summary>
        /// The entity must be FOUND in the DB first
        /// </summary>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Query for Updatable (Tracked) entities
        /// </summary>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Query for read-only (Untracked) entities
        /// </summary>
        IQueryable<TEntity> AsReadOnlyQueryable();

        Task<TEntity> InsertOrUpdate(
            Expression<Func<TEntity, bool>> predicate,
            TEntity entity
        );
    }
}

