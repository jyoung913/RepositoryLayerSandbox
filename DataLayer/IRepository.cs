using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRepository<TEntity>
    {
        void Insert(TEntity t);
        IEnumerable<TEntity> Read(); 
        void Update(TEntity t);
        void Delete(TEntity t);
        TEntity Find(int id);

        // there should really be funcs here as well for the find
        // https://www.youtube.com/watch?v=5gyldL1PF6U
    }
}
