/*
 发送短信，暂时没用
 */
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DKD.Framework;


//namespace DKD.Framework.Message
//{
//    /// <summary>
//    /// 短信发送类
//    /// </summary>
//    public class Sms
//    {

//        private SmsLib.LinkWS sms = new SmsLib.LinkWS();
//        private Config.FrameworkConfig config = Config.FrameworkConfig.Instance<Config.FrameworkConfig>();

//        /// <summary>
//        /// 单用户发送短信
//        /// </summary>
//        /// <param name="mobile">手机号码</param>
//        /// <param name="content">内容</param>
//        /// <returns></returns>
//        public bool SendSms(string mobile, string content)
//        {
//            try
//            {
//                int state = sms.Send(config.SmsUser, config.SmsPassword, mobile, content, null, null);

//                return state == 0 ? true : false;
//            }
//            catch (Exception e)
//            { "短信发送出错".Logger(e); }

//            return true;
//        }

//        /// <summary>
//        /// 多用户发送短信
//        /// </summary>
//        /// <param name="mobile">手机号码，多个号码以逗号（,）分隔 ,一次性最多支持600个号</param>
//        /// <param name="content">内容</param>
//        /// <returns></returns>
//        public bool BatchSend(string mobile, string content)
//        {
//            try
//            {
//                int state = sms.BatchSend(config.SmsUser, config.SmsPassword, mobile, content, null, null);

//                return state == 1 ? true : false;
//            }
//            catch (Exception e)
//            { "短信发送出错".Logger(e); }

//            return true;
//        }
//    }
//}
