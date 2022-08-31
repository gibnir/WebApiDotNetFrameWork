using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHttpClient.App_Layers
{
    public class BusinessLogicLayer
    {
        #region Singleton Implementaion
        private static BusinessLogicLayer mInstance;
        private BusinessLogicLayer() { }

        public static BusinessLogicLayer Self()
        {
            if (mInstance == null)
            {
                mInstance = new BusinessLogicLayer();
            }
            return mInstance;
        }

        #endregion singleton Implementaion

        #region Public Async Function
        // GET method
        public async Task<DataTable> GetEmployeeListAsync1BL()
        {
            return await DataAccessLayer.Self().GetEmployeeListAsync1DB();
        }

        // GET method
        public async Task<DataTable> GetEmployeeListAsync2BL()
        {
            return await DataAccessLayer.Self().GetEmployeeListAsync2DB();
        }

        public async Task<DataTable> AttendanceCalculatorAsync1BL(int id)
        {
            return await DataAccessLayer.Self().AttendanceCalculatorAsync1DB(id);
        }
        #endregion Public Async Function

        #region Public Function
        // GET method
        public DataTable GetEmployeeListBL()
        {
            return DataAccessLayer.Self().GetEmployeeListDB();
        }

        // GET method
        public DataTable GetEmployeeRolesBL(int id)
        {
            return DataAccessLayer.Self().GetEmployeeRolesDB(id);
        }

        // POST method
        public bool ClockInBL(int employeeId, int roleId, DateTime shiftStart, DateTime shiftEnd)
        {
            return DataAccessLayer.Self().ClockInDB(employeeId, roleId, shiftStart, shiftEnd);
        }

        // GET method
        public DataTable AttendanceCalculatorBL(int id)
        {
            return DataAccessLayer.Self().AttendanceCalculatorDB(id);
        }
        #endregion Public Function
    }
}