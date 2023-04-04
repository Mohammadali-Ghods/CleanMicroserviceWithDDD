using BaseDomain;
using MongoDB.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseDomain.Entity
    {
        public async Task<IEnumerable<T>> GetAll()
        {
            return await DB.Find<T>().ExecuteAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await DB.Find<T>().OneAsync(id);
        }

        public async Task Insert(T entity)
        {
            await entity.SaveAsync();
        }

        public async Task Update(T entity)
        {
            await DB.Update<T>()
                    .MatchID(entity.ID)
                    .ModifyExcept(x => new { x.ID }, entity)
                    .ExecuteAsync();
        }

        public async Task Delete(T entity)
        {
            await DB.DeleteAsync<T>(entity.ID);

        }
    }

}
