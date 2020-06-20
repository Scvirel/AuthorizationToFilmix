using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilmixPOST.Components
{
   class Requester
   {
        public  CookieContainer ZeroRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.GetResponse();
            request.Abort();
            return request.CookieContainer;
        }

        public  CookieContainer TryAuthorizeRequest(string url,CookieContainer container,string login,string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = container;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            string postData = $"login_name={login}&login_password={password}&login_not_save=1&login=submit";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);
            request.ContentLength = byte1.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream data = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string responseString = reader.ReadToEnd();
            }
            request.Abort();
            return request.CookieContainer;
        }

        public  string Request(string url,CookieContainer container)
        {
            string responseString="";
            string cookieString = GetCookieString(container);

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Cookie, cookieString);
                responseString = client.DownloadString(url);
            }

            return responseString;
        }

        private string GetCookieString(CookieContainer container)
        {
            string result = "";

            CookieCollection colection = container.GetCookies(new Uri("https://filmix.co/engine/ajax/user_auth.php"));

            foreach (Cookie cook in colection)
            {
                result += $"{cook.Name}={cook.Value};";
            }

            return result;
        }
   }
}
