using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTimesheet.Models
{
    public class SearchCustomerViewModel
    {
        public int totalCount { get; set; }
        public IEnumerable<AttendanceRecord> AttendanceRecords { get; set; }

        public SearchCriteria SearchCriteria { get; set; }

    }
}