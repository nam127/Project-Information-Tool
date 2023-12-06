using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects;
using PIMTool.Core.Interfaces.Helpers;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;
using PIMTool.Helpers;

namespace PIMTool.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly PimContext _pimContext;
        protected readonly DbSet<T> _set;
        
        public Repository(PimContext pimContext)
        {
            _pimContext = pimContext;
            _set = _pimContext.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _set.Where(x => true);
        }

        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Get().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _set.AddRangeAsync(entities, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(entity, cancellationToken);
        }

        public void Delete(params T[] entities)
        {
            _set.RemoveRange(entities);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _pimContext.SaveChangesAsync(cancellationToken);
        }

        public void  Update(T entity)
        {
            _pimContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public void DeleteAsync(T entity)
        {
            _set.Remove(entity);
        }
        
    }
}