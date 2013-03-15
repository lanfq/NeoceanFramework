﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neocean.Domain.Model.Customer
{
    /// <summary>
    /// 表示“用户”领域概念的聚合根
    /// </summary>
    public class User : AggregateRoot
    {
        #region Private Fields
        private string userName;
        private string password;
        private string email;
        private bool isDisabled;
        private DateTime dateRegistered;
        private string contact;
        private string phoneNumber;
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取或设置当前客户的用户名。
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 获取或设置当前客户的登录密码。
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 获取或设置当前客户的电子邮件地址。
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// 获取或设置一个<see cref="Boolean"/>值，该值表示当前用户账户是否已被禁用。
        /// </summary>
        /// <remarks>在ByteartRetail V3中，仅提供对此属性的管理界面，在实际业务处理中
        /// 并没有使用到该属性。</remarks>
        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        /// <summary>
        /// 获取或设置用户账户注册的时间。
        /// </summary>
        public DateTime DateRegistered
        {
            get { return dateRegistered; }
            set { dateRegistered = value; }
        }

        /// <summary>
        /// 获取或设置当前客户的联系人信息。
        /// </summary>
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        /// <summary>
        /// 获取或设置用户账户的联系电话信息。
        /// </summary>
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 返回表示当前Object的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return this.userName;
        }

        /// <summary>
        /// 禁用当前账户。
        /// </summary>
        public void Disable()
        {
            this.isDisabled = true;
        }

        /// <summary>
        /// 启用当前账户。
        /// </summary>
        public void Enable()
        {
            this.isDisabled = false;
        }
        #endregion
    }
}
