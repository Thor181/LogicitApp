using LogicitApp.Data.Models;
using LogicitApp.Data.Models.Interfaces;
using LogicitApp.Shared.Results;
using System.Linq.Expressions;

namespace LogicitApp.Data.DataLogic
{
    public class BaseLogic : IDisposable
    {
        private bool _disposed = false;

        public LogicitAppDbContext DbContext;

        public BaseLogic()
        {
            DbContext = new LogicitAppDbContext();
        }

        public virtual BaseResult Add<T>(T entity) where T : class, IDbEntity
        {
            var result = new BaseResult();
            try
            {
                DbContext.Set<T>().Add(entity);
                DbContext.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"При добавлении сущности возникла ошибка | {e.Message} | {e.InnerException}";
                return result;
            }
        }

        public virtual void MarkAsAdded<T>(T entity) where T : class, IDbEntity
        {
            DbContext.Add(entity);
        }

        public virtual IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, IDbEntity
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll<T>() where T : class, IDbEntity
        {
            return DbContext.Set<T>();
        }

        public virtual T Get<T>(long id) where T : class, IDbEntity
        {
            return DbContext.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public virtual BaseResult RemoveRange<T>(params long[] ids) where T : class, IDbEntity
        {
            var result = new BaseResult();

            try
            {
                var entities = GetAll<T>(x => ids.Contains(x.Id));

                DbContext.RemoveRange(entities);
                DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"При удалении сущностей возникла ошибка | {e.Message} | {e.InnerException}";
            }

            return result;
        }

        public virtual BaseResult Remove<T>(long id) where T : class, IDbEntity
        {
            var result = new BaseResult();

            try
            {
                var entity = Get<T>(id);

                DbContext.Remove(entity);
                DbContext.SaveChanges();

                result.Message = "Сущность успешно удалена";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"При удалении сущности возникла ошибка | {e.Message} | {e.InnerException}";
            }

            return result;
        }

        public virtual BaseResult Update<T>(T entity) where T : class, IDbEntity
        {
            var result = new BaseResult() {  Message = "Успешно обновлено" };

            try
            {
                DbContext.Update(entity);
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Во время обновления сущности возникла ошибка | {e.Message} | {e.InnerException}";
            }

            return result;
        }

        public BaseResult SaveChanges()
        {
            var result = new BaseResult();

            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }

            return result;
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    _disposed = true;
                }
            }
        }
        #endregion
    }
}
