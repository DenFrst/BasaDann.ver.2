using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasaDann.Classes
{
    public class Requests
    {

        private static string _req_single = "SELECT * FROM request req WHERE req.id = ";
        private static string _req_files = "SELECT * FROM request req INNER JOIN request_files reqf ON req.id = reqf.request_id INNER JOIN files fi ON reqf.file_id = fi.id where req.id = ";
        private static string _req_count_single = "SELECT count(*) FROM request req WHERE req.id = ";
        private static string _req_count_files = "SELECT count(*) FROM request req INNER JOIN request_files reqf ON req.id = reqf.request_id INNER JOIN files fi ON reqf.file_id = fi.id WHERE req.id = ";
        private static string _req_top_one = "SELECT id, name, company FROM request ORDER BY id DESC LIMIT 1";

        private static string _req_stat_all = "SELECT count(*) FROM request where Managers_id = 's.tikhomirov' and pid = 0";
        private static string _req_stat_closed = "and Status = \"Завершена\" ";

        private static string _req_stat_z =
            "SELECT null as status, count(*) as count, TIME_TO_SEC(lead_time) as time FROM request WHERE Managers_id = 's.tikhomirov' AND pid = 0 UNION ALL SELECT Status,count(Status), TIME_TO_SEC(lead_time) as time FROM request WHERE Managers_id = 's.tikhomirov' and pid = 0 and Status is not null group by Status";

        public static string Req_Single
        {
            get { return _req_single; }
            //set { _req_single = value;}
        }
        public static string Req_Files
        {
            get { return _req_files; }
            //set { _req_files = value; }
        }
        public static string Req_Count_Single
        {
            get { return _req_count_single; }
            //set { _req_count_single = value; }
        }
        public static string Req_Count_Files
        {
            get { return _req_count_files; }
            // set { _req_count_files = value; }
        }
        public static string Req_Top_One
        {
            get { return _req_top_one; }
            // set { _req_top_one = value; }
        }

        public static string Req_Stat_All
        {
            get { return _req_stat_all; }
            // set { _req_top_one = value; }
        }
        public static string Req_Stat_Closed
        {
            get { return _req_stat_closed; }
            // set { _req_top_one = value; }
        }
        public static string Req_Stat_Z
        {
            get { return _req_stat_z; }
            // set { _req_top_one = value; }
        }
    }
}
