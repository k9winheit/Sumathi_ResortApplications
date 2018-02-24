using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Common
{
   
    public class DbConnetClass
    {
        //private static ErrorLog error = new ErrorLog();
        private static MySqlConnection connect = null;

        private static MySqlConnection DBConnect()
        {
            try
            {
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
                return con;
            }
            catch (MySqlException mye)
            {
                //error.createErrorLog(mye.Message, mye.Source, "MySQL Error");
                return null;
            }
            catch (Exception e)
            {
                //error.createErrorLog(e.Message, e.Source, "Data Connection Error");
                return null;
            }
        }
        public static int setData(String q)
        {
            int result = 0;
            MySqlCommand com;
            MySqlDataReader dr;
            connect = DbConnetClass.DBConnect();
            try
            {
                connect.Open();
                com = new MySqlCommand(q);
                com.Connection = connect;
                dr = com.ExecuteReader();

                if (dr.RecordsAffected > 0)
                {
                    result = dr.RecordsAffected;
                    // MessageBox.Show("Record Successfuly Saved");
                }
                closeConnection();
            }
            catch (MySqlException mye)
            {
                //error.createErrorLog(mye.Message, mye.Source, "MySQL Error");
                closeConnection();
            }
            catch (Exception e)
            {
                //error.createErrorLog(e.Message, e.Source, "Data Sending Error");
                closeConnection();
            }

            return result;
        }
        public static MySqlDataReader getData(String q)
        {
            MySqlCommand com;
            MySqlDataReader dr;
            connect = DbConnetClass.DBConnect();
            try
            {
                connect.Open();
                com = new MySqlCommand(q);
                com.Connection = connect;
                dr = com.ExecuteReader();
                return dr;
            }
            catch (MySqlException mye)
            {
                //error.createErrorLog(mye.Message, mye.Source, "MySQL Error");
                closeConnection();
                return null;
            }
            catch (Exception e)
            {
                //error.createErrorLog(e.Message, e.Source, "Data Downloading Error");
                closeConnection();
                return null;
            }
        }

        public static DataSet getDataSet(String q)
        {
            MySqlCommand com;
            MySqlDataAdapter da;
            DataSet ds;
            connect = DbConnetClass.DBConnect();
            try
            {
                connect.Open();
                com = new MySqlCommand(q);
                com.Connection = connect;
                da = new MySqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (MySqlException mye)
            {
                //error.createErrorLog(mye.Message, mye.Source, "MySQL Error");
                closeConnection();
                return null;
            }
            catch (Exception e)
            {
                //error.createErrorLog(e.Message, e.Source, "Data Downloading Error");
                closeConnection();
                return null;
            }
        }

        public static void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
                connect.Dispose();
            }
        }
    }
}
