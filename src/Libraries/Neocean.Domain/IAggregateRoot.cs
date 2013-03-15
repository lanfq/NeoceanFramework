﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neocean.Domain.Events;

namespace Neocean.Domain
{
    /// <summary>
    /// 表示继承于该接口的类型是聚合根类型
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// 获取所有未经提交的领域事件。
        /// </summary>
        IEnumerable<IDomainEvent> UncommittedEvents { get; }
        /// <summary>
        /// 当完成领域事件向外部系统提交后，清除所有已产生的领域事件。
        /// </summary>
        void ClearEvents();
    }
}
