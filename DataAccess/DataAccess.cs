using System;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace LightWeightRulesEngine
{
    public class DataAccess
    {
        public DataAccess()
        {

        }
        static SqlConnection conn = new SqlConnection();
        public static SqlConnection CreateConnection(string connString)
        {

            conn = new SqlConnection();
            conn.ConnectionString = connString;
            conn.Open();
            return conn;
        }
        public static void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }
        public static object GetValue(string sSQL, string ConnectionString)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            object oReturn = null;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            oReturn = Cmd.ExecuteScalar();
            CloseConnection(conn);
            return oReturn;
        }
        public static IDataReader GetRecordArray(string sSQL, SqlConnection conn)
        {
            //SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            SqlDataReader rd = null;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            IDataReader irdr;
            try
            {
                rd = Cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                irdr = rd;

            }
            return irdr;
        }
        public static IDataReader GetRecordArray(string sSQL, string ConnectionString)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            SqlDataReader rd = null;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            IDataReader irdr;
            try
            {
                rd = Cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                irdr = rd;
            }
            return irdr;
        }
        public static ArrayList GetRecordArray(string sSQL, string ConnectionString, bool returnArray)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            SqlDataReader rd = null;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            
            ArrayList al = new ArrayList();
            try
            {
                rd = Cmd.ExecuteReader();
                while (rd.Read())
                {
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        object[] oValues = null ;
                        rd.GetValues(oValues);
                        al.Add(oValues);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return al;
        }
        public static long ExecuteSQL(string sSQL, string ConnectionString)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            long lngReturn = 0;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            try
            {
                lngReturn = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(conn);
            }
            return lngReturn;
        }
        public static string ExecuteSQL(out string sId, string sSQL, string ConnectionString)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand Cmd = conn.CreateCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = sSQL;
            try
            {
                sId = (string)Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(conn);
            }
            return sId;
        }

        public static void ExecuteSQL(String SQL, String ConnectionString, DataTable dt)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(SQL, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
        }

        public static void ExecuteSQL(String SQL, String ConnectionString, DataTable dt, SqlParameterCollection spc)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(SQL, conn);
            foreach (SqlParameter sp in spc) cmd.Parameters.Add(sp);
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
        }

        public static void ExecuteSQL(String SQL, String ConnectionString,  List<SqlParameter> spc)
        {
            SqlConnection conn = CreateConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand( SQL, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter sp in spc) cmd.Parameters.Add(((SqlParameter)sp));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static DataSet GetDataSet(string sSQL, string ConnectionString)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlDataAdapter da = new SqlDataAdapter(sSQL, ConnectionString);
                SqlConnection conn = da.SelectCommand.Connection;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public static DataSet GetDataSet(string sSQL, string ConnectionString, string DataSetName)
        {
            DataSet ds = new DataSet(DataSetName);
            try
            {

                SqlDataAdapter da = new SqlDataAdapter(sSQL, ConnectionString);
                SqlConnection conn = da.SelectCommand.Connection;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
    }
}