using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{ //generic constraint =>generic kısıtlama where T:class =>t burda class referans tip olabilir yani
  //,Entity eklediğimizde ya IEntity yada ondan implement edilmiş bir nesne olabilir
  // eğer sadece IEntity den türetilmiş olan classs istiyorsak new() yapabiliriz
  // çünkü IEntity new lenemez ve IEntity koyamayız ondan newlenmişleri koyabilir
    public interface IEntityRepository<T> where T : class,IEntity,new ()
    {
        //Expression filtreme yapsını kullanırken kullandığımız yapı
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T,bool>> filter);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
