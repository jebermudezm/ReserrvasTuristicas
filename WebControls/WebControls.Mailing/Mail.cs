using System;
namespace WebControls.Mailing
{
	public abstract class Mail
	{
		private string myFrom;
		private string myTo;
		private string myCc;
		private string myBcc;
		private string mySubject;
		private string myBody;
		public string From
		{
			get
			{
				return this.myFrom;
			}
			set
			{
				this.myFrom = value;
			}
		}
		public string To
		{
			get
			{
				return this.myTo;
			}
			set
			{
				this.myTo = value;
			}
		}
		public string Cc
		{
			get
			{
				return this.myCc;
			}
			set
			{
				this.myCc = value;
			}
		}
		public string Bcc
		{
			get
			{
				return this.myBcc;
			}
			set
			{
				this.myBcc = value;
			}
		}
		public string Subject
		{
			get
			{
				return this.mySubject;
			}
			set
			{
				this.mySubject = value;
			}
		}
		public string Body
		{
			get
			{
				return this.myBody;
			}
			set
			{
				this.myBody = value;
			}
		}
		public int AppCode
		{
			get;
			set;
		}
		public Mail()
		{
			this.myFrom = null;
			this.myTo = null;
			this.myCc = null;
			this.myBcc = null;
			this.mySubject = null;
			this.myBody = null;
			this.AppCode = 0;
		}
		public abstract void Send();
		protected void Verify()
		{
			if (this.From == null || this.From.Trim() == "")
			{
				throw new Exception("Debe colocar el Destinatario");
			}
			if (this.Subject == null || this.Subject.Trim() == "")
			{
				throw new Exception("Debe colocar el Asunto del Mensaje");
			}
			if (this.Body == null || this.Body.Trim() == "")
			{
				throw new Exception("Debe colocar el cuerpo del Mensaje");
			}
			if (this.AppCode == 0)
			{
				throw new Exception("Debe colocar el Identificador de la Aplicación que envía el Mensaje");
			}
		}
	}
}
