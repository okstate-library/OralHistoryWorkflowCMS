using LinqKit;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EntityData
{
    /// <summary>
    /// Defiens the generic methods for the generic repository class.
    /// </summary>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="EntityData.IGenericRepository{T}" />
    public abstract class GenericRepository<C, T> :
       IGenericRepository<T>
          where T : class
          where C : System.Data.Entity.DbContext, new()
    {

        /// <summary>
        /// The _entities
        /// </summary>
        private C _entities = new C();

        /// <summary>
        /// The database context for the repository
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        /// <summary>
        /// Gets all entities matching the predicate
        /// </summary>
        /// <returns>
        /// All entities matching the predicate.
        /// </returns>
        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        /// <summary>
        /// Set based on where condition
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>
        /// The records matching the given condition
        /// </returns>
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().AsExpandable().Where(predicate);
            return query;
        }

        /// <summary>
        /// Returns the first entity that matches the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>
        /// An entity matching the predicate
        /// </returns>
        public T First(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().First(predicate);
        }

        /// <summary>
        /// Returns the first entity that matches the predicate else null
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>
        /// An entity matching the predicate else null
        /// </returns>
        public T FirstToDelete(Expression<Func<T, bool>> predicate)
        {
            T t = _entities.Set<T>().FirstOrDefault(predicate);
            _entities.Entry(t).State = System.Data.Entity.EntityState.Detached;

            return t;// _entities.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Returns the first entity that matches the predicate else null
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>
        /// An entity matching the predicate else null
        /// </returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Adds a given entity to the context
        /// </summary>
        /// <param name="entity">The entity to add to the context</param>
        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        /// <summary>
        /// Deletes a given entity from the context
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            _entities.Set<T>().Remove(entity);

            //Save();
        }

        /// <summary>
        /// Deletes a given entity from the context
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// Return No of records that deleted.
        /// </returns>
        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);

            int deleteCount = query.Count();

            foreach (T item in query)
            {
                _entities.Set<T>().Remove(item);
            }

            //Save();

            return deleteCount;
        }

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Edit(T entity)
        {
           _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>
        /// Returns the id of the saved entry.
        /// </returns>
        public virtual int Save()
        {
            return _entities.SaveChanges();
        }

        /// <summary>
        /// Counts the specified predicate.
        /// </summary>
        /// <returns>
        /// Retrns the record count.
        /// </returns>
        public int Count()
        {
            return _entities.Set<T>().Count();
        }

        /// <summary>
        /// Counts the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// Returns the record count for the given predicate.
        /// </returns>
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Count(predicate);
        }
    }
}
