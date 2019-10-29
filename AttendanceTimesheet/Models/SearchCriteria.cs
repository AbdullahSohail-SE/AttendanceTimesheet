using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTimesheet.Models
{
    public class SearchCriteria
    {

        public int? pgNo { get; set; } 
        [Display(Name ="Entries per page")]
        public int? entries { get; set; }

       

        //original
         [Display(Name = "Month")]
           public int? month { get; set; } 
           [Display(Name = "Year")]
           public int? year { get; set; } 
        
        [Display(Name = "Employee No")]
        public int? prNo { get; set; }
            
    }
}