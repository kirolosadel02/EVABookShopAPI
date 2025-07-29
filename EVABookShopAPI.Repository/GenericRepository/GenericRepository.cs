using EVABookShopAPI.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EVABookShopAPI.Repository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly EVABookShopAPIContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(EVABookShopAPIContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName)
        {
            return await _dbSet
                .Where(x => ids.Contains(EF.Property<int>(x, columnName))).Where(wherePredicate)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids)
        {
            var idName = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties.Single().Name;
            return await _dbSet
                .Where(x => ids.Contains(EF.Property<int>(x, idName))).Where(wherePredicate)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, string columnName, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            return await _dbSetQueryable
                .Where(x => ids.Contains(EF.Property<int>(x, columnName))).Where(wherePredicate)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetByIds(Expression<Func<TEntity, bool>> wherePredicate, int[] ids, List<string> include)
        {
            var idName = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties.Single().Name;
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            return await _dbSetQueryable
                .Where(x => ids.Contains(EF.Property<int>(x, idName))).Where(wherePredicate)
                .ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public async Task AddRange(IEnumerable<TEntity> entityList)
        {
            _dbSet.AddRange(entityList);
            await Task.CompletedTask;
        }

        public TEntity Delete(int id)
        {
            var entity = GetById(id);
            return _dbSet.Remove(entity).Entity;
        }

        public TEntity GetById(int id)
        {
            var result = _dbSet.Find(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public async Task UpdateRange(IEnumerable<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await Task.CompletedTask;
        }

        public async Task<TEntity> FirstOrDefault()
        {
            var result = await _dbSet.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<object>> GetData(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.Where(wherePredicate).Select(selectPredicate).ToListAsync();
            return result;
        }

        public bool ExistsById(int id)
        {
            return _dbSet.Count(e => e == GetById(id)) > 0;
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).SingleOrDefaultAsync();
            return result;
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate)
        {
            var result = _dbSet.Where(predicate).AsQueryable();
            return result;
        }

        public bool ExistsByName(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsQueryable().Count(predicate) > 0;
        }

        public TEntity GetById(int id, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            _dbSet = (DbSet<TEntity>)_dbSetQueryable;
            var result = _dbSet.Find(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IQueryable<TEntity>> GetQueryableData(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = _dbSetQueryable.Where(predicate).AsQueryable();
            return result;
        }

        public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.GroupBy(predicate).Cast<TEntity>().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> GroupBy(Expression<Func<TEntity, TEntity>> predicate)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            var result = await _dbSetQueryable.GroupBy(predicate).Cast<TEntity>().ToListAsync();
            return result;
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.Where(predicate).SingleOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll(List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.ToListAsync();
            return result;
        }

        public async Task RemoveRange(IEnumerable<TEntity> myObject)
        {
            _dbSet.RemoveRange(myObject);
            await Task.CompletedTask;
        }

        public async Task<object> FirstOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.Where(wherePredicate).Select(selectPredicate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<object> SingleOrDefault(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> selectPredicate, List<string> include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in include)
                _dbSetQueryable = _dbSetQueryable.Include(item);
            var result = await _dbSetQueryable.Where(wherePredicate).Select(selectPredicate).SingleOrDefaultAsync();
            return result;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
        {
            return disableTracking
                ? _context.Set<TEntity>().Where(predicate).AsNoTracking()
                : _context.Set<TEntity>().Where(predicate);
        }
    }
} 