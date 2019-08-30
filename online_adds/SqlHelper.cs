using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace online_adds
{
    public class SqlHelper
    {
        private static string strConn;

        static SqlHelper()
        {
            strConn = ConfigurationManager.ConnectionStrings["online_dbConnectionString"].ConnectionString;
        }

        public static int ExecuteNonQuery(string query)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cnn.Open();
            int retval = cmd.ExecuteNonQuery();
            cnn.Close();
            return retval;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(query, cnn);
            FillParameters(cmd, p);
            cnn.Open();
            int retval = cmd.ExecuteNonQuery();
            cnn.Close();
            return retval;
        }

        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader ExecuteReader(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            FillParameters(cmd, p);
            cnn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static object ExecuteScalar(string sql)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            object retval = cmd.ExecuteScalar();
            cnn.Close();
            return retval;
        }

        public static object ExecuteScalar(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            FillParameters(cmd, p);
            cnn.Open();
            object retval = cmd.ExecuteScalar();
            cnn.Close();
            return retval;
        }

        public static DataSet ExecuteDataSet(string sql)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public static DataSet ExecuteDataSet(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            FillParameters(cmd, p);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        private static void FillParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
        } 
    }
}