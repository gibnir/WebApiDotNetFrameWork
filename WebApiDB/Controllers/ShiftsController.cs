using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using static WebApiHttpClient.Models.Models;
using WebApiHttpClient.App_Layers;

namespace WebApiHttpClient.Controllers
{
    /// <summary>
    /// GetEmployeeList - a GET method, no parameters.
    /// GetEmployeeRoles - a GET method, one parameter.
    /// ClockIn - this POST (http://127.0.0.1/api/Attendance/ClockIn) will receive a json from the client side in the form:
    /// {"employeeId": 123, "roleId": 1, "shiftStart": 30/01/2022 10:55", "shiftEnd": 30/01/2022 14:55"}
    /// Once the server receives the message, it will insert the record to the database to "attendance" table.You may use any date-time format you see fit
    /// AttendanceCalculator(Mid-Level Candidates) – a GET method, one parameter.
    /// example: http://127.0.0.1/api/Attendance/Calc?id=123
    /// The service will accumulate the time the employee has worked in each role.
    /// 
    /// The API will retrun a json.
    /// </summary>
    public class ShiftsController : ApiController
    {
        #region Async Controllers

        [Route("api/Shifts/GetEmployeeListAsync1")]
        [HttpGet]
        public async Task<List<Employee>> GetEmployeeListAsync1()
        {

            DataTable dt = new DataTable();
            dt = await BusinessLogicLayer.Self().GetEmployeeListAsync1BL();
            List<Employee> EmployeesList = new List<Employee>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee emp = new Employee()
                {
                    id = (int)dt.Rows[i]["id"],
                    Name = dt.Rows[i]["Name"].ToString()
                };
                EmployeesList.Add(emp);
            }
            return EmployeesList;
        }

        [Route("api/Shifts/GetEmployeeListAsync2")]
        [HttpGet]
        public async Task<List<Employee>> GetEmployeeListAsync2()
        {

            DataTable dt = new DataTable();
            dt = await BusinessLogicLayer.Self().GetEmployeeListAsync2BL();
            List<Employee> EmployeesList = new List<Employee>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee emp = new Employee()
                {
                    id = (int)dt.Rows[i]["id"],
                    Name = dt.Rows[i]["Name"].ToString()
                };
                EmployeesList.Add(emp);
            }
            return EmployeesList;
        }

        [Route("api/Shifts/AttendanceCalculatorAsync1/{id:int}")]
        [HttpGet]
        public async Task<List<ShiftTotalAttendence>> AttendanceCalculatorAsync1(int id)
        {

            DataTable dt = new DataTable();
            dt = await BusinessLogicLayer.Self().AttendanceCalculatorAsync1BL(id);
            List<ShiftTotalAttendence> EmployeeTotalAttendence = new List<ShiftTotalAttendence>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShiftTotalAttendence ShiftTotalHours = new ShiftTotalAttendence()
                {
                    EmployeeId = (int)dt.Rows[i]["EmployeeId"],
                    RoleId = (int)dt.Rows[i]["RoleId"],
                    DurationHours = (int)dt.Rows[i]["DurationHours"]
                };
                EmployeeTotalAttendence.Add(ShiftTotalHours);
            }
            return EmployeeTotalAttendence;
        }
        #endregion Async Controllers

        /// GetEmployeeList - a GET method, no parameters.
        /// GetEmployeeRoles - a GET method, one parameter.
        /// ClockIn - this POST (http://127.0.0.1/api/Attendance/ClockIn) will receive a json from the client side in the form:
        /// {"employeeId": 123, "roleId": 1, "shiftStart": 30/01/2022 10:55", "shiftEnd": 30/01/2022 14:55"}
        /// Once the server receives the message, it will insert the record to the database to "attendance" table.You may use any date-time format you see fit
        /// AttendanceCalculator(Mid-Level Candidates) – a GET method, one parameter.
        /// example: http://127.0.0.1/api/Attendance/Calc?id=123
        /// The service will accumulate the time the employee has worked in each role.

        [Route("api/Shifts/GetEmployeeList")]
        [HttpGet]
        public List<Employee> GetEmployeeList()
        {

            DataTable dt = new DataTable();
            dt = BusinessLogicLayer.Self().GetEmployeeListBL();
            List<Employee> EmployeesList = new List<Employee>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee emp = new Employee()
                {
                    id = (int)dt.Rows[i]["id"],
                    Name = dt.Rows[i]["Name"].ToString()
                };
                EmployeesList.Add(emp);
            }
            return EmployeesList;
        }

        [Route("api/Shifts/GetEmployeeRoles/{id:int}/Role")]
        [HttpGet]
        public List<Role> GetEmployeeRoles(int id)
        {

            DataTable dt = new DataTable();
            dt = BusinessLogicLayer.Self().GetEmployeeRolesBL(id);
            List<Role> EmployeeRolesList = new List<Role>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Role role = new Role()
                {
                    RoleId = (int)dt.Rows[i]["id"],
                    Description = dt.Rows[i]["Description"].ToString()
                };
                EmployeeRolesList.Add(role);
            }
            return EmployeeRolesList;
        }

        [Route("api/Shifts/ClockIn")]
        [HttpPost]
        // POST: api/Shifts
        public bool ClockIn([FromBody]ShiftAttendence value)
        {
            bool result = BusinessLogicLayer.Self().ClockInBL(
                value.RoleId,
                value.EmployeeId,
                DateTime.Parse(value.ShiftStart),
                DateTime.Parse(value.shiftEnd)
                );
            if (!result) { throw new Exception("DB fail to sav record!"); }
            return result;
        }

        [Route("api/Shifts/AttendanceCalculator/{id:int}")]
        [HttpGet]
        public List<ShiftTotalAttendence> AttendanceCalculator(int id)
        {

            DataTable dt = new DataTable();
            dt = BusinessLogicLayer.Self().AttendanceCalculatorBL(id);
            List<ShiftTotalAttendence> EmployeeTotalAttendence = new List<ShiftTotalAttendence>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShiftTotalAttendence ShiftTotalHours = new ShiftTotalAttendence()
                {
                    EmployeeId = (int)dt.Rows[i]["EmployeeId"],
                    RoleId = (int)dt.Rows[i]["RoleId"],
                    DurationHours = (int)dt.Rows[i]["DurationHours"]
                };
                EmployeeTotalAttendence.Add(ShiftTotalHours);
            }
            return EmployeeTotalAttendence;
        }
        // GET: api/Shifts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Shifts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Shifts
        public void Post([FromBody]string value)
        {
        }

        /*
        // GET: api/Shifts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Shifts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Shifts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Shifts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Shifts/5
        public void Delete(int id)
        {
        }
        */
    }
}
