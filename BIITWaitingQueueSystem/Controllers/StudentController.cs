using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BIITWaitingQueueSystem.Models;
namespace BIITWaitingQueueSystem.Controllers
{
    public class StudentController : ApiController
    {
        BiitProjectQueueSystemEntities9 db = new BiitProjectQueueSystemEntities9();
        
        //---std profile
        [HttpGet]
        public HttpResponseMessage userProfile(string email)
        {
            try
            {
                var data = db.Users.Where(x => x.user_email==email).Select(m => new {
                    m.user_id,
                    m.user_email,
                    m.user_name,
                    m.user_type
                });
                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Not Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //---Meeting Information
        [HttpGet]
        public HttpResponseMessage meetingInfoStudent(string regno)
        {
            try
            {
                var mi = db.MeetingSchedules.Where(x => x.reg_no == regno).Select(r => new
                {
                    r.meeting_id,
                    r.group_no,
                    r.reg_no,
                    r.std_name,
                    r.std_class,
                    r.std_supervisor,
                    r.project_title,
                    r.technology,
                    r.meeting_time,
                    r.meeting_date,
                    r.meeting_status
                }).ToList();
                if (mi!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, mi);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Nothing found");
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }
        //-----Request Message
        [HttpPost]
        public HttpResponseMessage sendreqMessage(Reschedule_Requests rsr)
        {
            try
            {
                db.Reschedule_Requests.Add(rsr);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, rsr);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
                throw;
            }
        }
    }
}
