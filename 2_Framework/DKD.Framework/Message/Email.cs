using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace DKD.Framework.Message
{
    /// <summary>
    /// 电子邮件发送
    /// </summary>
    public class Email
    {
        private Config.FrameworkConfig config = Config.ConfigBase.Instance<Config.FrameworkConfig>();

        /// <summary>
        /// 发送电子邮件 ，格式为HTML模式 True:隐密模式 False:不是隐密模式
        /// </summary>
        /// <param name="title">电子邮件标题</param>
        /// <param name="body">电子邮件内容</param>
        /// <param name="from">发送人</param>
        /// <param name="toUser">接收人</param>
        /// <returns></returns>
        public bool SendMail(string title, string body, EmailAddress from, EmailAddress toUser)
        {
            return SendMail(title, body, from, new List<EmailAddress> { toUser }, false);
        }

        /// <summary>
        /// 发送电子邮件 ，格式为HTML模式 True:隐密模式 False:不是隐密模式
        /// </summary>
        /// <param name="title">电子邮件标题</param>
        /// <param name="body">电子邮件内容</param>
        /// <param name="from">发送人</param>
        /// <param name="toUser">接收人</param>
        /// <param name="isPrivate">是否隐密模式</param>
        /// <returns></returns>
        public bool SendMail(string title, string body, EmailAddress from, EmailAddress toUser, bool isPrivate)
        {
            return SendMail(title, body, from, new List<EmailAddress> { toUser }, isPrivate);
        }

        /// <summary>
        /// 发送电子邮件 ，格式为HTML模式 True:隐密模式 False:不是隐密模式
        /// </summary>
        /// <param name="title">电子邮件标题</param>
        /// <param name="body">电子邮件内容</param>
        /// <param name="from">发送人</param>
        /// <param name="toUser">接收人</param>
        /// <param name="isPrivate">是否隐密模式</param>
        /// <param name="isSsl">是否启用ssl</param>
        /// <returns></returns>
        public bool SendMail(string title, string body, EmailAddress from, EmailAddress toUser, bool isPrivate, bool isSsl)
        {
            return SendMail(title, body, from, new List<EmailAddress> { toUser }, isPrivate, isSsl);
        }

        /// <summary>
        /// 发送电子邮件 ，格式为HTML模式 True:隐密模式 False:不是隐密模式 默认启用ssl
        /// </summary>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="from">由谁发的</param>
        /// <param name="toUsers">收件人地址</param>
        /// <param name="isPrivate">是否隐密模式</param>
        /// <returns></returns>
        public bool SendMail(string title, string body, EmailAddress from, List<EmailAddress> toUsers, bool isPrivate)
        {
            return SendMail(title, body, from, toUsers, isPrivate, true);
        }

        /// <summary>
        /// 发送电子邮件 ，格式为HTML模式 True:隐密模式 False:不是隐密模式
        /// </summary>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="from">由谁发的</param>
        /// <param name="toUsers">收件人地址</param>
        /// <param name="isPrivate">是否隐密模式</param>
        /// <param name="isSsl">是不启用ssl</param>
        /// <returns></returns>
        public bool SendMail(string title, string body, EmailAddress from, List<EmailAddress> toUsers, bool isPrivate, bool isSsl)
        {
            if (toUsers == null || toUsers.Count == 0)
                return false;

            try
            {
                var msg = new MailMessage();
                msg.From = from.Address == null ? new MailAddress(config.EmailUser) : new MailAddress(from.Address, from.ShowName);

                if (isPrivate)

                    toUsers.ForEach(delegate(EmailAddress ea)
                    {
                        msg.Bcc.Add(new MailAddress(ea.Address, ea.ShowName));
                    });

                else
                    toUsers.ForEach(delegate(EmailAddress ea)
                    {
                        msg.To.Add(new MailAddress(ea.Address, ea.ShowName));
                    });

                msg.Subject = title;
                msg.SubjectEncoding = Encoding.UTF8;

                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;

                msg.Priority = MailPriority.High;

                msg.IsBodyHtml = true;

                SmtpClient scMailServer = new SmtpClient();
                scMailServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                scMailServer.Credentials = new NetworkCredential(config.EmailUser, config.EmailPassword);
                scMailServer.Host = config.EmailHost;
                scMailServer.Port = config.EmailPort;
                scMailServer.EnableSsl = isSsl;

                scMailServer.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("电子邮件发送出错",ex);
            }

        }

    }

    /// <summary>
    /// 电子邮件的收/发件人
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// 显示的友好名
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 真实的地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="showName"></param>
        /// <param name="address"></param>
        public EmailAddress(string showName, string address)
        {
            ShowName = showName;
            Address = address;
        }
    }
}