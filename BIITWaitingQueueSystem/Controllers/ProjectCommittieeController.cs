using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using Aspose.Cells;
using Workbook = Aspose.Cells.Workbook;
using BIITWaitingQueueSystem.Models;

namespace BIITWaitingQueueSystem.Controllers
{
    [RoutePrefix("api/file")]
    public class ProjectCommittieeController : ApiController
    {
        BiitProjectQueueSystemEntities9 db = new BiitProjectQueueSystemEntities9();
        //--------Read File
        [HttpGet]
        public HttpResponseMessage readExcelSheet()
        {
            try
            {
                var workbook = new Workbook("F:/FreeLance-Projects/Api/BIITWaitingQueueSystem/BIITWaitingQueueSystem/Content/Uploads/MeetingSchedulesFYPII.xlsx");
                workbook.Save("F:/FreeLance-Projects/Api/BIITWaitingQueueSystem/BIITWaitingQueueSystem/Content/Uploads/Output.json");
                StreamReader r = new StreamReader("F:/FreeLance-Projects/Api/BIITWaitingQueueSystem/BIITWaitingQueueSystem/Content/Uploads/Output.json");
                string jsonString = r.ReadToEnd();

                var objResponse1 = JsonConvert.DeserializeObject<List<Student>>(jsonString);
                foreach (Student obj in objResponse1)
                {
                    MeetingSchedule m = new MeetingSchedule();
                    m.group_no = obj.group_no;
                    m.reg_no = obj.reg_no;
                    m.std_name = obj.std_name;
                    m.std_gender = obj.std_gender;
                    m.std_class = obj.std_class;
                    m.std_supervisor = obj.std_supervisor;
                    m.project_title = obj.project_title;
                    m.technology = obj.technology;
                    m.meeting_time = obj.meeting_time;
                    m.meeting_date = obj.meeting_date;
                    m.meeting_status = obj.meeting_status;
                    m.fyp = obj.fyp;
                    m.remarks = "No Remarks";
                    db.MeetingSchedules.Add(m);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Successfull Imported");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------------
        //-----------Update date
        [HttpGet]
        public HttpResponseMessage updateDate(String fdate,String sdate) {
            try
            {
                //var alld = from data in db.MeetingSchedules select data;
                var alld = db.MeetingSchedules.Select(x => x).Take(192);
                var list = db.MeetingSchedules.Select(y=>y).ToList();
                //var alld2 = db.MeetingSchedules.Select(x => x).Skip(192).Take(101); ;
                var alld2= list.Skip(Math.Max(0, list.Count() - 192)).Take(208);
                foreach (var x in alld)
                {
                    x.meeting_date = fdate;
                }
                foreach (var z in alld2)
                {
                    z.meeting_date = sdate;
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully Updated");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------------------
        //----Delete All Meetings
        [HttpGet]
        public HttpResponseMessage deleteAll()
        {
            try
            {
                //var alld = from data in db.MeetingSchedules select data;
                var alld = db.MeetingSchedules.Select(x=>x).ToList();
                foreach (var x in alld)
                {
                    db.MeetingSchedules.Remove(x);
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully Deleted");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Update date with from to 
        [HttpGet]
        public HttpResponseMessage fromto(int group1 , int group2, String date)
        {
            try
            {
                var um = db.MeetingSchedules.Where(x => x.group_no >= group1 && x.group_no<=group2).ToList();
                foreach (var x in um)
                {
                    x.meeting_date = date;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, um);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Update date with from to 
        [HttpGet]
        public HttpResponseMessage fromtowithS(String sup1,String sup2, String date)
        {
            try
            {
                var um= db.MeetingSchedules.Where(x => x.std_supervisor.StartsWith(sup1)).ToList();
                var um1 = db.MeetingSchedules.Where(x => x.std_supervisor.StartsWith(sup2)).ToList();

                foreach (var x in um)
                {
                    x.meeting_date = date;
                    db.SaveChanges();
                }
                foreach (var x in um1)
                {
                    x.meeting_date = date;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, um);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Import Users
        [HttpGet]
        public HttpResponseMessage readUsers()
        {
            try
            {
                var workbook = new Workbook("F:/FreeLance-Projects/Material/Arslan/Users.xlsx");
                workbook.Save("F:/FreeLance-Projects/Material/Arslan/Output2.json");
                StreamReader r = new StreamReader("F:/FreeLance-Projects/Material/Arslan/Output2.json");
                string jsonString = r.ReadToEnd();

                var objResponse1 = JsonConvert.DeserializeObject<List<Users>>(jsonString);
                foreach (Users obj in objResponse1)
                {
                    User u = new User();
                    u.user_id = obj.user_id;
                    u.user_name = obj.user_name;
                    u.user_email = obj.user_email;
                    u.user_password = obj.user_password;
                    u.user_type = obj.user_type;
                    db.Users.Add(u);
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Successfull Imported");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        ///---Write to Excel 
        [HttpPost]
        public HttpResponseMessage FileUpload()
        {
            //try
            //{

                var request = HttpContext.Current.Request;


                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));

                Excel.Application myexcelApplication = new Excel.Application();
                if (myexcelApplication != null)
                {
                    Excel.Workbook myexcelWorkbook = myexcelApplication.Workbooks.Add();
                    Excel.Worksheet myexcelWorksheet = (Excel.Worksheet)myexcelWorkbook.Sheets.Add();

                    myexcelWorksheet.Cells[1, 1] = photo;


                    myexcelApplication.ActiveWorkbook.SaveAs(@"C:\abc.xls", Excel.XlFileFormat.xlWorkbookNormal);

                    myexcelWorkbook.Close();
                    myexcelApplication.Quit();
                }
                //return new HttpResponseMessage(HttpStatusCode.OK);
                return Request.CreateResponse(HttpStatusCode.OK," Upload" );
            //}
            //catch (Exception x)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            //}
        }
        //----------------File Upload From Tutorial
        //[Route("upload")]
        [HttpPost]
        public HttpResponseMessage uploadFile()
        {
            try
            {
                var request = HttpContext.Current.Request;
                var description = request.Form["description"];
                var file = request.Files["file"];
                file.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + file.FileName));

                //readExcelSheet();

                return Request.CreateResponse(HttpStatusCode.OK, "Uploaded");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------------Allgroups Info 
        [HttpGet]
        public HttpResponseMessage allgroups(int fypid)
        {
            try
            {
                var q = db.MeetingSchedules.Where(z => z.fyp == 1).Select(x => new
                {
                    x.meeting_id,
                    x.group_no,
                    x.reg_no,
                    x.std_name,
                    x.std_gender,
                    x.std_class,
                    x.std_supervisor,
                    x.project_title,
                    x.technology,
                    x.meeting_time,
                    x.meeting_date,
                    x.meeting_status,
                    x.fyp,
                    x.remarks
                }).ToList();
                var q2 = db.MeetingSchedules.Where(z => z.fyp == 2).Select(x => new
                {
                    x.meeting_id,
                    x.group_no,
                    x.reg_no,
                    x.std_name,
                    x.std_gender,
                    x.std_class,
                    x.std_supervisor,
                    x.project_title,
                    x.technology,
                    x.meeting_time,
                    x.meeting_date,
                    x.meeting_status,
                    x.fyp,
                    x.remarks
                }).ToList();
                if (fypid == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, q);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, q2);
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
    //----------------Class for Excel Sheet data
    public class Student
    {
        public int group_no { get; set; }
        public string reg_no { get; set; }
        public string std_name { get; set; }
        public string std_gender { get; set; }
        public string std_class { get; set; }
        public string std_supervisor { get; set; }
        public string project_title { get; set; }
        public string technology { get; set; }
        public string meeting_time { get; set; }
        public string meeting_date { get; set; }
        public int meeting_status { get; set; }
        public int fyp { get; set; }
    }
    public class Users
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_password { get; set; }
        public int user_type { get; set; }
    }
}
