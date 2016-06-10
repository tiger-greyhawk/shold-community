using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Shold_Community
{
    
    class Initial
    {
        //private static string server = "http://192.168.10.110:8080/backend/";
        private static string server = "http://91.122.191.190:8181/backend/";
        static HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "login");
        static string sCookies;
        public static  string GetAuth()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "login");
            //request.Method = "GET";
  //          CookieContainer cont = new CookieContainer();
//            request.CookieContainer = cont;
            //HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
            //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "player/me?_type=json");// 'username'='test'&&'password'='test'&&'submit'='Login'");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            String postData = ("username=Tiger_Greyhawk&password=test&submit=Login");
            Encoding encoding = Encoding.UTF8;
            byte[] postDataByte = encoding.GetBytes(postData);
            //request.ContentLength = postDataByte.Length;
            

            //            CookieContainer cont = new CookieContainer();
            //            request.CookieContainer = cont;


            
                        request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.Net.ServicePointManager.Expect100Continue = false;
            //            CookieCollection cc = new CookieCollection();
            //            cc = response.Cookies;
            //            cont.Add(cc);
//            if (response.Headers["Location"] != null)
//            {
//                MessageBox.Show("23");
                //CookieCollection cookies = new CookieCollection();
                //cookies = response.Cookies;

                /*                string[] cookieVal = null;
                                if (response.Headers["Set-Cookie"] != null)
                                    cookieVal = response.Headers["Set-Cookie"].Split(new char[] { ',' });
                                //string location = response.Headers["Location"].ToString();
                                CookieContainer cookie = new CookieContainer();
                                String cookieMy = "";
                                if (cookieVal.Length>0)
                                foreach (string cook in cookieVal)
                                {
                                    string[] cookie1 = cook.Split(new char[] { ';' });
                                    if (cookie1.Length < 2)
                                        continue;
                                    cookieMy = cookie1[0];
                                    //cookie.Add(new Cookie(cookie1[0].Split(new char[] { '=' })[0], cookie1[0].Split(new char[] { '=' })[1], cookie1[1].Split(new char[] { '=' })[1], cookie1.Length > 2 ? cookie1[2].Split(new char[] { '=' })[1] : ""));
                                }
                                */
                sCookies = String.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
                response.Close();
                request = null;
                //request = (HttpWebRequest)WebRequest.Create(location);
                request = (HttpWebRequest)WebRequest.Create(server+"login");
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request.Headers.Add("Referer", "http://localhost:8080/backend/login");
                //request.Connection = "keep-alive";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.ContentLength = postDataByte.Length;
                //                request.CookieContainer = new CookieContainer();
                /*foreach (Cookie oneCookie in cookies)
                {
                    request.CookieContainer.Add(oneCookie);
                }*/
                //request.CookieContainer = cookie;
                Stream st = request.GetRequestStream();
                st.Write(postDataByte, 0, postDataByte.Length);
                //st.Write(postDataByte, 0, postDataByte.Length);
                response = (HttpWebResponse)request.GetResponse();
  //              MessageBox.Show(response.ToString());
            //            }

            sCookies = String.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //requestStream.Close();
            //request.GetRequestStream().Close();
            //response.Close();

            return reader.ReadToEnd();
        }

        public static string GetMe()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "player/me?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;

            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static string GetPatterns()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;

            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static string GetPatternOne(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch
            {
                MessageBox.Show("Нет связи или неправильный запрос");
                return null;
            }
            
            //System.Net.ServicePointManager.Expect100Continue = false;

            
        }
    }
}
