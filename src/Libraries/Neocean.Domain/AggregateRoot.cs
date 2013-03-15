using System;
using System.Collections.Generic;

using Neocean.Domain.Events;

namespace Neocean.Domain
{
    /// <summary>
    /// 表示继承于该接口的类型是聚合根类型
    /// </summary>
    public class AggregateRoot : Entity, IAggregateRoot
    {
        #region Private Fields
        private readonly List<IDomainEvent> events = new List<IDomainEvent>();
        #endregion

        #region Protected Methods
        /// <summary>
        /// 产生领域事件。
        /// </summary>
        /// <typeparam name="TEvent">需要产生的领域事件的类型。</typeparam>
        /// <param name="evnt">需要产生的领域事件。</param>
        protected virtual void RaiseEvent<TEvent>(TEvent evnt)
            where TEvent : class, IDomainEvent
        {
            DomainEventAggregator.Publish<TEvent>(evnt);
            events.Add(evnt);
        }
        /// <summary>
        /// 产生领域事件。
        /// </summary>
        /// <typeparam name="TEvent">需要产生的领域事件的类型。</typeparam>
        /// <param name="evnt">需要产生的领域事件。</param>
        //protected virtual async void RaiseEventAsync<TEvent>(TEvent evnt)
        //    where TEvent : class, IDomainEvent
        //{
        //    await DomainEventAggregator.PublishAsync<TEvent>(evnt);
        //    events.Add(evnt);
        //}
        #endregion

        #region Public Methods
        /// <summary>
        /// 确定指定的Object是否等于当前的Object。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的Object与当前Object相等，则返回true，否则返回false。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.equals。
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            IAggregateRoot ar = obj as IAggregateRoot;
            if (ar == null)
                return false;
            return this.id == ar.ID;
        }
        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
        #endregion

        #region IAggregateRoot Members
        /// <summary>
        /// 获取所有未经提交的领域事件。
        /// </summary>
        public IEnumerable<IDomainEvent> UncommittedEvents
        {
            get { return events; }
        }
        /// <summary>
        /// 当完成领域事件向外部系统提交后，清除所有已产生的领域事件。
        /// </summary>
        public void ClearEvents()
        {
            events.Clear();
        }
        #endregion
    }
}
