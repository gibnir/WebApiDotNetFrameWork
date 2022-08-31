using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using WebApiHttpClient.Utils;

namespace WebApiHttpClient.App_Layers
{
    public class DataAccessLayer
    {
        #region Singleton Implementaion
        private static DataAccessLayer mInstance;
        private DataAccessLayer() { }
        public static DataAccessLayer Self()
        {
            if (mInstance == null)
            {
                mInstance = new DataAccessLayer();
            }
            return mInstance;
        }
        #endregion singleton Implementaion

        #region Public Async Stored Procedure Function
        // GET method
        public async Task<DataTable> GetEmployeeListAsync1DB()
        {
            try
            {
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = await helper.ExecuteStoredProcedureAsync1("spEmployeeList");
                // DataTable dt = helper.ExecuteStoredProcedure("spEmployeeList");
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        // GET method
        public async Task<DataTable> GetEmployeeListAsync2DB()
        {
            try
            {
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = await helper.ExecuteStoredProcedureAsync2("spEmployeeList");
                // DataTable dt = helper.ExecuteStoredProcedure("spEmployeeList");
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        // GET method
        public async Task<DataTable> AttendanceCalculatorAsync1DB(int id)
        {
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = await helper.ExecuteStoredProcedureWithParamtersAsync1("spGetEmpAttendanceByID", sqlParams);
                Thread.Sleep(Utils.Tools.GetRandomNumber(10, 16));
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion Public Async Stored Procedure Function

        #region Public Stored Procedure Function
        // GET method
        public DataTable GetEmployeeListDB()
        {
            try
            {
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = helper.ExecuteStoredProcedure("spEmployeeList");
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }

        // GET method
        public DataTable GetEmployeeRolesDB(int id)
        {
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = helper.ExecuteStoredProcedureWithParamters("spGetEmployeeRolesByID", sqlParams);
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        // POST method
        public bool ClockInDB(int employeeId, int roleId, DateTime shiftStart, DateTime shiftEnd)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlParameters.Add(new SqlParameter("@RoleId", roleId));
                sqlParameters.Add(new SqlParameter("@ShiftStart", shiftStart));
                sqlParameters.Add(new SqlParameter("@shiftEnd", shiftEnd));

                SqlDBHelper helper = new SqlDBHelper();
                var res = helper.ExecuteStoredProcedureWithParamters("spInsertShift", sqlParameters);

                if ((int)res.Rows[0][0] >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        // GET method
        public DataTable AttendanceCalculatorDB(int id)
        {
            try
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                SqlDBHelper helper = new SqlDBHelper();
                DataTable dt = helper.ExecuteStoredProcedureWithParamters("spGetEmpAttendanceByID", sqlParams);
                Thread.Sleep(Utils.Tools.GetRandomNumber(10, 16));
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion Public Stored Procedure Function
    }
}