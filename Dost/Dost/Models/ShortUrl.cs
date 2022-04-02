using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Dost.Models
{
    class MyURLShortener
    {
        private const string APIkey =
           "AIzaSyCZm9HvMq2NNOE3EPu56gbKFCFCK7qapKvC8";

        public string ShortUrl()
        {
            var longUrl = "";
            var url = MyURLShorten(longUrl);
            return url;
        }
        public string MyURLShorten(string Longurl)
        {
            string post = "{\"longUrl\": \"" + Longurl + "\"}";
            string MyshortUrl = Longurl;
            HttpWebRequest Myrequest = (HttpWebRequest)WebRequest
               .Create("https://www.googleapis.com/urlshortener/v1/url?key=" + APIkey);
            try
            {
                Myrequest.ServicePoint.Expect100Continue = false;
                Myrequest.Method = "POST";
                Myrequest.ContentLength = post.Length;
                Myrequest.ContentType = "application/json";
                Myrequest.Headers.Add("Cache-Control", "no-cache");

                using (Stream requestStream =
                   Myrequest.GetRequestStream())
                {
                    byte[] postBuffer = Encoding.ASCII.GetBytes(post);
                    requestStream.Write(postBuffer, 0,
                       postBuffer.Length);
                }

                using (HttpWebResponse response =
                   (HttpWebResponse)Myrequest.GetResponse())
                {
                    using (Stream responseStream =
                       response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new
                           StreamReader(responseStream))
                        {
                            string json = responseReader.ReadToEnd();
                            MyshortUrl = Regex.Match(json, @"""id"":
                        ?""(?<id>.+)""").Groups["id"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return MyshortUrl;
        }
    }
}