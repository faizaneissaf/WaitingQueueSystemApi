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
    public class QueuehandlerController : ApiController
    {
        BiitProjectQueueSystemEntities9 db = new BiitProjectQueueSystemEntities9();
        //-------fyp1 all meetings
        [HttpGet]
        public HttpResponseMessage allMeetings()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z=>z.fyp==1 && z.meeting_status==0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------Female groups
        [HttpGet]
        public HttpResponseMessage femalegroupMeetings()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 1 && z.std_gender=="F" && z.meeting_status==0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------male groups
        [HttpGet]
        public HttpResponseMessage malegroupMeetings()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 1 && z.std_gender == "M" && z.meeting_status==0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------Sort By Supervisor
        [HttpGet]
        public HttpResponseMessage sortbySupervisorsMeetings(string sname)
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 1 && z.std_supervisor == sname && z.meeting_status==0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Meeting details
        [HttpGet]
        public HttpResponseMessage meetingDetails(int groupid)
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 1 && z.group_no == groupid ).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Call meeting
        [HttpGet]
        public HttpResponseMessage callgroupfyp1(int groupid)
        {
            try
            {
                List<MeetingSchedule> results= (from m in db.MeetingSchedules
                                                where m.fyp == 1 && m.group_no == groupid
                                                select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 1;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Called");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Skip meeting
        [HttpGet]
        public HttpResponseMessage skipgroupfyp1(int groupid)
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 1 && m.group_no == groupid
                                                 select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 2;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Skiped");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Cancel meeting
        [HttpGet]
        public HttpResponseMessage cancelgroupfyp1(int groupid)
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 1 && m.group_no == groupid
                                                 select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 3;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cancel");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----------Delay 
        [HttpGet]
        public HttpResponseMessage delaygroupfyp1()
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 1 && m.meeting_status == 0
                                                 select m).ToList();
                //var results = db.MeetingSchedules.Where(x => x.fyp == 2 && x.meeting_status == 0).Select(yy=>yy.meeting_time).ToList();
                var time = "";
                //List<String> timing = new List<String>();
                //timing = results;
                //List<String> tr = new List<String>();
                //for (int i = 0; i < timing.Count(); i++)
                //{
                //    string[] time2 = { ":", " " };
                //    Int32 count = 3;
                //    string[] lst = timing[i].Split(time2, count, StringSplitOptions.RemoveEmptyEntries);
                //    string zz = lst[0];
                //    string zzz = lst[1];
                //    int hour = Int32.Parse(zz);
                //    int mins = Int32.Parse(zzz);
                //    int ph = 2;
                //    int pm = 10;
                //    hour += ph;
                //    mins += pm;
                //    time = hour.ToString() + ":" + mins.ToString() + " " + lst[2];
                //    db.SaveChanges();
                //}
                foreach (var i in results)
                {
                    time = i.meeting_time;
                    string[] time2 = { ":", " " };
                    Int32 count = 3;
                    string[] lst = time.Split(time2, count, StringSplitOptions.RemoveEmptyEntries);
                    string zz = lst[0];
                    string zzz = lst[1];
                    int hour = Int32.Parse(zz);
                    int mins = Int32.Parse(zzz);
                    int ph = 2;
                    int pm = 10;
                    hour += ph;
                    mins += pm;
                    time = hour.ToString() + ":" + mins.ToString() + " " + lst[2];
                    i.meeting_time = time;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        ////////----------------------------------------------------
        ///[HttpGet]
        public HttpResponseMessage delaygroupfyp2()
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 2 && m.meeting_status == 0
                                                 select m).ToList();
                //var results = db.MeetingSchedules.Where(x => x.fyp == 2 && x.meeting_status == 0).Select(yy=>yy.meeting_time).ToList();
                var time = "";
                //List<String> timing = new List<String>();
                //timing = results;
                //List<String> tr = new List<String>();
                //for (int i = 0; i < timing.Count(); i++)
                //{
                //    string[] time2 = { ":", " " };
                //    Int32 count = 3;
                //    string[] lst = timing[i].Split(time2, count, StringSplitOptions.RemoveEmptyEntries);
                //    string zz = lst[0];
                //    string zzz = lst[1];
                //    int hour = Int32.Parse(zz);
                //    int mins = Int32.Parse(zzz);
                //    int ph = 2;
                //    int pm = 10;
                //    hour += ph;
                //    mins += pm;
                //    time = hour.ToString() + ":" + mins.ToString() + " " + lst[2];
                //    db.SaveChanges();
                //}
                foreach (var i in results)
                {
                    time = i.meeting_time;
                    string[] time2 = { ":", " " };
                    Int32 count = 3;
                    string[] lst = time.Split(time2, count, StringSplitOptions.RemoveEmptyEntries);
                    string zz = lst[0];
                    string zzz = lst[1];
                    int hour = Int32.Parse(zz);
                    int mins = Int32.Parse(zzz);
                    int ph = 2;
                    int pm = 10;
                    hour += ph;
                    mins += pm;
                    time = hour.ToString() + ":" + mins.ToString() + " " + lst[2];
                    i.meeting_time = time;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        ///------
        //-------FYP II functionalities
        [HttpGet]
        public HttpResponseMessage allMeetingsfyp2()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 2 && z.meeting_status == 0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------Female groups
        [HttpGet]
        public HttpResponseMessage femalegroupMeetingsfyp2()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 2 && z.std_gender == "F" && z.meeting_status == 0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------male groups
        [HttpGet]
        public HttpResponseMessage malegroupMeetingsfyp2()
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 2 && z.std_gender == "M" && z.meeting_status == 0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //--------Sort By Supervisor
        [HttpGet]
        public HttpResponseMessage sortbySupervisorsMeetingsfyp2(string sname)
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 2 && z.std_supervisor == sname && z.meeting_status == 0).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Meeting details
        [HttpGet]
        public HttpResponseMessage meetingDetailsfyp2(int groupid)
        {
            try
            {
                var m = db.MeetingSchedules.Where(z => z.fyp == 2 && z.group_no == groupid).Select(x => new
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
                    x.fyp
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, m);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Call meeting
        [HttpGet]
        public HttpResponseMessage callgroupfyp2(int groupid)
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 2 && m.group_no == groupid
                                                 select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 1;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Called");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Skip meeting
        [HttpGet]
        public HttpResponseMessage skipgroupfyp2(int groupid)
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 2 && m.group_no == groupid
                                                 select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 2;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Skiped");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Cancel meeting
        [HttpGet]
        public HttpResponseMessage cancelgroupfyp2(int groupid)
        {
            try
            {
                List<MeetingSchedule> results = (from m in db.MeetingSchedules
                                                 where m.fyp == 2 && m.group_no == groupid
                                                 select m).ToList();
                foreach (var item in results)
                {
                    item.meeting_status = 3;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cancel");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---------------File Upload test
        [Route("upload")]
        [HttpPost]
        public HttpResponseMessage uploadFile()
        {
            try
            {
                var request = HttpContext.Current.Request;
                var description = request.Form["description"];
                var file = request.Files["file"];
                file.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads" + file.FileName));

                return Request.CreateResponse(HttpStatusCode.OK, "Uploaded");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}