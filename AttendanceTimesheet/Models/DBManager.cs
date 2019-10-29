using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace AttendanceTimesheet.Models
{
    public class DBManager
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["AttendanceDBConnection"].ConnectionString;
        private SqlConnection _con;
        private SqlCommand _cmd;
        private string _tableName;
        public DBManager(string tableName)
        {
            _con = new SqlConnection(_connectionString);
            _cmd = new SqlCommand();
            _cmd.Connection = _con;
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _tableName = tableName;
        }
        public int GetTotalRecords()
        {
            _cmd.Parameters.Clear();

            if (_tableName == "SummaryTable")
                _cmd.CommandText = "SummaryGetTotalRecords";
            else if (_tableName == "DetailsTable")
                _cmd.CommandText = "DetailsGetTotalRecords";

            _con.Open();
            var val= Convert.ToInt32(_cmd.ExecuteScalar());
            _con.Close();
            return val;
        }
       public List<AttendanceRecord> GetRecords(int? pgno,int? entries)
        {
            if (_tableName == "SummaryTable")
                _cmd.CommandText = "SummaryPagination";
            else if (_tableName == "DetailsTable")
                _cmd.CommandText = "DetailsPagination";
            
            _cmd.Parameters.Clear();
            _cmd.Parameters.Add("@pg_no", SqlDbType.TinyInt).Value = pgno;
            _cmd.Parameters.Add("@entries", SqlDbType.TinyInt).Value = entries ?? DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);

            return MapData();
           
           
        }
      
        public List<AttendanceRecord> GetRecordsBySearch(int? month,int? year,int? PRNO)
        {
            _cmd.Parameters.Clear();

            if (_tableName == "SummaryTable")
                _cmd.CommandText = "SummarySearchByCriteria";
            else if (_tableName == "DetailsTable")
                _cmd.CommandText = "DetailsSearchByCriteria";

            _cmd.Parameters.Add("month", SqlDbType.Int).Value = month;
            _cmd.Parameters.Add("year", SqlDbType.Int).Value = year;
            _cmd.Parameters.Add("PRNo", SqlDbType.Int).Value = PRNO;

            return MapData();

        }

        public List<AttendanceRecord> MapData()
        {
            _con.Open();
            var reader = _cmd.ExecuteReader();
            var recordsList = new List<AttendanceRecord>();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var record = new AttendanceRecord();
                    record.PersonNR = (int)reader["PERSONNR"];
                    record.Logdate = (DateTime)reader["LOGDATE"];
                    record.TimeIn = reader["TIMEIN"] == DBNull.Value ? null : (TimeSpan?)reader["TIMEIN"];
                    record.TimeOut = reader["TIMEOUT"] == DBNull.Value ? null : (TimeSpan?)reader["TIMEOUT"];
                    record.Creation_date = (string)reader["CREATION_DATE"];
                    record.V_NUM2 = (byte)reader["V_NUM2"];
                    record.TimeInMachineCode = (string)reader["TIME_IN_MACHINE_CODE"];
                    record.TimeInMachineName = (string)reader["TIME_IN_MACHINE_NAME"];
                    record.TimeOutMachineCode = (string)reader["TIME_OUT_MACHINE_CODE"];
                    record.TimeOutMachineName = (string)reader["TIME_OUT_MACHINE_NAME"];

                    recordsList.Add(record);

                }
            }
            _con.Close();
            return recordsList;
        }
    }
}