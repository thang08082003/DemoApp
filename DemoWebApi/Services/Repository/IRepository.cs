using DemoShared.Constants;
using DemoWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi.Services.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> DeleteItems(List<T> entity);
        Task<bool> AddItems(List<T> entity);
        Task<bool> UpdateItems(List<T> entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private DbSet<T> entities;
        public Repository(IDbContextFactory<DataContext> contextFactory, IServiceScopeFactory scopeFactory)
        {
            _contextFactory = contextFactory;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Add(T entity)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;
                await executionStrategy.Execute(async () =>
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            entities = _context.Set<T>();
                            await entities.AddAsync(entity);
                            await _context.SaveChangesAsync();
                            trans.Commit();
                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<bool> Update(T item)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;

                await executionStrategy.Execute(async () =>
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            entities = _context.Set<T>();
                            entities.Entry(item).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            trans.Commit();
                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }

            }
        }

        /// <summary>
        /// get All employee
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                entities = _context.Set<T>();
                var ret = await entities.ToListAsync();
                return ret;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(T entity)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;
                await executionStrategy.Execute(async () =>
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            entities = _context.Set<T>();
                            entities.Remove(entity);
                            await _context.SaveChangesAsync();
                            trans.Commit();

                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// Delete a record list
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItems(List<T> items)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;
                await executionStrategy.Execute(async () =>
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            entities = _context.Set<T>();
                            foreach (var item in items)
                            {
                                _context.Entry(item).State = EntityState.Detached;
                            }

                            entities.RemoveRange(items);
                            await _context.SaveChangesAsync();
                            trans.Commit();

                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<bool> AddItems(List<T> items)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;
                try
                {
                    await executionStrategy.Execute(async () =>
                    {
                        using (var trans = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                entities = _context.Set<T>();
                                await entities.AddRangeAsync(items);
                                await _context.SaveChangesAsync();
                                trans.Commit();

                                ret = true;
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                ret = false;
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    return false;
                }
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }

            }
        }

        public async Task<bool> UpdateItems(List<T> items)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var executionStrategy = _context.Database.CreateExecutionStrategy();
                bool ret = false;
                await executionStrategy.Execute(async () =>
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            entities = _context.Set<T>();
                            foreach (var item in items)
                            {
                                entities.Entry(item).State = EntityState.Modified;
                            }
                            await _context.SaveChangesAsync();
                            trans.Commit();

                            ret = true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            ret = false;
                        }
                    }
                });
                if (ret)
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
