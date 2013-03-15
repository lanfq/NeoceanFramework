using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neocean.Domain
{
    /// <summary>
    /// 表示继承于该接口的类型是实体类型
    /// </summary>
    public class Entity : IEntity
    {
        #region Protected Fields
        protected Guid id;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public Entity()
        {
            ID = Guid.NewGuid();
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// 获取当前领域实体类的全局唯一标识。
        /// </summary>
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    }
}
