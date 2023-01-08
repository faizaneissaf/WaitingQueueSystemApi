using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BIITWaitingQueueSystem.Models;

namespace BIITWaitingQueueSystem.Controllers
{
    public class LoginSignupController : ApiController
    {
        BiitProjectQueueSystemEntities9 db = new BiitProjectQueueSystemEntities9();
        [HttpGet]
        public HttpResponseMessage login(string email,string password)
        {
            try
            {
                var data = db.Users.FirstOrDefault(x => x.user_email == email && x.user_password == password);
                if (data!=null)
                {
                    if (data.user_type == 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Student");
                    }
                    else if (data.user_type == 1)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Supervisor");
                    }
                    else if (data.user_type == 2)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "QueueHandler");
                    }
                    else if (data.user_type == 3)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "ProjectCommittie");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid Email/Password");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Nothing Found");
                }
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
         }
        //-------Test
        [HttpGet]
        public HttpResponseMessage Test()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Okay");
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, x.Message);
            }
        }

    }
}
