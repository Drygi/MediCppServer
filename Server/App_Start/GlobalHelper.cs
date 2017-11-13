using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Server.App_Start
{
    public static class GlobalHelper
    {
        public static string DecryptRSA(Authorize authorize)
        {
            try
            {
                var bytesToDecrypt = Convert.FromBase64String(authorize.cipherData.Trim());
                AsymmetricCipherKeyPair keyPair;
                var decryptEngine = new Pkcs1Encoding(new RsaEngine());
                using (var txtreader = new StringReader(authorize.privateKey.Trim()))
                {
                    keyPair = (AsymmetricCipherKeyPair)new PemReader(txtreader).ReadObject();

                    decryptEngine.Init(false, keyPair.Private);
                }
                var decrypted = Encoding.UTF8.GetString(decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
                return decrypted;
            }
            catch (Exception)
            {

                return null;
            }
           
        }
    }
}
