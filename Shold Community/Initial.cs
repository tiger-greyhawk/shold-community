using Shold_Community.Entities;
using Shold_Community.Entities.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using static Shold_Community.Entities.ArmysToAttack;
using static Shold_Community.Entities.Villages;

namespace Shold_Community
{
    
    class Initial
    {
        private static string serverAddres = Properties.Settings.Default.Server;
        private static string serverPort = Properties.Settings.Default.Port;
        private static string login = Properties.Settings.Default.Login;
        private static string password = Properties.Settings.Default.Password;
        //private static string server = "http://192.168.10.110:8080/backend/";
        //private static string server = "http://91.122.191.190:8181/backend/";
        //private static string server = "http://localhost:8080/backend/";
        //private static string server = "http://192.168.10.25:8080/backend/";
        //private static string server = "http://"+serverAddres+":"+serverPort+"/api/";
        private static string server = "http://" + serverAddres + ":" + serverPort + "/islands/";
        //private static string server = "http://shold.tk:8181/backend/";
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
            string serverAddres = Properties.Settings.Default.Server;
            string serverPort = Properties.Settings.Default.Port;
            string login = Properties.Settings.Default.Login;
            string password = Properties.Settings.Default.Password;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            String postData = ("username="+login+"&password="+password+"&submit=Login");
            //String postData = ("username=Sergey_Kostin&password=test&submit=Login");
            //String postData = ("username=Farbraumdarstellun&password=test&submit=Login");
            //String postData = ("username=Yurik-888&password=test&submit=Login");
            //String postData = ("username=Pirate-01&password=test&submit=Login");
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
            request = (HttpWebRequest)WebRequest.Create(server + "login");
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
            Form1.online = true;
            string temp = reader.ReadToEnd();
            return temp;
            }

        public static Player GetMe()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "player/me?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //System.Net.ServicePointManager.Expect100Continue = false;

                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Player));
                return (Player)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())));
            }
            catch
            {
                return null;
            }
            
        }

        public static List<Player> GetPlayers()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "player/" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;

            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
            List<Player> players = ((List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))));
            return players;
        }

        public static Player GetPlayer(int playerId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "player/" + playerId + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;

            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Player));
            Player player = ((Player)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))));
            return player;
        }

        public static List<Friend> GetPlayerFriends(int playerId)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends/"+playerId+"?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;

            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Friend>));
            List<Friend> friends = ((List<Friend>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))));
            return friends;
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

        public static string GetPatternsFriends()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/friends?_type=json");
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

        public static Pattern GetPatternOne(int id)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/" + id + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Pattern));
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                Pattern clear = (Pattern)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                return clear;
            }
            catch
            {
                MessageBox.Show("Нет связи или неправильный запрос. На получении конкретного шаблона");
                return null;
            }
        }

        public static string GetPatternPhotoOne(int id)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id + "?_type=json");
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
                MessageBox.Show("Нет связи или неправильный запрос. На получении фото шаблона");
                return null;
            }



            //System.Net.ServicePointManager.Expect100Continue = false;


        }

        public static string GetPatternFormationOne(int id)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_formation/" + id + "?_type=json");
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
                MessageBox.Show("Нет связи или неправильный запрос. На получении файла шаблона");
                return null;
            }
        }

        public static string PostPattern(Pattern pattern)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Pattern));
            ser.WriteObject(stream1, pattern);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //                response.Close();
                //            }
                //            try
                //            {
                //                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
                //            }
                //            catch
                //            {
                //                MessageBox.Show("Нет связи или неправильный запрос");
                //                return null;
            }
        }

        public static string PostPatternPhoto(PatternPhoto patternPhoto)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PatternPhoto));
            ser.WriteObject(stream1, patternPhoto);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" +patternPhoto.patternId+ "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static string PostPatternFormation(PatternFormation patternFormation)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PatternFormation));
            ser.WriteObject(stream1, patternFormation);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_formation/" + patternFormation.patternId + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static string PutPattern(Pattern pattern)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Pattern));
            ser.WriteObject(stream1, pattern);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/" +pattern.id+ "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "PUT";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static void DeletePatternOne(int id)
        {
            
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/" + id + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "DELETE";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            //request.ContentLength = body.Length;
            //request.ContentLength = 0;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) ;
            /*
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }
            
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //    return reader.ReadToEnd();
            }
            */
        }

        public static List<RequestRes> GetRequestResources()
        {
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "requests/resources" + "?_type=json");
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

                request.AllowAutoRedirect = false;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestRes>));
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    string temp = reader.ReadToEnd();
                    List<RequestRes> clear = (List<RequestRes>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                    return clear;
                }
                catch
                {
                    //GetAuth();
                    //GetRequestResources();
                    
                    MessageBox.Show("Нет связи или неправильный запрос. На получении списка запросов");
                    return null;
                }
            }
        }

        public static List<RequestRes> GetNewRequestResources(long lastTimestamp)
        {
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "requests/resources/new/" +lastTimestamp+ "?_type=json");
                DateTime testing = new DateTime(lastTimestamp);
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

                request.AllowAutoRedirect = false;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestRes>));
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    string temp = reader.ReadToEnd();
                    List<RequestRes> clear = (List<RequestRes>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                    return clear;
                }
                catch
                {
                    //GetAuth();
                    //GetRequestResources();

                    MessageBox.Show("Нет связи или неправильный запрос. На проверке новых запросов");
                    return null;
                }
            }
        }

        public static string PostRequestRes(RequestRes requestRes)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RequestRes));
            ser.WriteObject(stream1, requestRes);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "requests/resources" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using ( HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //                response.Close();
                //            }
                //            try
                //            {
                //                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
                //            }
                //            catch
                //            {
                //                MessageBox.Show("Нет связи или неправильный запрос");
                //                return null;
            }
        }

        public static string PostSendRes(SendRes sendRes)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SendRes));
            ser.WriteObject(stream1, sendRes);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "send/resources" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }


        public static List<ArmyToAttack> GetPlansAttack(String secret)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "planattack/secret/"+secret+"?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<ArmyToAttack>));
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                List<ArmyToAttack> clear = (List<ArmyToAttack>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                return clear;
            }
            catch
            {
                //GetAuth();
                //GetRequestResources();

                MessageBox.Show("Нет связи или неправильный запрос. На получении списка атак");
                return null;
            }
        }

        public static string PostPlanAttack(ArmyToAttack armyToAttack)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ArmyToAttack));
            ser.WriteObject(stream1, armyToAttack);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends/invite/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "planattack" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }


        public static int GetPingDelay()
        {
            int pingDelay = 0;
            long pingTime = 0;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "ping/" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<ArmyToAttack>));
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                //List<ArmyToAttack> clear = (List<ArmyToAttack>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                //long temp2 = long.Parse(temp);
                //DateTime temp1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds(temp2);
                pingTime = long.Parse(temp);
                //return temp1;
                //return new DateTime();
            }
            catch
            {
                //GetAuth();
                //GetRequestResources();

                MessageBox.Show("Нет связи или неправильный запрос. На получении списка атак");
                //return new DateTime(0);
            }

            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(server + "ping/" +pingTime+ "?_type=json");
            request2.ContentType = "application/json;charset=UTF-8";
            request2.Method = "GET";
            request2.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request2.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request2.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request2.GetResponse();
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<ArmyToAttack>));
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                //List<ArmyToAttack> clear = (List<ArmyToAttack>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                //long temp2 = long.Parse(temp);
                //DateTime temp1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds(temp2);
                pingDelay = int.Parse(temp);
                //return temp1;
                //return new DateTime();
            }
            catch
            {
                //GetAuth();
                //GetRequestResources();

                MessageBox.Show("Нет связи или неправильный запрос. На получении списка атак");
                //return new DateTime(0);
            }

            return pingDelay;
        }

        public static List<Village> GetVillages()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "village?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Village>));
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                List<Village> clear = (List<Village>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                return clear;
            }
            catch
            {
                //GetAuth();
                //GetRequestResources();

                MessageBox.Show("Нет связи или неправильный запрос. На получении списка деревень");
                return null;
            }
        }

        public static string PostVillage(Village village)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Village));
            ser.WriteObject(stream1, village);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "village" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static void DeleteVillageOne(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "village/" + id + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "DELETE";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            //request.ContentLength = body.Length;
            //request.ContentLength = 0;
            System.Net.ServicePointManager.Expect100Continue = false;
            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            HttpWebResponse response;// = new HttpWebResponse();
                
                try
                {
                response = (HttpWebResponse)request.GetResponse();
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    
                    
                    //    return reader.ReadToEnd();
                }
                catch (WebException exception)
                {
                    { MessageBox.Show("Деревня используется"); }
                }
            
            
            /*
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }
            
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //    return reader.ReadToEnd();
            }
            */
        }

        public static string PostFriends(Player player)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Player));
            ser.WriteObject(stream1, player);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close(); 
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static string DeleteFromFriends(Player player)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Player));
            ser.WriteObject(stream1, player);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "DELETE";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static string PostInvite(Player player)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Player));
            ser.WriteObject(stream1, player);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();

            byte[] body = Encoding.UTF8.GetBytes(json);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends/invite/" + id+"?_type=json");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "friends/invite" + "?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

            request.AllowAutoRedirect = false;
            request.ContentLength = body.Length;
            System.Net.ServicePointManager.Expect100Continue = false;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static List<AdminMessage> GetAdminMessages()
        {
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "message" + "?_type=json");
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

                request.AllowAutoRedirect = false;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<AdminMessage>));
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    string temp = reader.ReadToEnd();
                    List<AdminMessage> clear = (List<AdminMessage>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                    return clear;
                }
                catch
                {
                    //GetAuth();
                    //GetRequestResources();

                    MessageBox.Show("Нет связи или неправильный запрос. На админ. сообщении");
                    return null;
                }
            }
        }

        public static String GetOnlineServer()
        {
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "login" + "?_type=json");
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

                request.AllowAutoRedirect = false;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    /*
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<AdminMessage>));
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    string temp = reader.ReadToEnd();
                    List<AdminMessage> clear = (List<AdminMessage>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                    return clear;
                    */
                    return "online";
                }
                catch
                {
                    //GetAuth();
                    //GetRequestResources();

                    //MessageBox.Show("Нет связи или неправильный запрос");
                    return "offline";
                }
            }
        }

        public static void Logout()
        {
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "logout/" + "?_type=json");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.10.110:8080/api/logout");
                //request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                if (!String.IsNullOrEmpty(sCookies)) request.Headers.Add(HttpRequestHeader.Cookie, sCookies);

                request.AllowAutoRedirect = false;
                if (Form1.online == true)
                {
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        if (!String.IsNullOrEmpty(sCookies)) sCookies = null;
                        //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestRes>));
                        //Stream data = (Stream)response.GetResponseStream();
                        //StreamReader reader = new StreamReader(data, Encoding.UTF8);
                        //string temp = reader.ReadToEnd();
                        //List<RequestRes> clear = (List<RequestRes>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(temp)));
                        //                    return clear;
                    }
                    catch
                    {
                        MessageBox.Show("Нет связи или неправильный запрос. На выходе");
                        //                    return null;
                    }
                }
            }
        }

    }


}
