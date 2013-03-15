using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Entity;
using System.Linq.Expressions;
using Neocean.Infrastructure;

namespace Neocean.Domain.Data
{
    public class EfRepository<TAggregateRoot> : IRepository<TAggregateRoot>
         where TAggregateRoot : AggregateRoot, new()
    {
        #region Private Fields
        private readonly IDbContext _context;
        private IDbSet<TAggregateRoot> _entities;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Property
        public virtual IQueryable<TAggregateRoot> Table
        {
            get
            {
                return this.Entities;
            }
        }
        #endregion

        #region Private Property
        private IDbSet<TAggregateRoot> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TAggregateRoot>();
                return _entities;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="sortPredicate"></param>
        /// <param name="sortOrder"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        protected IEnumerable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            IQueryable<TAggregateRoot> queryable = null;
            queryable = this.Entities.Where(specification);
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return queryable.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return queryable.ToList();
        }

        protected PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1。");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1。");

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            IQueryable<TAggregateRoot> queryable = null;
            queryable = this.Entities.Where(specification);

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending = queryable.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                    case SortOrder.Descending:
                        var pagedGroupDescending = queryable.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="aggregateRoot">需要添加到仓储的聚合根实例。</param>
        public void Add(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");
            this.Entities.Add(aggregateRoot);
        }
        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="aggregateRoot">需要更新的聚合根。</param>
        public void Update(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");
            this.Entities.Attach(aggregateRoot);
        }
        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="aggregateRoot">需要从仓储中移除的聚合根。</param>
        public void Remove(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException("aggregateRoot");
            this.Entities.Remove(aggregateRoot);
        }
        /// <summary>
        /// 返回一个<see cref="Boolean"/>值，该值表示符合指定规约条件的聚合根是否存在。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>如果符合指定规约条件的聚合根存在，则返回true，否则返回false。</returns>
        public bool Exists(Expression<Func<TAggregateRoot, bool>> specification)
        {
            int count = this.Entities.Count<TAggregateRoot>(specification);
            return count != 0;
        }
        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <returns>聚合根实例。</returns>
        public TAggregateRoot GetByKey(Guid key)
        {
            return this.Entities.Where(p => p.ID == key).FirstOrDefault();
        }
        /// <summary>
        /// 从仓储中读取所有聚合根。
        /// </summary>
        /// <returns>所有的聚合根。</returns>
        public IEnumerable<TAggregateRoot> GetAll()
        {
            return this.Entities.ToList();
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>排序后的所有聚合根。</returns>
        public IEnumerable<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.GetAll(o => true, sortPredicate, sortOrder);
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的聚合根集合。</returns>
        public PagedResult<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.GetAll(o => true, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// 根据指定的规约，从仓储中获取所有符合条件的聚合根。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>所有符合条件的聚合根。</returns>
        public IEnumerable<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, bool>> specification)
        {
            return this.Entities.Where(specification).ToList();
        }
        /// <summary>
        /// 根据指定的规约，从仓储中获取所有符合条件的聚合根
        /// </summary>
        /// <param name="specification">规约</param>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns>排序后的所有聚合根</returns>
        public IEnumerable<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, bool>> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder);
            if (results == null || results.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的聚合根集合。</returns>
        public PagedResult<TAggregateRoot> GetAll(Expression<Func<TAggregateRoot, bool>> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
            if (results == null || results == PagedResult<TAggregateRoot>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }
        /// <summary>
        /// 根据指定的规约获取聚合根。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>聚合根。</returns>
        public TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> specification)
        {
            TAggregateRoot result = this.Find(specification);
            if (result == null)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return result;
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，从仓储中查找所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <returns>排序后的所有聚合根。</returns>
        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.GetAll(sortPredicate, sortOrder);
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中查找所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的聚合根集合。</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return FindAll(o => true, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        /// <summary>
        /// 根据指定的规约，从仓储中获取所有符合条件的聚合根。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>所有符合条件的聚合根。</returns>
        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> specification)
        {
            return this.GetAll(specification);
        }
        /// <summary>
        /// 以指定的排序字段和排序方式，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="sortPredicate">用于表述排序字段的Lambda表达式。</param>
        /// <param name="sortOrder">排序方式。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的聚合根集合。</returns>
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var results = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
            if (results == null || results == PagedResult<TAggregateRoot>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }
        /// <summary>
        /// 根据指定的规约查找聚合根。
        /// </summary>
        /// <param name="specification">规约。</param>
        /// <returns>聚合根。</returns>
        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> specification)
        {
            return this.Entities.Where(specification).FirstOrDefault();
        }
        #endregion
    }
}
