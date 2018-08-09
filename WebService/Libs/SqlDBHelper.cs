using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Class help for connect database.
/// </summary>
public class SqlDBHelper
{
    public string ConnString = string.Empty;
    public SqlDBHelper()
    {
        ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
    /// <summary>
    /// Thực hiện store without parameter
    /// </summary>
    /// <param name="CommandName"></param>
    /// <param name="cmdType"></param>
    /// <returns></returns>
    private DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType)
    {
        DataTable table = null;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = CommandName;
                cmd.CommandType = cmdType;

                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
                    }
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                }
                catch
                {
                    throw;
                }
            }
        }
        return table;
    }
    /// <summary>
    /// Thực hiện store parameter
    /// </summary>
    /// <param name="commandName"></param>
    /// <param name="cmdType"></param>
    /// <param name="parameters">NULL nếu không có tham số</param>
    /// <returns></returns>
    public DataTable ExecuteCommand(string commandName, CommandType cmdType, SqlParameter[] parameters)
    {
        if (parameters == null)
        {
            return ExecuteSelectCommand(commandName, cmdType);
        }
        DataTable table = null;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandTimeout = 3000;
                cmd.CommandText = commandName;
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
                    }
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                }
                catch
                {
                    throw;
                }
            }
        }
        return table;
    }
    public DataSet ExecuteCommandDataSet(string commandName, CommandType cmdType, SqlParameter[] parameters)
    {
        DataSet ds = null;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandTimeout = 1500;
                cmd.CommandText = commandName;
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        ds = new DataSet();
                        adapter.Fill(ds);
                    }
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                }
                catch
                {
                    throw;
                }
            }
        }
        return ds;
    }
    /// <summary>
    /// Thực hiện query stored procedure 
    /// </summary>
    /// <returns>True: hoàn tất , False : fail</returns>
    public bool ExecuteWithoutResult(string CommandName, CommandType cmdType, SqlParameter[] parameters)
    {
        int result = 0;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandTimeout = 3000;
                cmd.CommandType = cmdType;
                cmd.CommandText = CommandName;
                cmd.Parameters.AddRange(parameters);

                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
        return (result > 0);
    }
    public DataTable ExecuteCommandReader(string CommandName, CommandType cmdType, SqlParameter[] parameters)
    {
        DataTable table = null;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = cmdType;
                cmd.CommandText = CommandName;
                cmd.Parameters.AddRange(parameters);
                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(dr);
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }
        return table;
    }
   
}