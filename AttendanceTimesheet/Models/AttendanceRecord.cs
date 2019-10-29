using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AttendanceTimesheet.Models
{
    public class AttendanceRecord
    {
        
        public int PersonNR { get; set; }

        public DateTime Logdate { get; set; }
           
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public string Creation_date { get; set; }
        public byte V_NUM2  { get; set; }
        public string TimeInMachineCode { get; set; }
        public string TimeInMachineName { get; set; }
        public string TimeOutMachineCode { get; set; }
        public string TimeOutMachineName { get; set; }

    }
}