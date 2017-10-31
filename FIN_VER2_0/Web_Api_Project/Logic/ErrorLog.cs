using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mail;

namespace Web_Api_Project.Logic
{
    public class dbErrorLogging
    {
        public dbErrorLogging()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void LogError(Exception oEx)
        {
            bool blLogCheck = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["EnableLog"]);
            bool blLogEmailCheck = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["EnableLogEmail"]);
            if (blLogCheck)
            {
                HandleException(oEx);
            }
            if (blLogEmailCheck)
            {
                SendExceptionMail(oEx);
            }
        }
        public static void HandleException(Exception ex)
        {
            HttpContext ctxObject = HttpContext.Current;
            string strLogConnString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString; ;
            string logDateTime = DateTime.Now.ToString("g");
            string strReqURL = (ctxObject.Request.Url != null) ? ctxObject.Request.Url.ToString() : String.Empty;
            string strReqQS = (ctxObject.Request.QueryString != null) ? ctxObject.Request.QueryString.ToString() : String.Empty;
            string strServerName = String.Empty;
            if (ctxObject.Request.ServerVariables["HTTP_REFERER"] != null)
            {
                strServerName = ctxObject.Request.ServerVariables["HTTP_REFERER"].ToString();
            }
            string strUserAgent = (ctxObject.Request.UserAgent != null) ? ctxObject.Request.UserAgent : String.Empty;
            string strUserIP = (ctxObject.Request.UserHostAddress != null) ? ctxObject.Request.UserHostAddress : String.Empty;
            string strUserAuthen = (ctxObject.User.Identity.IsAuthenticated.ToString() != null) ? ctxObject.User.Identity.IsAuthenticated.ToString() : String.Empty;
            string strUserName = (ctxObject.User.Identity.Name != null) ? ctxObject.User.Identity.Name : String.Empty;
            string strMessage = string.Empty, strSource = string.Empty, strTargetSite = string.Empty, strStackTrace = string.Empty;
            while (ex != null)
            {
                strMessage = ex.Message;
                strSource = ex.Source;
                strTargetSite = ex.TargetSite.ToString();
                strStackTrace = ex.StackTrace;
                ex = ex.InnerException;
            }
            if (strLogConnString.Length > 0)
            {
                SqlCommand strSqlCmd = new SqlCommand();
                strSqlCmd.CommandType = CommandType.StoredProcedure;
                strSqlCmd.CommandText = "sp_LogExceptionToDB";
                SqlConnection sqlConn = new SqlConnection(strLogConnString);
                strSqlCmd.Connection = sqlConn;
                sqlConn.Open();
                try
                {
                    strSqlCmd.Parameters.Add(new SqlParameter("@Source", strSource));
                  
                    strSqlCmd.Parameters.Add(new SqlParameter("@Message", strMessage));
                    strSqlCmd.Parameters.Add(new SqlParameter("@QueryString", strReqQS));
                    strSqlCmd.Parameters.Add(new SqlParameter("@TargetSite", strTargetSite));
                    strSqlCmd.Parameters.Add(new SqlParameter("@StackTrace", strStackTrace));
                    strSqlCmd.Parameters.Add(new SqlParameter("@ServerName", strServerName));
                    strSqlCmd.Parameters.Add(new SqlParameter("@RequestURL", strReqURL));
                    strSqlCmd.Parameters.Add(new SqlParameter("@UserAgent", strUserAgent));
                    strSqlCmd.Parameters.Add(new SqlParameter("@UserIP", strUserIP));
                    strSqlCmd.Parameters.Add(new SqlParameter("@UserAuthentication", strUserAuthen));
                    strSqlCmd.Parameters.Add(new SqlParameter("@UserName", strUserName));
                    SqlParameter outParm = new SqlParameter("@EventId", SqlDbType.Int);
                    outParm.Direction = ParameterDirection.Output;
                    strSqlCmd.Parameters.Add(outParm);
                    strSqlCmd.ExecuteNonQuery();
                    strSqlCmd.Dispose();
                    sqlConn.Close();
                }
                catch (Exception exc)
                {
                    EventLog.WriteEntry(exc.Source, "Database Error From Exception Log!", EventLogEntryType.Error, 65535);
                }
                finally
                {
                    strSqlCmd.Dispose();
                    sqlConn.Close();
                }
            }
        }
        protected static string FormatExceptionDescription(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            sb.Append("<b>Time of Error: </b>" + DateTime.Now.ToString("g") + "<br />");
            sb.Append("<b>URL:</b> " + context.Request.Url + "<br />");
            sb.Append("<b>QueryString: </b> " + context.Request.QueryString.ToString() + "<br />");
            sb.Append("<b>Server Name: </b> " + context.Request.ServerVariables["SERVER_NAME"] + "<br />");
            sb.Append("<b>User Agent: </b>" + context.Request.UserAgent + "<br />");
            sb.Append("<b>User IP: </b>" + context.Request.UserHostAddress + "<br />");
            sb.Append("<b>User Host Name: </b>" + context.Request.UserHostName + "<br />");
            sb.Append("<b>User is Authenticated: </b>" + context.User.Identity.IsAuthenticated.ToString() + "<br />");
            sb.Append("<b>User Name: </b>" + context.User.Identity.Name + "<br />");
            while (e != null)
            {
                sb.Append("<b>Message: </b>" + e.Message + "<br />");
                sb.Append("<b> Source: </b>" + e.Source + "<br />");
                sb.Append("<b>TargetSite: </b>" + e.TargetSite + "<br />");
                sb.Append("<b>StackTrace: </b>" + e.StackTrace + "<br />");
                sb.Append(Environment.NewLine + "<br />");
                e = e.InnerException;
            }
            sb.Append("--------------------------------------------------------" + "<br />");
            sb.Append("Regards," + "<br />");
            sb.Append("Admin");
            return sb.ToString();
        }
        public static void SendExceptionMail(Exception e)
        {
            //string strEmails = ConfigurationSettings.AppSettings["LogToEmail"].ToString();
            //if (strEmails.Length > 0)
            //{
            //    string[] arEmails = strEmails.Split(Convert.ToChar("|"));
            //    MailMessage strMessage = new MailMessage();
            //    strMessage.BodyFormat = MailFormat.Html;
            //    strMessage.To = arEmails[0];
            //    for (int i = 1; i < arEmails.Length; i++)
            //        strMessage.Cc = arEmails[i];
            //    string strFromEmail = ConfigurationSettings.AppSettings["LogFromEmail"].ToString();
            //    strMessage.From = strFromEmail;
            //    strMessage.Subject = "Erro Log: Exception dated " + DateTime.Now + " !";
            //    string sExceptionDescription = FormatExceptionDescription(e);
            //    strMessage.Body = sExceptionDescription;
            //    SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["smtpServer"].ToString();
            //    try
            //    {
            //        SmtpMail.Send(strMessage);
            //    }
            //    catch (Exception excm)
            //    {
            //        Debug.WriteLine(excm.Message);
            //        throw;
            //    }
            //}
            //else
            //{
            //    return;
            //}
        }
    }
}