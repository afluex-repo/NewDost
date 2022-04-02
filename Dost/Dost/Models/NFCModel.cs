using Dost.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Dost.Models
{
    public class NFCModel
    {
        public string Code { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public string Medium { get; set; }
        public List<NFCModel> lst { get; set; }
        public string ViewDateTime { get; set; }
        public string FK_UserId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string ZipCode { get; set; }
        public string Location { get; set; }
        public string LogId { get; set; }
        public string Device { get; set; }
        public string Body { get; set; }
        public string Result { get; set; }
        public DataSet CheckNFCCode()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckNFCCode", para);
            return ds;
        }
        public DataSet GetNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code),
                  new SqlParameter ("@LogId",LogId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCProfileData", para);
            return ds;
        }
        public DataSet InsertLog()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code),
                new SqlParameter ("@Browser",Browser),
                new SqlParameter ("@IP",IP),
                new SqlParameter ("@Medium",Medium),
                new SqlParameter ("@Lat",Lat),
                new SqlParameter ("@Long",Long),
                new SqlParameter ("@Location",Location),
                new SqlParameter ("@ZipCode",ZipCode),
                new SqlParameter ("@Device",Device)
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertLog", para);
            return ds;
        }

        public DataSet ActivateNFCCode(string ActivationCode, string DistributorCode)
        {
            SqlParameter[] para ={
                new SqlParameter ("@LoginId",LoginId),
                new SqlParameter ("@NFCCode",Code),
                new SqlParameter ("@DistributorCode",DistributorCode),
                new SqlParameter ("@ActivationCode",ActivationCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateNFC", para);
            return ds;
        }

        public DataSet CheckDistributor(string DistributorCode)
        {
            SqlParameter[] para ={
                new SqlParameter ("@DistributorCode",DistributorCode)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckDistributor", para);
            return ds;
        }



        public DataSet GetNFCCodes()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetNFCCodes");
            return ds;
        }

        public DataSet UpdateNFCEncCode()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Id",LoginId),
                new SqlParameter ("@EncCode",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateNFCEncCode", para);
            return ds;
        }
        public DataSet GetRecentAnalytics()
        {
            SqlParameter[] para ={
                new SqlParameter ("@FK_UserId",FK_UserId)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetRecentAnalytics", para);
            return ds;
        }
    }

   
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }
}
