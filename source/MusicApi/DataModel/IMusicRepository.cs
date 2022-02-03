using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MusicApi.DataModel
{
    public interface IMusicRepository : IDisposable
    {
        IQueryable<T> Set<T>() where T : class;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null) where T : class;
        Task SaveAsync();
    }

    public class MusicRepository : IMusicRepository
    {
        private readonly MusicDataContext context;

        public MusicRepository(MusicDataContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Set<T>() where T : class => context.Set<T>();

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            return query;
        }
        
        public Task SaveAsync() => context.SaveChangesAsync();
        
        public void Dispose() => context?.Dispose();
    }
}