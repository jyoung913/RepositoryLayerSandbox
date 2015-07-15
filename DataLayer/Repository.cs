using System;
using System.Collections.Generic;
using System.Data;

namespace DataLayer
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
    {
        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var items = new List<TEntity>();
                while (reader.Read())
                {
                    var item = CreateEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        protected abstract void Map(IDataRecord record, TEntity entity);

        protected abstract TEntity CreateEntity();

        public abstract void Insert(TEntity t);

        public abstract IEnumerable<TEntity> Read();

        public abstract void Update(TEntity t);

        public abstract void Delete(TEntity t);

        public abstract TEntity Find(int id);
    }
}
