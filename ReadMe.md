# c#发送需要回执的邮件

原理：发送时，通过配置项，发送回执邮件。读取统计时，根据邮件标题来进行统计。

### 发送相关代码

```
mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure 
    | DeliveryNotificationOptions.Delay;
smtp.Credentials = new System.Net.NetworkCredential("", "");


mail.Headers.Add("ReturnReceipt", "1");
mail.Headers.Add("Disposition-Notification-To", "");
```

### 接收时的相关代码
```
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
```

额外的话：如果不需要统计清楚是哪个账号发送的，可以在发送邮件的内容中，配置图片img src地址填入自己的后台接口（后台接口实现统计入库）完成总量统计。

# 注意:如有问题，自己解决
# 来仓库找密码的可以省省了。
