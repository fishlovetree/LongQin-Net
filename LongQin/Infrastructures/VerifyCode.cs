using System.Net.Mail;
using System;
using System.Runtime.Caching;

namespace LongQin.Infrastructures
{
    public static class VerifyCode
    {
        private static MemoryCache _cache = MemoryCache.Default;

        /// <summary> 
        /// 发送验证码 
        /// </summary> 
        /// <param name = "Email" > 邮箱 </ param >
        /// < returns > 元祖类型（发送状态和发送结果描述）</returns>
        public static Tuple<bool, string> SendCode(string Email)
        {
            //随机生成验证码（6位）
            var code = new Random().Next(100000, 999999).ToString();
            Tuple<bool, string> result = null;
            //发送邮件
            result = SendEmail(Email, code);
            //返回是否发送成功
            //成功，用true，提示成功发送
            //失败，用false，并且告诉失败的原因
            if (result.Item1)
            {
                //在发送成功后，把验证码存起来，以便验证的时候使用
                //cache > key - value
                var policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(30) };
                _cache.Set(Email, code, policy); //这个key设置为不重复的即可。每个用户对应的key是 单独的。30分钟过期
                return Tuple.Create(true, $"发送成功，验证码是{code}");
            }
            else
            {
                return Tuple.Create(false, result.Item2);
            }
        }

        /// <summary>
        /// 后台邮件发送
        /// </summary>
        /// <param name="toMail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static Tuple<bool, string> SendEmail(string toMail, string code)
        {
            //发件人邮箱
            string from = "344589313@qq.com";
            //发件人
            string fromName = "龙琴科技";
            string host = "smtp.qq.com";
            int port = 587;
            //注意开启邮箱中POP3/SMTP服务 这是授权码
            string smtpPass = "hmwcghzkhyijbjjf";
            try
            {
                //收件人信息
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(from, fromName);//发件人的邮箱 地址和显示名称，这个可以放到配置文件里
                msg.To.Add(new MailAddress(toMail)); //收件人地址
                msg.Subject = "【龙琴科技】邮箱验证码";//邮件标题
                msg.Body = "注册邮箱验证码为" + code + "，验证码有效期为30分钟!"; //正文内容,可以放html
                //第二部分：发邮件服务器信息
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host; //qq的发邮件服务器 smtp.qq.com
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(from, smtpPass);
                //第三部分：发送
                smtp.Send(msg);
                return Tuple.Create(true, "发送成功");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, $"发送失败，具体原因：{ex.Message}");
            }
        }

        public static string GetVerifyCode(string Email) 
        {
            return _cache.Get(Email) == null ? "" : _cache.Get(Email).ToString();
        }
    }
}