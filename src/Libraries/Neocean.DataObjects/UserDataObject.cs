using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Neocean.DataObjects
{
    public class UserDataObject
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请重新输入密码以便确认")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "电子邮件")]
        [Required(ErrorMessage = "请输入电子邮件")]
        [DataType(DataType.EmailAddress, ErrorMessage = "电子邮件格式不正确")]
        public string Email { get; set; }

        [Display(Name = "已禁用")]
        public bool? IsDisabled { get; set; }

        [Display(Name = "注册时间")]
        [DataType(DataType.Date)]
        public DateTime? DateRegistered { get; set; }

        [Display(Name = "联系人")]
        [Required(ErrorMessage = "请输入联系人")]
        public string Contact { get; set; }

        [Display(Name = "电话号码")]
        [Required(ErrorMessage = "请输入电话号码")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)",
            ErrorMessage = "电话号码格式不正确")]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    
    }
}
