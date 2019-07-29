using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StockDataBL
{
    public static class DbSetExtensions
    {
        //        static void InsertIfNotExists<TEntity>(this DbSet<TEntity> set, TEntity entity, Expression<Func<TEntity, bool>> predicate)
        //            where TEntity : class
        //        {
        //            if (entity == null) throw new ArgumentNullException(nameof(entity));
        //
        //            if (set.Any(predicate))
        //            {
        //                return;
        //            }
        //
        //            set.Add(entity);
        //        }
        //        static void InsertIfNotExists<TEntity>(this DbSet<TEntity> set,IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
        //            where TEntity : class
        //
        //        {
        //            foreach (var entity in entities)
        //            {
        //                set.InsertIfNotExists(entity, (ent) =>);
        //            }
        //        }

        public static void InsertIfNotExists(this DbSet<dane_gieldowe> set, dane_gieldowe entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (set.Any(x => x.data == entity.data && x.nazwa == entity.nazwa))
            {
                return;
            }

            set.Add(entity);
        }
        public static void InsertIfNotExists(this DbSet<dane_gieldowe> set, IEnumerable<dane_gieldowe> entities)
        {
            foreach (var entity in entities)
            {
                set.InsertIfNotExists(entity);
            }
        }

    }
}