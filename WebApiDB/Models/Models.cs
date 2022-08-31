using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHttpClient.Models
{
    public class Models
    {
        public class Employee
        {
            public int id { get; set; }
            public string Name { get; set; }
        }

        public class Role
        {
            public int RoleId { get; set; }
            public string Description { get; set; }
        }

        public class EmployeeRole
        {
            public int EmployeeId { get; set; }
            public int RoleId { get; set; }
            public int Enabled { get; set; }
        }

        public class ShiftAttendence
        {
            public int EmployeeId { get; set; }
            public int RoleId { get; set; }
            public string ShiftStart { get; set; }
            public string shiftEnd { get; set; }

            //public DateTime ShiftStart { get; set; }
            //public DateTime shiftEnd { get; set; }
        }

        public class ShiftTotalAttendence
        {
            public int EmployeeId { get; set; }
            public int RoleId { get; set; }
            public int DurationHours { get; set; }
        }
    }
}