using System.Linq.Expressions;
using CarSharing.Context;
using CarSharing.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Создаёт запись в бд.
        /// </summary>
        /// <param name="entity">Пользовательский ввод.</param>
        /// <returns>Результат операции.</returns>
        public virtual async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        /// <summary>
        /// Удаляет запись из бд.
        /// </summary>
        /// <param name="entity">Удаляемая запись.</param>
        /// <returns>Результат операции.</returns>
        public virtual async Task Delete(T entity)
        {
            await Task.Run(() =>
                _context.Set<T>().Remove(entity));
        }
        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }
        /// <summary>
        /// Возвращает записи по условию.
        /// </summary>
        /// <param name="expression">Условие, по которому фильтруются записи.</param>
        /// <returns>Результат запроса.</returns>
        public virtual async Task<List<T>> GetByExpression(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .AsQueryable()
                .ToListAsync();
        }
        /// <summary>
        /// Обновляет данные записи.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task Update(T entity)
        {
            return Task.Run(() => _context.Set<T>().Update(entity));
        }
    }
}
