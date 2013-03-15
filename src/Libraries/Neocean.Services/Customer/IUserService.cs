using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neocean.DataObjects;

namespace Neocean.Services.Customer
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        void InsertUser(List<UserDataObject> user);
    }
}
