using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFacturacion.EntityConections
{
    public class SendMessage
    {

        MimeMessage message = new MimeMessage();
      //  message.





















        //        SmtpClient client = new SmtpClient("mysmtpserver");
        //        client.UseDefaultCredentials = false;
        //client.Credentials = new NetworkCredential("username", "password");

        //        MailMessage mailMessage = new MailMessage();
        //        mailMessage.From = new MailAddress("whoever@me.com");
        //        mailMessage.To.Add("receiver@me.com");
        //mailMessage.Body = "body";
        //mailMessage.Subject = "subject";
        //client.Send(mailMessage);


        //2
        //        //create the mail message
        //        MailMessage maile = new MailMessage();

        //        //set the addresses
        //        mail.From = new MailAddress("me@mycompany.com");
        //        mail.To.Add("you@yourcompany.com");

        ////set the content
        //mail.Subject = "This is an email";
        //mail.Body = "this content is in the body";

        ////add an attachment from the filesystem
        //mail.Attachments.Add(new Attachment("c:\\temp\\example.txt"));

        ////to add additional attachments, simply call .Add(...) again
        //mail.Attachments.Add(new Attachment("c:\\temp\\example2.txt"));
        //mail.Attachments.Add(new Attachment("c:\\temp\\example3.txt"));

        ////send the message
        //SmtpClient smtp = new SmtpClient("127.0.0.1");
        //        smtp.Send(mail);
    }
}
