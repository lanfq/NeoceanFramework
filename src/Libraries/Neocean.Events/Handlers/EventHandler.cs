using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neocean.Events.Handlers
{
    /// <summary>
    /// 表示事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件的类型，针对该类型事件会被当前事件处理器所处理.</typeparam>
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : class,IEvent
    {
        #region IEventHandler<TEvent> Members
        /// <summary>
        /// 处理给定的事件.
        /// </summary>
        /// <param name="evnt">需要处理的事件</param>
        public abstract void Handle(TEvent evnt);

        #endregion

        #region IEventHandler Members
        /// <summary>
        /// 处理给定的事件.
        /// </summary>
        /// <param name="evnt">需要处理的事件</param>
        public void Handle(IEvent evnt)
        {
            if (evnt is TEvent)
                Handle(evnt as TEvent);
        }

        #endregion
    }
}
