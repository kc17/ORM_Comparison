using PetaPoco;
using PetaPoco.Core;
using PetaPocoCRUD.UnitOfWorks;
using System.Collections.Generic;

namespace PetaPocoCRUD.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        protected readonly Database Context;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            Context = unitOfWork.Context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var pd = PocoData.ForType(typeof(TEntity), new ConventionMapper());
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return Context.Query<TEntity>(sql);
        }

        public TEntity Get<TKey>(TKey id)
        {
            return Context.SingleOrDefault<TEntity>(id);
        }

        public TKey Add<TKey>(TEntity entity)
        {
            return (TKey)Context.Insert(entity);
        }

        public void Modify(TEntity entity)
        {
            Context.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Delete(entity);
        }

        public TKey InsertOrUpdate<TKey>(TEntity entity)
        {
            var pd = PocoData.ForType(typeof(TEntity), new ConventionMapper());
            var primaryKey = pd.TableInfo.PrimaryKey;

            var id = entity.GetType().GetProperty(primaryKey).GetValue(entity, null);
            var exists = Context.SingleOrDefault<TEntity>(id);

            if (!EqualityComparer<TEntity>.Default.Equals(exists, default))
            {
                Context.Update(entity);
                return (TKey)id;
            }

            return (TKey)Context.Insert(entity);
        }
    }
}