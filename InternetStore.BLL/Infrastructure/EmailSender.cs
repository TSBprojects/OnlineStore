using Internet_store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternetStore.BLL.Infrastructure
{
    static class EmailSender
    {
        static private Timer messTimeout;
        static private Queue<MailMessage> messPool;
        static public void Send(string recipient, string subject, string body)
        {
            if(messPool == null)
            {
                messPool = new Queue<MailMessage>();
            }

            //"anonim.ill-wisher@bk.ru"
            MailAddress from = new MailAddress("be.green@inbox.ru");
            MailAddress to = new MailAddress(recipient);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.bk.ru", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("be.green@inbox.ru", "7&3XzuJQrHlh");
            smtp.EnableSsl = true;

            if (messTimeout == null)
            {
                smtp.Send(m);
                messTimeout = new Timer(new TimerCallback(MessTimeout), smtp, 3000, Timeout.Infinite);
            }
            else
            {
                messPool.Enqueue(m);
            }
        }

        static public void MessTimeout(object obj)
        {
            if(messPool.Count > 0)
            {
                ((SmtpClient)obj).Send(messPool.Dequeue());
                new Timer(new TimerCallback(MessTimeout), (SmtpClient)obj, 3000, Timeout.Infinite);
            }
            else
            {
                messTimeout = null;
            }
        }

        static public string GetHtmlEmail(User user, Order order)
        {
            string header = "<html><head><meta http-equiv='Content-Type' content='text/html;charset=windows-1252'></head><body>" +
                    "<table cellpadding='7' border='1' style='border-collapse: collapse;text-align:center;" +
                    "width: 545px;position: absolute;top: 0;right: 0;bottom: 0;left: 0;margin: auto;'>" +
                        "<tbody>" +
                            "<tr style='background-color: rgb(148, 218, 70);'>" +
                                "<th>Product</th>" +
                                "<th>Price</th>" +
                                "<th>Quantity</th>" +
                                "<th>Total</th>" +
                            "</tr>";
            string footer = "</tbody></table></body></html>";
            string body = "";
            foreach (OrderItem item in order.OrderItems)
            {
                body += "<tr><td>" + item.Product.Name + "</td>" +
                    "<td>$" + item.Product.Price.ToString(".00").Replace(",", ".") + "</td>" +
                    "<td>" + item.ItemCount + "</td>" +
                    "<td>$" + (item.Product.Price * item.ItemCount).ToString(".00").Replace(",", ".") + "</td></tr>";
            }
            body += "<tr><td> - </td>" +
                    "<td> - </td>" +
                    "<td>" + order.ProductsCount + "</td>" +
                    "<td>$" + order.OrderPrice.ToString(".00").Replace(",", ".") + "</td></tr>";
            return header + body + footer;
        }
    }

}
