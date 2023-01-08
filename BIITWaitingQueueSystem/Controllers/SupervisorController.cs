using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BIITWaitingQueueSystem.Models;

namespace BIITWaitingQueueSystem.Controllers
{
    public class SupervisorController : ApiController
    {
        BiitProjectQueueSystemEntities9 db = new BiitProjectQueueSystemEntities9();
        //----My gROUPS
        [HttpGet]
        public HttpResponseMessage myGroups(string sup)
        {
            try
            {
                var q = db.MeetingSchedules.Where(z => z.std_supervisor == sup && z.meeting_status == 0).Select(x => new
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
                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        ///-----fyp sort
        [HttpGet]
        public HttpResponseMessage myGroupsfyp(string sup,int fypid)
        {
            try
            {
                var q = db.MeetingSchedules.Where(z => z.std_supervisor == sup && z.meeting_status == 0 && z.fyp==1).Select(x => new
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
                var q2 = db.MeetingSchedules.Where(z => z.std_supervisor == sup && z.meeting_status == 0 && z.fyp == 2).Select(x => new
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
                if (fypid==0)
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
        //------Remarks
        [HttpGet]
        public HttpResponseMessage remarkStd(String stdarid, String remarks)
        {
            try
            {
                var q = db.MeetingSchedules.FirstOrDefault(z => z.reg_no == stdarid);
                //var q2 = db.MeetingSchedules.FirstOrDefault(z => z.remarks == remarks);
                q.remarks = remarks;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //------Update Time
        [HttpGet]
        public HttpResponseMessage supFreetime(String stdarid, String time)
        {
            try
            {
                var q = db.MeetingSchedules.FirstOrDefault(z => z.reg_no == stdarid);
                //var q2 = db.MeetingSchedules.FirstOrDefault(z => z.remarks == remarks);
                q.meeting_time = time;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //----Student m info
        [HttpGet]
        public HttpResponseMessage stdMInfo(String stdarid)
        {
            try
            {
                var q = db.MeetingSchedules.Where(z => z.reg_no==stdarid).Select(x => new
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
                return Request.CreateResponse(HttpStatusCode.OK, q);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}
