using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebService.Libs
{
    public class Common
    {
        public static string LOG_FILE_PATH = HostingEnvironment.MapPath("~/App_Data/Logs");

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static void WriteLog(string file, string message)
        {

            // Write logs to file
            try
            {
                if (!Directory.Exists(LOG_FILE_PATH + "/" + file))
                {
                    Directory.CreateDirectory(LOG_FILE_PATH + "/" + file);
                }
                var filePath = LOG_FILE_PATH + "/" + file + "/" + DateTime.Now.Year +
                    ((DateTime.Now.Month < 10) ? ("0" + DateTime.Now.Month) : DateTime.Now.Month.ToString()) +
                    ((DateTime.Now.Day < 10) ? ("0" + DateTime.Now.Day) : DateTime.Now.Day.ToString()) +
                    "_logs.txt";


                if (!File.Exists(filePath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine("---------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine(message);
                        sw.WriteLine("---------------------------------------");
                    }
                }
                else
                {
                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine("---------------------------------------");
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine(message);
                        sw.WriteLine("---------------------------------------");
                    }
                }
            }
            catch
            {

            }

        }
        public static DataTable ExecuteNonQuery(SqlCommand cmd)
        {
            using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
            {
                dt.SelectCommand = cmd;
                cmd.CommandTimeout = 100000;
                DataTable dtTemp = new DataTable();

                dt.Fill(dtTemp);

                return dtTemp;
            }
        }
        public static DataSet ExecuteDataSet(SqlCommand cmd)
        {
            using (SqlDataAdapter dt = new SqlDataAdapter(cmd))
            {
                dt.SelectCommand = cmd;
                cmd.CommandTimeout = 100000;
                DataSet dtTemp = new DataSet();

                dt.Fill(dtTemp);
                return dtTemp;
            }
        }

    }
}