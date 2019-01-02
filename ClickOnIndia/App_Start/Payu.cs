using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ClickOnIndia.App_Start
{
    public class PayU
    {

        public string CheckOut(string PayuKey, string PayuSalt, string PayuUrl, string PayuSuccessUrl, string PayuFailUrl, double Amount, string TransId, string StudentFirstName,
           string FeeDetails, string ParentEmail, string ParentMobNo)
        {
            Random random = new Random();
            int Value = random.Next(10000, 20000);
            string TranId = TransId + "_" + Value;
            String text = PayuKey + "|" + TranId + "|" + Amount + "|" + FeeDetails + "|" + StudentFirstName + "|" + ParentEmail + "|" + "1" + "|" + "1" + "|" + "1" + "|" + "1" + "|" + "1" + "||||||" + PayuSalt;
            //Response.Write(text);
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }


            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            data.Add("hash", hex.ToString());
            data.Add("txnid", TranId);
            data.Add("key", PayuKey);
            data.Add("amount", Amount);
            data.Add("firstname", StudentFirstName.Trim());
            data.Add("email", ParentEmail.Trim());
            data.Add("phone", ParentMobNo.Trim());
            data.Add("productinfo", FeeDetails.Trim());
            data.Add("udf1", "1");
            data.Add("udf2", "1");
            data.Add("udf3", "1");
            data.Add("udf4", "1");
            data.Add("udf5", "1");

            data.Add("surl", PayuSuccessUrl);
            data.Add("furl", PayuFailUrl);

            data.Add("service_provider", "");


            string strForm = PreparePOSTForm(PayuUrl, data);
            return strForm;

        }
        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }

    }
}