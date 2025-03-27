using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Payment Successfully";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }

            SendMail();
        }



        private void SendMail()
        {
            string fromMail = "limcl-wm23@student.tarc.edu.my";
            string fromPassword = "980304108093";

            string customerEmail = Session["CustomerEmail"].ToString();


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Thank You!";
            message.To.Add(new MailAddress(customerEmail));
            message.Body = "<html><body> Thank you for your purchase! You may check your purchase history at your account </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}