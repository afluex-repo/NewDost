using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;


namespace Dost.Models
{
    public class GoogleResponse
    {
       
    
        public string Kind { get; set; }
        public string Id { get; set; }
        public string LongUrl { get; set; }
        public string FullUrl { get; set; }
        public string shortUrl { get; set; }
    }

    public class bitly
    {
        private string loginAccount;
        private string apiKeyForAccount;
        private string submitPath = @"http://api.bit.ly/shorten?version=2.0.1&format=xml";
        private int errorStatus = 0;
        private string errorMessage = "";
       
        public bitly()
        
        {
            
        }
       
        public bitly(string login, string APIKey)
        {
            loginAccount = "Reenajaiswal";
            apiKeyForAccount = "8a093d9ca64e32a90ec18f39ece812ad36dde711";
            submitPath += "&login=" + loginAccount + "&apiKey=" + apiKeyForAccount;
        }
        // Properties to retrieve error information.  
        public int ErrorCode
        {
            get { return errorStatus; }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
        }
        /// <summary>  
        /// Shortens a provided URL  
        /// </summary>  
        /// <param name="url">A URL</param>  
        /// <returns>A shortened bit.ly URL String</returns>  
        public string shorten(string url)
        {
            errorStatus = 0;
            errorMessage = "";
            XmlDocument doc = buildDocument(url);
            if (doc.DocumentElement != null)
            {
                XmlNode shortenedNode = doc.DocumentElement.SelectSingleNode("results/nodeKeyVal/shortUrl");
                if (shortenedNode != null)
                {
                    return shortenedNode.InnerText;
                }
                else
                {
                    errorCode(doc);
                }
            }
            else
            {
                this.errorStatus = -1;
                this.errorMessage = "Unable to connect to bit.ly for shortening of URL";
            }
            return "";
        }
        // Sets error code and message in the situation we receive a response, but there was  
        // something wrong with our submission.  
        private void errorCode(XmlDocument doc)
        {
            XmlNode errorNode = doc.DocumentElement.SelectSingleNode("errorCode");
            XmlNode errorMessage = doc.DocumentElement.SelectSingleNode("errorMessage");
            if (errorNode != null)
            {
                this.errorStatus = Convert.ToInt32(errorNode.InnerText);
                this.errorMessage = errorMessage.InnerText;
            }
        }
        // Builds an XmlDocument using the XML returned by bit.ly in response   
        // to our URL being submitted  
        private XmlDocument buildDocument(string url)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                // Load the XML response into an XML Document and return it.  
                doc.LoadXml(readSource(submitPath + "&longUrl=" + url));
                return doc;
            }
            catch (Exception e)
            {
                return new XmlDocument();
            }
        }
        // Fetches a result from bit.ly provided the URL submitted  
        private string readSource(string url)
        {
            WebClient client = new WebClient();
            try
            {
                using (StreamReader reader = new StreamReader(client.OpenRead(url)))
                {
                    // Read all of the response  
                    return reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

  

}