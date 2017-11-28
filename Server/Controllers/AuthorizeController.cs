using Server.App_Start;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Server.Controllers
{
    public class AuthorizeController : ApiController
    {
        private MediCppEntities3 db = new MediCppEntities3();

        //[HttpGet]
        //public IHttpActionResult Authorize()
        //{
        //   var s = GlobalHelper.EncryptTextAES("Authorized", "123abc");

        //    var z = GlobalHelper.DecryptTextAES(s, "123abc");
        //    return Json(s+" "+z);
        //}
        // POST: api/Authorize
        [HttpPost]
        public IHttpActionResult Authorize(Authorize authorize)
        {
            var dec = GlobalHelper.DecryptRSA(authorize).Split('%');
            if (dec == null)
                return NotFound();
            Doctor dr = new Doctor();
            dr.Name = dec[0];
            dr.LastName = dec[1];
            dr.PESEL = dec[2].ToString();

            var DFDB = (from d in db.Doctor where d.PESEL.ToString() == dr.PESEL select d).SingleOrDefault();

            if (DFDB != null && DFDB.CipherData.Trim() == authorize.cipherData.Trim())

                return Json(MyAES.EncryptStringToBytes(DFDB.Id.ToString()));

            else
                return NotFound();
        }
    }
}
