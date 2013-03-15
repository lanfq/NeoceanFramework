using System;
using System.Net;
using System.Net.Mail;
using Neocean.Infrastructure.Config;

namespace Neocean.Infrastructure
{
    /// <summary>
    /// 表示用于整个Neocean Frame框架的工具类。
    /// </summary>
    public class Utils
    {
        #region Public Static Methods

        /// <summary>
        /// 向指定的邮件地址发送邮件。
        /// </summary>
        /// <param name="to">需要发送邮件的邮件地址。</param>
        /// <param name="subject">邮件主题。</param>
        /// <param name="content">邮件内容。</param>
        public static void SendEmail(string to, string subject, string content)
        {
            var msg = new MailMessage(NeoceanConfigurationReader.Instance.EmailSender,
                                      to,
                                      subject,
                                      content);
            var smtpClient = new SmtpClient(NeoceanConfigurationReader.Instance.EmailHost)
                                 {
                                     Port = NeoceanConfigurationReader.Instance.EmailPort,
                                     Credentials =
                                         new NetworkCredential(NeoceanConfigurationReader.Instance.EmailUserName,
                                                               NeoceanConfigurationReader.Instance.EmailPassword),
                                     EnableSsl = NeoceanConfigurationReader.Instance.EmailEnableSsl
                                 };
            smtpClient.Send(msg);
        }

        #endregion
    }
}