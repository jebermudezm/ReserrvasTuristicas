using System;
using System.Data.SqlClient;
using System.Net.Mail;
namespace WebControls.Mailing
{
	public static class MailFactory
	{
		public static Mail CreateBUIMail(SqlConnection conx)
		{
			return new BUIMail(conx);
		}
		public static Mail CreateEMail(SmtpClient conx)
		{
			return new EMail(conx);
		}
		public static Mail CreateEMail(string server)
		{
			return new EMail(server);
		}
	}
}
