using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Specialized;
//using Disk.SDK.Provider;
//using Disk.SDK;

namespace Shold_Community
{
    static class UploadScreenshot
    {

        public static String GetListFromYandexApi()
        {                                                                                                                               //ARb5DIcAAyM5KQRLGzYRTdCM3d69ZVPmuw
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/shold-community/?oauth_token=ARbxfUwAAyM5MvWK2GMERsymbP1h2_NS8A");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/shold-community/");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/sholdcommunity/");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/shold-community/?oauth_token=ARbxfUwAAyM5QcNtzQ7gSraQX3qmiKbSXg");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://oauth.yandex.ru/authorize?response_type=token&client_id=d4dac9ca4fa848ec91816792ce2b886f");
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "OAuth ARb5DIcAAyM5KQRLGzYRTdCM3d69ZVPmuw"); // sholdcommunity OAuth ARb5DIcAAyM5KQRLGzYRTdCM3d69ZVPmuw
            //Stream requestStream = request.GetRequestStream();                             // shold-community OAuth ARbxfUwAAyM5MvWK2GMERsymbP1h2_NS8A
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //var sdk = new DiskSdkClient("ARbxfUwAAyM5QcNtzQ7gSraQX3qmiKbSXg");

            Stream data = (Stream)response.GetResponseStream();
            //            }
            //sdk.GetListAsync("/");
            //data.Position = 0;
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //requestStream.Close();
            //request.GetRequestStream().Close();
            //response.Close();

            return reader.ReadToEnd();
            //return 
        }

        public static String PostImgToYandexApi(Bitmap bmp)//, CookieContainer cont)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/shold-community/photos/");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/sholdcommunity/photos/");

            const int diapason = 999999;
            const int diapason2 = 9999999;
            Random rand = new Random();

            string boundarynumber = rand.Next(diapason).ToString("D6") + rand.Next(diapason2).ToString("D7");

            string boundary = "--" + boundarynumber + ";";
            //            request.Headers.Add("Cache-Control", "no-cache");
            //            request.Headers.Add("Pragma", "no-cache");

            //            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Accept = "application/json";
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary="+boundarynumber;
            //request.Headers.Add("Authorization", "OAuth ARbxfUwAAyM5MvWK2GMERsymbP1h2_NS8A");  // shold.community
            request.Headers.Add("Authorization", "OAuth ARb5DIcAAyM5KQRLGzYRTdCM3d69ZVPmuw");  // sholdcommunity
            //System.Text.Encoding.UTF8.GetBytes(bmp);
            //            request.ContentType = "multipart/form-data; charset=UTF-8; boundary=-----------------------------" + boundarynumber;
            /*            request.KeepAlive = true;
                        request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7,windows-1251,utf-8;q=0.7,*;q=0.7");
                        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        request.Headers.Add("Accept-Language", "ru,en-us;q=0.7,en;q=0.3,ru,en-us;q=0.7,en;q=0.3");
                        request.Headers.Add("Keep-Alive", "300");
                        request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.7) Gecko/20100625 Firefox/3.6.7";
                        //request.CookieContainer = cont;
            */
            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, ImageFormat.Jpeg);
            //byte[] bitmapBytes = memoryStream.GetBuffer();
            request.Headers.Add("Content-Disposition", "form - data; name =\"image\"; filename=\"picture\"");
            request.ContentType = ("image/jpeg");
            byte[] EncodedPostParams = Encoding.GetEncoding(65001).GetBytes(
                boundary + "\r\nContent-Disposition: form-data; name=\"image\"; filename=\"picture\"\r\n" +
                boundary + "\r\nContent-Type: image/jpeg\r\n" //+
//                Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks)
                //                boundary + "\r\nContent-Disposition: form-data; name = \"client\" \r\n\r\nipic.su" +
                //                boundary + "\r\nContent-Disposition: form-data; name = \"image\";\"filename=\""
                );
            /*
                        byte[] EncodedPostParams = Encoding.GetEncoding(1251).GetBytes(
                            boundary + "\r\nContent-Disposition: form-data; name=\"rm\"\r\n\r\nexecuted-puzzle\r\n" +
                            boundary + "\r\nContent-Disposition: form-data; name=\"key\"\r\n\r\n" + puzzlestring + "\r\n" +
                            boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\noffice_prof_machine\r\n" +
                            boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\nchat\r\n" +
                            boundary + "\r\nContent-Disposition: form-data; name=\"wblocks_params\"\r\n\r\n\r\n" +
                            boundary + "\r\nContent-Disposition: form-data; name=\"cwindow\"\r\n\r\noffice\r\n" +
                            boundary);
            */
            //            request.ContentLength = EncodedPostParams.Length;
            //            request.GetRequestStream().Write(EncodedPostParams, 0, EncodedPostParams.Length);
            //Stream dataStream = request.GetRequestStream();
            //System.Net.ServicePointManager.Expect100Continue = false;
            //request.ContentLength = bitmapBytes.Length;
            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(EncodedPostParams, 0, EncodedPostParams.Length);
//            //Stream dataStream = request.GetRequestStream();
            //WebResponse response = requestStream.GetResponse();
            //Stream data = (Stream)response.GetResponseStream();
            //StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //response.Close();
            //request.GetRequestStream().Close();
            //return reader.ReadToEnd();


            System.Net.ServicePointManager.Expect100Continue = false;
            //request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentLength = memoryStream.Length;
            Stream requestStream = request.GetRequestStream();
//            requestStream.Write(EncodedPostParams, 0, EncodedPostParams.Length);
            
            memoryStream.WriteTo(requestStream);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Console.WriteLine("Content length is {0}", request.ContentLength);
            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //response.Close();
            requestStream.Close();
            
            return reader.ReadToEnd();

        }
        public static String POSTsendToJpegshareNet(Bitmap bmp)//, CookieContainer cont)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://jpegshare.net/upload.php");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.10.10");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api-fotki.yandex.ru/api/users/shold-community/");

            const int diapason = 999999;
            const int diapason2 = 9999999;
            Random rand = new Random();
            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, ImageFormat.Png);
            byte[] bitmapBytes = memoryStream.GetBuffer();
            //bitmapBytes = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(1250), bitmapBytes);
            string boundarynumber = rand.Next(diapason).ToString("D6") + rand.Next(diapason2).ToString("D7");

            string boundary = "-----------------------------" + boundarynumber ;
            //request.Headers.Add("Cache-Control", "no-cache");
            //request.Headers.Add("Pragma", "no-cache");
//            request.Method = "GET";
            request.Method = "POST";
//            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //request.ContentType = "multipart/form-data; charset=UTF-8; boundary=-----------------------------" + boundarynumber;
//            request.ContentType = "multipart/form-data; boundary=-----------------------------" + boundarynumber;
//            request.KeepAlive = true;
            //request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7,windows-1251,utf-8;q=0.7,*;q=0.7");
            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
//            request.Headers.Add("Accept-Encoding","gzip, deflate");
//            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            //request.Headers.Add("Keep-Alive", "300");
//            request.Referer = "http://jpegshare.net/";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.7) Gecko/20100625 Firefox/3.6.7";
            //request.CookieContainer = cont;
            //String myTemp = ASCIIEncoding.Default.GetBytes(bitmapBytes);
            /*            byte[] EncodedPostParams = Encoding.GetEncoding(1252).GetBytes(   // utf8 - 65001
                            "POSTDATA=" +boundary + "\r\nContent-Disposition: form-data; name=\"imgfile\";filename=\"noname.jpg\"\r\n" +
                            "Content-Type: image/jpeg\r\n\r\n" + 
                            Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks)
                         //memoryStream.ToArray();
                            //+ Convert.ToBase64String(memoryStream.ToArray())
                            //                boundary + "\r\nContent-Disposition: form-data; name = \"action\"\r\n\r\nloadimg" +
                            //                boundary + "\r\nContent-Disposition: form-data; name = \"client\" \r\n\r\nipic.su" +
                            //                boundary + "\r\nContent-Disposition: form-data; name = \"image\";\"filename=\""
                            );
            */
            byte[] EncodedPostParams = Encoding.GetEncoding(65001).GetBytes("Authorization: OAuth ARbxfUwAAyM5QcNtzQ7gSraQX3qmiKbSXg");
                        /*
                                    byte[] EncodedPostParams = Encoding.GetEncoding(1251).GetBytes(
                                        boundary + "\r\nContent-Disposition: form-data; name=\"rm\"\r\n\r\nexecuted-puzzle\r\n" +
                                        boundary + "\r\nContent-Disposition: form-data; name=\"key\"\r\n\r\n" + puzzlestring + "\r\n" +
                                        boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\noffice_prof_machine\r\n" +
                                        boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\nchat\r\n" +
                                        boundary + "\r\nContent-Disposition: form-data; name=\"wblocks_params\"\r\n\r\n\r\n" +
                                        boundary + "\r\nContent-Disposition: form-data; name=\"cwindow\"\r\n\r\noffice\r\n" +
                                        boundary);
                        */
                        System.Net.ServicePointManager.Expect100Continue = false;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentLength = EncodedPostParams.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(EncodedPostParams, 0, EncodedPostParams.Length);
            
            //Stream dataStream = request.GetRequestStream();
            //WebResponse.ContentLength;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();



            //response.ContentLength = 0;
            //     (HttpWebResponse)request.GetResponse();
            Console.WriteLine("Content length is {0}", response.ContentLength);
            //            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //            {
            Stream data = (Stream) response.GetResponseStream();
//            }
            
            //data.Position = 0;
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            requestStream.Close();
            //request.GetRequestStream().Close();
            //response.Close();
            return reader.ReadToEnd();
        }


        public static String POSTsendToIpicSu(string puzzlestring)//, CookieContainer cont)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ipic.su/api/index.php");

            const int diapason = 999999;
            const int diapason2 = 9999999;
            Random rand = new Random();

            string boundarynumber = rand.Next(diapason).ToString("D6") + rand.Next(diapason2).ToString("D7");

            string boundary = "-----------------------------" + boundarynumber + ";";
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Pragma", "no-cache");
            request.Method = "POST";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "multipart/form-data; charset=UTF-8; boundary=-----------------------------" + boundarynumber;
            request.KeepAlive = true;
            request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7,windows-1251,utf-8;q=0.7,*;q=0.7");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers.Add("Accept-Language", "ru,en-us;q=0.7,en;q=0.3,ru,en-us;q=0.7,en;q=0.3");
            request.Headers.Add("Keep-Alive", "300");
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.7) Gecko/20100625 Firefox/3.6.7";
            //request.CookieContainer = cont;
            byte[] EncodedPostParams = Encoding.GetEncoding(65001).GetBytes(
                boundary + "\r\nContent-Disposition: form-data; name=\"link\"\r\n\r\n/"+
                boundary + "\r\nContent-Disposition: form-data; name = \"action\"\r\n\r\nloadimg"+
                boundary + "\r\nContent-Disposition: form-data; name = \"client\" \r\n\r\nipic.su" +
                boundary + "\r\nContent-Disposition: form-data; name = \"image\";\"filename=\""
                );
  /*
              byte[] EncodedPostParams = Encoding.GetEncoding(1251).GetBytes(
                  boundary + "\r\nContent-Disposition: form-data; name=\"rm\"\r\n\r\nexecuted-puzzle\r\n" +
                  boundary + "\r\nContent-Disposition: form-data; name=\"key\"\r\n\r\n" + puzzlestring + "\r\n" +
                  boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\noffice_prof_machine\r\n" +
                  boundary + "\r\nContent-Disposition: form-data; name=\"blocks\"\r\n\r\nchat\r\n" +
                  boundary + "\r\nContent-Disposition: form-data; name=\"wblocks_params\"\r\n\r\n\r\n" +
                  boundary + "\r\nContent-Disposition: form-data; name=\"cwindow\"\r\n\r\noffice\r\n" +
                  boundary);
  */
            request.ContentLength = EncodedPostParams.Length;
            request.GetRequestStream().Write(EncodedPostParams, 0, EncodedPostParams.Length);
            //Stream dataStream = request.GetRequestStream();
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            response.Close();
            request.GetRequestStream().Close();
            return data.ToString();
        }


        /*        public static string UploadTo(Bitmap bitmap)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    bitmap.Save(memoryStream, ImageFormat.Jpeg);
                    using (var w = new WebClient())
                    {
                        //bitmap = 
                        String values = "<input type=\"hidden\" name=\"action\" value=\"loadimg\" /> " +
                            "< input type =\"hidden\" name=\"quality\" value=\"75\" />" +
                            "<input type=\"file\" name=\"image\" value=\""+ (memoryStream.ToString()) + "\" /> ";
                        byte[] postArray = Encoding.ASCII.GetBytes(values);
                        / *1
                        hidden: action = "loadimg" // тип действия - загрузка изображения
                        hidden: quality = "85"// качество JPEG [0..100], оно же и для PNG (в png качество от 0 до 9)
                        file: image = "c:\pic.png" // собственно само поле выбора изображения, следует отсылать файлы меньше 5Мб следующих форматов: JPG, PNG, GIF

                        Также можно указать описание и параметры изменения размеров изображения:
                                    text: name = "Картинка" // название изображения, будет отображаться на странице изображения, максимум 254 символа
                        textarea: desc = "Сделал сегодня скрин в игре" // описание изображения, максимум 254 символа
                        checkbox: crop = true // Уменьшить ли изображение?
                        text: cropselect = "800" // Размер наибольшей стороны изображения на выходе

                        *1 /
                        / 2*                var values = new NameValueCollection
                                        {

                                            { "image", Convert.ToBase64String(memoryStream.ToArray()) }
                                        };
                        *2 /
                        //w.
                        byte[] responseArray = w.UploadData("http://ipic.su/index.php", "POST", postArray);
                        //stream.Write(postArray, 0, postArray.Length);
                        //String a = stream.;
                        //String a = w.
                        string debug = values.ToString();
                        //byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);
                        //XDocument xDocument = XDocument.Load(new MemoryStream(response));
                        return Encoding.ASCII.GetString(responseArray); 
                    }
                }
        */
    }
}
