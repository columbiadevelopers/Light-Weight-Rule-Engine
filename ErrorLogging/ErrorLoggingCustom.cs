using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Reflection;


namespace LightWeightRulesEngine
{
    public partial class ErrorLogging
    {
        private static string _ConnectionString = String.Empty;
        public static string ConnectionString
        {
            get
            {
                string pathRaw = Assembly.GetExecutingAssembly().Location;
                string ConnectionStringPath = pathRaw.Substring(0, pathRaw.IndexOf("bin") - 1);
                if (_ConnectionString == String.Empty)
                    _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.Replace("XXXXXX", ConnectionStringPath);
                
                return _ConnectionString;
            }
        }

        public ErrorLogging(Int32 ErrorId, Int32 AppId, Int32 FormId, String ErrorDescription)
        {

            ErrorIdValue = ErrorId;
            AppIdValue = AppId;
            FormIdValue = FormId;
            ErrorDescriptionValue = ErrorDescription;
        }

        public static void Insert_ErrorLogging(ErrorLogging oError)
			{
                List<SqlParameter> spc = new List<SqlParameter>();
            spc.Add( new SqlParameter("AppId", oError.AppId));
            spc.Add(new SqlParameter("FormId", oError.FormId));
            spc.Add(new SqlParameter("ErrorDescription", oError.ErrorDescription));
			String SQL = "pr_App_ErrorLogging_Insert";
			DataAccess.ExecuteSQL(SQL, ConnectionString, spc);			
			}

        public static DataTable SelectAll_ErrorLogging()
        {            
            String SQL = "pr_App_ErrorLogging_SelectAll";
            DataTable dt = new DataTable();
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            return dt;

        }

        public static void ClearLog()
        {
            String SQL = "pr_App_ErrorLogging_Clear";
            DataAccess.ExecuteSQL(SQL, ConnectionString);
        }
    }
}
