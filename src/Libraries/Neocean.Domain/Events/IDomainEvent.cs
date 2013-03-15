using System;
using Neocean.Events;

namespace Neocean.Domain.Events
{
    public interface IDomainEvent : IEvent
    {
        #region Properties
        /// <summary>
        /// 获取产生领域事件的事件源对象。
        /// </summary>
        IEntity Source { get; }
        #endregion
    }
}
