using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
        ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria != null){
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (cur,inc) => cur.Include(inc));

            return query;
        }
    }
}