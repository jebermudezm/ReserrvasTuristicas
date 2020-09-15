using System;
using System.Data.SqlClient;
namespace WebControls.Mailing
{
	public class BUIMail : Mail
	{
		private SqlConnection myConx;
		public BUIMail(SqlConnection conx)
		{
			this.myConx = conx;
		}
		public override void Send()
		{
			//string text = "INSERT INTO ENVIADOR_CORREO (";
            //text += "CORREO_PROFILE_NAME, CORREO_TO, CORREO_CC, CORREO_CCO, CORREO_SUBJECT, CORREO_BODY, CORREO_DATETIME, CORREO_APLIC_CODE";
            //text += ") VALUES (";
            //text += "@CORREO_PROFILE_NAME, @CORREO_TO, @CORREO_CC, @CORREO_CCO, @CORREO_SUBJECT, @CORREO_BODY, @CORREO_DATETIME, @CORREO_APLIC_CODE)";
            //base.Verify();
            try
            {
                //	this.myConx.Open();
                //	SqlCommand expr_47 = new SqlCommand(text, this.myConx);
                //	expr_47.Parameters.AddWithValue("@CORREO_PROFILE_NAME", base.From);
                //	expr_47.Parameters.AddWithValue("@CORREO_TO", base.To.Replace(",", ";"));
                //	expr_47.Parameters.AddWithValue("@CORREO_CC", (base.Cc == null) ? DBNull.Value : base.Cc.Replace(",", ";"));
                //	expr_47.Parameters.AddWithValue("@CORREO_CCO", (base.Bcc == null) ? DBNull.Value : base.Bcc.Replace(",", ";"));
                //	expr_47.Parameters.AddWithValue("@CORREO_SUBJECT", base.Subject);
                //	expr_47.Parameters.AddWithValue("@CORREO_BODY", base.Body);
                //	expr_47.Parameters.AddWithValue("@CORREO_DATETIME", DateTime.Now);
                //	expr_47.Parameters.AddWithValue("@CORREO_APLIC_CODE", base.AppCode);
                //	expr_47.ExecuteNonQuery();
                //	this.myConx.Close();
            }
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
	}
}
