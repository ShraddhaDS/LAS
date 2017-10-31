using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class DBUtility
    {


        private static string connectionString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
      //  private static string connectionString = Common.QueryStringModule.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        static DBUtility()
        {
        }

        /// <summary>
        /// Returns numbers of rows affected
        /// </summary>
        /// <param name="cmd">SqlCommand Object</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(ref SqlCommand cmd, string spName)
        {
            SqlCommand _cmd = null;
            int _result = 0;

            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _result = _cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }
            return _result;


        }

        public static int ExecuteNonQueryInTrans(SqlCommand cmd, string spName)
        {
            SqlCommand _cmd = null;
            int _result = 0;

            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }

            SqlTransaction objTran = _cnn.BeginTransaction();

            _cmd = cmd;
            _cmd.Transaction = objTran;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _result = _cmd.ExecuteNonQuery();
                objTran.Commit();
                _cnn.Close();
            }
            catch (Exception ex)
            {
                objTran.Rollback();
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                GC.Collect();
            }
            return _result;


        }

        public static object ExecuteScalerInTrans(SqlCommand cmd, string spName)
        {
            object _result = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction objTran = con.BeginTransaction();

                try
                {
                    cmd.Connection = con;
                    cmd.Transaction = objTran;
                    _result = cmd.ExecuteScalar();
                    objTran.Commit();
                    con.Close();
                }
                catch (Exception ex)
                {
                    objTran.Rollback();
                    throw;
                }
            }
            return _result;
        }

        public static object ExecuteScaler(ref SqlCommand cmd, string spName)
        {
            SqlCommand _cmd = null;
            object _result = "";
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _result = _cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _result;

        }

        public static object Execute(string paramName, string ParamValue, string spName)
        {
            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }

            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;

            if ((paramName != null))
            {
                _cmd.Parameters.Add(paramName, SqlDbType.NVarChar, 50).Value = ParamValue;
            }

            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds, "BN_Table");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds.Tables["BN_Table"];
        }

        public static DataTable Execute(ref SqlCommand cmd, string spName)
        {
            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = cmd;
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds, "BN_Table");
            }
            catch (Exception ex)
            {
                _cmd.Dispose();
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds.Tables["BN_Table"];
        }

        public static DataSet ExecuteDS(ref SqlCommand cmd, string spName)
        {

            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = cmd;
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds);
            }
            catch (Exception ex)
            {
                _cmd.Dispose();
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds;
        }

        public static DataSet ExecuteDSInTransaction(SqlCommand cmd, string spName)
        {

            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = cmd;
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);


            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            SqlTransaction objSqlTran = _cnn.BeginTransaction();
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.Transaction = objSqlTran;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds);
                objSqlTran.Commit();
            }
            catch (Exception ex)
            {
                objSqlTran.Rollback();
                _cmd.Dispose();
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds;
        }

        public static DataTable Execute(ref SqlCommand cmd, ref string strErr, string spName)
        {
            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = cmd;
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd = cmd;
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.StoredProcedure;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds, "BN_Table");
            }
            catch (Exception ex)
            {
                _cmd.Dispose();


                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds.Tables["BN_Table"];
        }

        public static object Execute(string spName)
        {
            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd.Connection = _cnn;
            _cmd.CommandText = spName;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.Text;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds, "BN_Table");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds.Tables["BN_Table"];
        }

        public static DataTable ExecuteSQLQuery(string sqlQuery)
        {
            SqlCommand _cmd = null;
            SqlDataAdapter _adapter = null;
            DataSet _ds = null;
            SqlConnection _cnn = null;
            _cmd = new SqlCommand();
            _adapter = new SqlDataAdapter();
            _ds = new DataSet();
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }
            _cmd.Connection = _cnn;
            _cmd.CommandText = sqlQuery;
            _cmd.CommandTimeout = 0;
            _cmd.CommandType = CommandType.Text;
            _adapter.SelectCommand = _cmd;
            _ds = new DataSet();
            try
            {
                _adapter.Fill(_ds, "BN_Table");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                _cmd.Dispose();
                _adapter.Dispose();
                GC.Collect();
            }

            return _ds.Tables["BN_Table"];
        }

        public static void ExecuteBulk(DataTable sourceTable, string distinationTable)
        {
            SqlConnection _cnn = null;
            _cnn = new SqlConnection(connectionString);
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_cnn))
                {
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.DestinationTableName = distinationTable;
                    bulkCopy.WriteToServer(sourceTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                GC.Collect();
            }
        }

        public static bool Truncate(string sourceTable)
        {
            SqlConnection _cnn = null;
            _cnn = new SqlConnection(connectionString);
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            if ((_cnn.State == ConnectionState.Open))
            {
                _cnn.Close();
            }
            else
            {
                _cnn.Open();
            }

            cmd.CommandText = "Truncate table " + sourceTable;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _cnn;
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if ((_cnn.State == ConnectionState.Open))
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
                GC.Collect();
            }
            return true;
        }

        public static bool ExecuteBulkCopy(string sourceFile, string sourceTable)
        {
            try
            {

                System.Data.OleDb.OleDbConnection MyConnection = null;
                string query = "provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + sourceFile + "'; Extended Properties='text;HDR=Yes;FMT=Delimited(,)';";
                MyConnection = new System.Data.OleDb.OleDbConnection(query);
                System.Data.OleDb.OleDbCommand MyCommand = new System.Data.OleDb.OleDbCommand("SELECT * FROM [{0}]", MyConnection);
                MyConnection.Open();
                System.Data.OleDb.OleDbDataReader rdr = MyCommand.ExecuteReader();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString);
                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.DestinationTableName = sourceTable;
                bulkCopy.WriteToServer(rdr);
                rdr.Close();

                MyConnection.Close();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ExecuteBulkCopyOnDataTable(DataTable table, string sourceTable)
        {
            try
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString);
                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.DestinationTableName = sourceTable;
                bulkCopy.WriteToServer(table);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ExecuteBulkCopy_xlsx(string sourceFile, string sourceTable)
        {
            try
            {
                System.Data.OleDb.OleDbConnection MyConnection = null;
                string query = "provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + sourceFile + "'; Extended Properties=\"Excel 12.0 Xml;HDR=Yes\\\"";
                MyConnection = new System.Data.OleDb.OleDbConnection(query);
                System.Data.OleDb.OleDbCommand MyCommand = new System.Data.OleDb.OleDbCommand("select * from [Sheet1$]", MyConnection);
                MyConnection.Open();

                using (System.Data.OleDb.OleDbDataReader rdr = MyCommand.ExecuteReader())
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString);
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.DestinationTableName = sourceTable;
                    bulkCopy.WriteToServer(rdr);
                    rdr.Close();
                }
                MyConnection.Close();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                GC.Collect();

            }
            return true;
        }




    }
}
