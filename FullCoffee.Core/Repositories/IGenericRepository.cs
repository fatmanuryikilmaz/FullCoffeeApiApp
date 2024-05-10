using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);//datayı aldıktan sonra belki sorgu isteyebilirim o yüzden IEnumarable kullanmadım
        Task<T> GetByIdAsync(int id);   
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity); // efcore takip ettiği product ın sadece state ini değiştirir uzun sürmez o yüzden async yok 
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
