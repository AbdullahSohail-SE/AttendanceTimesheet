using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceTimesheet.Models;


namespace AttendanceTimesheet.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance  
        public ActionResult Summary(SearchCriteria searchCriteria)
        {
            int totalcount = 0;
            IEnumerable<AttendanceRecord> recordsList;
            var manager = new DBManager("SummaryTable");

           
            

            if (searchCriteria.month.HasValue ||searchCriteria.year.HasValue||searchCriteria. prNo.HasValue) //hasvalue cahnaged
            {
                recordsList = manager.GetRecordsBySearch(searchCriteria.month,searchCriteria. year,searchCriteria. prNo);
                totalcount = recordsList.Count();
                    searchCriteria.pgNo = searchCriteria.pgNo.HasValue ? searchCriteria.pgNo.Value : 1;
                    searchCriteria.entries = searchCriteria.entries.HasValue ? searchCriteria.entries.Value : 10;

                recordsList = recordsList.Skip((searchCriteria.pgNo.Value - 1) * searchCriteria.entries.Value).Take(searchCriteria.entries.Value);
               
            }
            else
            {
                recordsList = manager.GetRecords(searchCriteria.pgNo,searchCriteria.entries);
                totalcount = manager.GetTotalRecords();
                
            }

            

            var searchCustomerViewModel = new SearchCustomerViewModel()
            {
                totalCount = totalcount,
                AttendanceRecords = recordsList,
                SearchCriteria = searchCriteria
            };

            return View(searchCustomerViewModel);
        }
        
        public ActionResult SummaryNextPage(SearchCriteria searchCriteria)
        {
            
            searchCriteria.pgNo += 1;

            return RedirectToAction("Summary", searchCriteria);
        }
        public ActionResult SummaryPrevPage(SearchCriteria searchCriteria)
        {
           
            searchCriteria.pgNo -= 1;

            return RedirectToAction("Summary", searchCriteria);
        }

        public ActionResult Details(SearchCriteria searchCriteria)
        {
            int totalcount = 0;
            IEnumerable<AttendanceRecord> recordsList;
            var manager = new DBManager("DetailsTable");




            if (searchCriteria.month.HasValue || searchCriteria.year.HasValue || searchCriteria.prNo.HasValue) //hasvalue cahnaged
            {
                recordsList = manager.GetRecordsBySearch(searchCriteria.month, searchCriteria.year, searchCriteria.prNo);
                totalcount = recordsList.Count();
                searchCriteria.pgNo = searchCriteria.pgNo.HasValue ? searchCriteria.pgNo.Value : 1;
                searchCriteria.entries = searchCriteria.entries.HasValue ? searchCriteria.entries.Value : 10;

                recordsList = recordsList.Skip((searchCriteria.pgNo.Value - 1) * searchCriteria.entries.Value).Take(searchCriteria.entries.Value);

            }
            else
            {
                recordsList = manager.GetRecords(searchCriteria.pgNo, searchCriteria.entries);
                totalcount = manager.GetTotalRecords();

            }



            var searchCustomerViewModel = new SearchCustomerViewModel()
            {
                totalCount = totalcount,
                AttendanceRecords = recordsList,
                SearchCriteria = searchCriteria
            };

            return View(searchCustomerViewModel);
        }

        public ActionResult DetailsNextPage(SearchCriteria searchCriteria)
        {
            searchCriteria.pgNo += 1;

            return RedirectToAction("Details", searchCriteria);
        }
        public ActionResult DetailsPrevPage(SearchCriteria searchCriteria)
        {
            searchCriteria.pgNo -= 1;

            return RedirectToAction("Details", searchCriteria);
        }
    }
}