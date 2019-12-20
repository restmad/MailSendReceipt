using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using OpenPop.Pop3;

namespace MailSendReceipt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //这是收取回执的邮箱
            var Receipt = "";

            //实例化两个必要的
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            //发送邮箱地址
            mail.From = new MailAddress("");

            //收件人
            mail.To.Add(new MailAddress(""));
            mail.To.Add(new MailAddress(""));
            mail.To.Add(new MailAddress(""));

            //    mail.To.Add(new MailAddress("ruimaj@isoftstone.com"));

            //是否以HTML格式发送
            mail.IsBodyHtml = true;
            //主题的编码格式
            mail.SubjectEncoding = Encoding.UTF8;
            //邮件的标题
            mail.Subject = "测试一下发件的标题" + Guid.NewGuid().ToString();

            textBox2.Text = mail.Subject;
            //内容的编码格式
            mail.BodyEncoding = Encoding.UTF8;
            //邮件的优先级
            mail.Priority = MailPriority.Normal;
            mail.ReplyToList.Add("");
            //发送内容,带一个图片标签,用于对方打开之后,回发你填写的地址信息
            mail.Body = @"邮件通知";

            //发件邮箱的服务器地址
            smtp.Host = "";
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 1000000;
            //是否为SSL加密
            smtp.EnableSsl = true;
            //设置端口,如果不设置的话,默认端口为25
            smtp.Port = 25;
            smtp.UseDefaultCredentials = true;
            //验证发件人的凭据
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;
            smtp.Credentials = new System.Net.NetworkCredential("", "");


            mail.Headers.Add("ReturnReceipt", "1");
            mail.Headers.Add("Disposition-Notification-To", "");
            //    mail.Headers.Add("Return-Receipt-To", "ruimaj@isoftstone.com");
            try
            {
                //发送邮件
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pop3Client client = new Pop3Client();

            if (client.Connected)
            {
                client.Disconnect();
            }

            List<string> accountList = new List<string>();
            // Connect to the server, false means don't use ssl
            client.Connect("", 110, false);

            // Authenticate ourselves towards the server by email account and password
            client.Authenticate("", "");

            //email count
            int messageCount = client.GetMessageCount();

            //i = 1 is the first email; 1 is the oldest email
            for (int i = 1; i <= messageCount; i++)
            {
                var header = client.GetMessageHeaders(i);
                if (header.Subject.Contains(textBox2.Text))
                {
                    string mail = header.From.Address;
                    if (!accountList.Contains(mail))
                    {
                        accountList.Add(mail);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (string account in accountList)
            {
                sb.Append(account + "已阅\n");
            }

            richTextBox1.Text = sb.ToString();
        }
    }
}
