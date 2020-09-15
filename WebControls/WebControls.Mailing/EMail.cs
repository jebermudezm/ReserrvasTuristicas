using System;
using System.Net.Mail;
using System.Text;
namespace WebControls.Mailing
{
	public class EMail : Mail
	{
		private SmtpClient myConx;
		public EMail(SmtpClient conx)
		{
			this.myConx = conx;
		}
		public EMail(string server)
		{
			this.myConx = new SmtpClient(server);
		}
		public override void Send()
		{
			MailMessage mailMessage = new MailMessage();
			base.AppCode = 1;
			base.Verify();
			try
			{
				mailMessage.From = new MailAddress(base.From);
				mailMessage.To.Add(base.To.Replace(";", ","));
				if (base.Cc != null)
				{
					mailMessage.CC.Add(base.Cc.Replace(";", ","));
				}
				if (base.Bcc != null)
				{
					mailMessage.Bcc.Add(base.Bcc.Replace(";", ","));
				}
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.Subject = base.Subject;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = base.Body;
				this.myConx.Send(mailMessage);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		public void Dispose()
		{
			this.myConx.Dispose();
		}
	}
}
