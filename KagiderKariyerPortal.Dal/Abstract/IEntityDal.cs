using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Dal.Abstract
{
   public interface IEntityDal<T>
   {
       void Add(T entity);

       T Get(T entity);

       List<T> GetList();

       void Del(T entity);

       void Update(T entity);
   }


}
