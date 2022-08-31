using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiHttpClient.Utils
{
    class SqlDBHelper
    {
        #region Members
        private string connectionString = string.Empty;
        private static SqlDBHelper mInstance;
        #endregion Members

        #region Singleton implementation
        public SqlDBHelper()
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = GetconnectionString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // retrieve the Singleton instance
        public static SqlDBHelper Self()
        {
            if (mInstance == null)
            {
                mInstance = new SqlDBHelper();
            }
            return mInstance;
        }

        #endregion Singleton implementation

        #region Private Methods
        private string GetconnectionString()
        {
            string result = ConfigurationManager.AppSettings.Get("ConnectionString");
            return result;
        }
        #endregion Private Methods

        #region Public Async Methods
        public async Task<DataTable> ExecuteStoredProcedureAsync1(string spName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection objCon = new SqlConnection(this.connectionString);
            SqlCommand objCom = new SqlCommand(spName, objCon);
            objCom.Connection = objCon;
            objCom.CommandText = spName;
            objCom.CommandType = CommandType.StoredProcedure;

            try
            {
                objCon.Open();
                objCom.ExecuteNonQuery();
                //await Task.Run(() => objCom.ExecuteNonQuery());
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(objCom);
                // adapter.Fill(ds);
                await Task.Run(() => adapter.Fill(ds));
                if (ds.Tables.Count > 0)
                {
                    dataTable = ds.Tables[0];
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCon.Close();
            }
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync2(string spName)
        {
            DataTable table = new DataTable();
            using (SqlConnection objCon = new SqlConnection(this.connectionString))
            {
                SqlCommand objCom = new SqlCommand(spName, objCon);
                objCom.Connection = objCon;
                objCom.CommandText = spName;
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();

                using (var reader = await objCom.ExecuteReaderAsync())
                {

                    table.Load(reader);
                    objCon.Close();
                    return table;
                }
            }
        }

        public async Task<DataTable> ExecuteStoredProcedureWithParamtersAsync1(string spName, List<SqlParameter> spParams)
        {
            DataTable dataTable = new DataTable();
            SqlConnection objCon = new SqlConnection(this.connectionString);
            SqlCommand objCom = new SqlCommand();
            objCom.Connection = objCon;
            objCom.CommandText = spName;
            objCom.CommandType = CommandType.StoredProcedure;

            foreach (var param in spParams)
            {
                objCom.Parameters.Add(param);
            }

            try
            {
                objCon.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(objCom);
                // adapter.Fill(ds);
                await Task.Run(() => adapter.Fill(ds));
                if (ds.Tables.Count > 0) { dataTable = ds.Tables[0]; }
                return dataTable;
            }
            catch (Exception ex) { throw ex; }
            finally { objCon.Close(); }
        }
        #endregion Public Async Methods

        #region Public Methods
        public DataTable ExecuteStoredProcedure(string spName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection objCon = new SqlConnection(this.connectionString);
            SqlCommand objCom = new SqlCommand(spName, objCon);
            objCom.Connection = objCon;
            objCom.CommandText = spName;
            objCom.CommandType = CommandType.StoredProcedure;

            try
            {
                objCon.Open();
                objCom.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(objCom);
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dataTable = ds.Tables[0];
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCon.Close();
            }
        }

        /*
         public async Task<DataTable> ExecuteStoredProcedureAsync2(string spName)
        {
            using (SqlConnection objCon = new SqlConnection(this.connectionString))
            {
                SqlCommand objCom = new SqlCommand(spName, objCon);
                objCom.Connection = objCon;
                objCom.CommandText = spName;
                objCom.CommandType = CommandType.StoredProcedure;
                
                using (var reader = await objCom.ExecuteReaderAsync())
                {
                    var table = new DataTable();
                    table.Load(reader);

                    return table;
                }
            }
        }
             */

        //public async Task<DataTable> CallDb(string connStr, string sql)
        //{
        //    var dt = new DataTable();
        //    var connection = new SqlConnection(connStr);
        //    var reader = await connection.CreateCommand().ExecuteReaderAsync();
        //    dt.Load(reader);

        //    return dt;
        //}

        public List<string> ExecuteStoredProcedureWithOutputParams(string spName, List<SqlParameter> spParams, string[] outputParams)
        {
            List<string> list = new List<string>();
            SqlConnection objCon = new SqlConnection(this.connectionString);
            SqlCommand objCom = new SqlCommand();
            objCom.Connection = objCon;
            objCom.CommandText = spName;
            objCom.CommandType = CommandType.StoredProcedure;

            foreach (var param in spParams)
            {
                objCom.Parameters.Add(param);
            }

            try
            {
                objCon.Open();
                objCom.ExecuteNonQuery();
                for (int i = 0; i < outputParams.Length; i++)
                {
                    list.Add(objCom.Parameters[outputParams[i].ToString()].Value.ToString().Trim());
                }
                return list;
            }
            catch (Exception ex) { throw ex; }
            finally { objCon.Close(); }
        }

        public DataTable ExecuteStoredProcedureWithParamters(
            string spName, List<SqlParameter> spParams)
        {

            DataTable dataTable = new DataTable();
            SqlConnection objCon = new SqlConnection(this.connectionString);
            SqlCommand objCom = new SqlCommand();
            objCom.Connection = objCon;
            objCom.CommandText = spName;
            objCom.CommandType = CommandType.StoredProcedure;

            foreach (var param in spParams)
            {
                objCom.Parameters.Add(param);
            }

            try
            {
                objCon.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(objCom);
                adapter.Fill(ds);
                if (ds.Tables.Count > 0) { dataTable = ds.Tables[0]; }
                return dataTable;
            }
            catch (Exception ex) { throw ex; }
            finally { objCon.Close(); }
        }

        #endregion Public Methods
    }
}