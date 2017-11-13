using Server.App_Start;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    public class AuthorizeController : ApiController
    {
        private MediCppEntities2 db = new MediCppEntities2();

        // POST: api/Authorize
        public IHttpActionResult Authorize(Authorize authorize)
        { 

        var dec = GlobalHelper.DecryptRSA(authorize).Split('%');
            if (dec == null)
                return Json(new Status { status = "notAuthorized" });
            Doctor dr = new Doctor();
            dr.Name = dec[0];
            dr.LastName = dec[1];
            dr.PESEL = dec[2].ToString();

            var DFDB = (from d in db.Doctor where d.PESEL.ToString() == dr.PESEL select d).SingleOrDefault();

            if (DFDB != null && DFDB.CipherData.Trim() == authorize.cipherData.Trim())
                return Json(new Status { status = "Authorized" });
            else
                return Json(new Status { status = "notAuthorized" });
        }

        // PUT: api/Authorize/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authorize/5
        public void Delete(int id)
        {
        }
    }
}
