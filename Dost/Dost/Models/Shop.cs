using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
namespace Dost.Models
{
    public class Shop
    {
        public DataTable dtproductitem { get; set; }
        public List<Shop> lstproduct { get; set; }
        public List<Shop> lstcoupon { get; set; }
        #region property
        public string Quantity { get; set; }
        public string Total { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserCode { get; set; }
        public string Brand { get; set; }
        public string Remarks { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DeliverCharge { get; set; }
        public string ProductPrice { get; set; }
        public string Balance { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string BinaryPercent { get; set; }
        public string DirectPercent { get; set; }
        public string BV { get;  set; }
        public string OfferPrice { get;  set; }
        public string EventImage { get;  set; }
        public string EventDescription { get;  set; }
        public string NoOfSeats { get;  set; }
        public string PK_EventId { get; set; }
        public string Pk_CardId { get; set; }
        public string Discount { get; set; }
        public string NumberOfItems { get; set; }
        public string TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Fk_UserId { get; set; }
        public string AddedBy { get; set; }
        public string Result { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string WalletBalance { get; set; }
        public string DeliveryCharge { get; set; }
        public string AddressId { get; set; }
        public string Gst { get; set; }
        public List<AddressBook> lstAddressBook { get; set; }
        public string City { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        #region

        public List<productimagelst> lstProductImage { get; set; }

        #endregion
        public string CouponCode { get; set; }
        public string CouponName { get; set; }
        public string Message { get; set; }
        public string ImageId { get; set; }
        #endregion
        public DataSet productlist()
        {
            SqlParameter[] para = { new SqlParameter("@PK_EventId",PK_EventId ),
                    new SqlParameter("@Fk_UserId",Fk_UserId )
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFC",para);
            return ds;
        }

        #region get multiple product image list
        public DataSet productimagelist()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_productid",PK_EventId )
                  
            };
            DataSet ds = DBHelper.ExecuteQuery("SP_GetProductimage", para);
            return ds;
        }
        #endregion
        public DataSet AddToCard()
        {
            SqlParameter[] para = 
                {
                new SqlParameter("@FK_EventId",PK_EventId ),
                 new SqlParameter("@TotalItems",NumberOfItems ),
                 new SqlParameter("@AddedBy",AddedBy )
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveAddToCart", para);
            return ds;
        }
        public DataSet AddToCartList()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId ),
                new SqlParameter("@PK_EventId",PK_EventId )
            };
            DataSet ds = DBHelper.ExecuteQuery("AddToCartList", para);
            return ds;
        }
        public DataSet CheckoutItem()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                 new SqlParameter("@FirstName",FirstName),
                  new SqlParameter("@LastName",LastName),
                   new SqlParameter("@UserName",UserName),
                    new SqlParameter("@Email",Email),
                     new SqlParameter("@Address",Address),
                     new SqlParameter("@Pincode",Pincode),
                     new SqlParameter("@State",State),
                     new SqlParameter("@Pk_EventId",PK_EventId),
                      new SqlParameter("@DtCheckoutItem",dtproductitem)

            };
            DataSet ds = DBHelper.ExecuteQuery("UserCheckoutItem", para);
            return ds;
        }
        public DataSet GetUserAddressBook()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUserAddressBook", para);
            return ds;
        }
        public DataSet GetWalletMoney()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletMoney", para);
            return ds;
        }
        public DataSet SaveEventDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_EventId",PK_EventId),
                new SqlParameter("@NoOfSeats",NoOfSeats),
                new SqlParameter("@TotalPrice",TotalPrice),
                new SqlParameter("@PINCode",Pincode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City),
                new SqlParameter("@Address",Address),
                new SqlParameter("@AddressId",AddressId),
                new SqlParameter("@ReferralBV",BV),
                new SqlParameter("@DeliveryCharge",DeliveryCharge),
                new SqlParameter("@Gst",Gst),
                new SqlParameter("@TotalPayable",TotalPrice),
                new SqlParameter("@Dwallet",WalletBalance),
                new SqlParameter("@Fwallet","0"),
                new SqlParameter("@Discount","0"),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@dtproductitem",dtproductitem)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveChecOutItem", para);
            return ds;
        }
      

        public DataSet DeleteCardlist()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@FK_UserId",Fk_UserId),
                new SqlParameter("@FK_EventId",PK_EventId)
               
               
            };
            DataSet ds = DBHelper.ExecuteQuery("SP_Deleteitem", para);
            return ds;
        }
        public DataSet UpdateItems()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@TotalItem",NumberOfItems ),
                 new SqlParameter("@FK_UserId",Fk_UserId ),
                 new SqlParameter("@FK_EventId",PK_EventId )
            };
            DataSet ds = DBHelper.ExecuteQuery("SP_updateitem", para);
            return ds;
        }
        public DataSet ApplyCoupon()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@CouponCode", CouponCode),
                 new SqlParameter("@TotalPrice",TotalPrice )
            };
            DataSet ds = DBHelper.ExecuteQuery("ApplyCoupon", para);
            return ds;
        }
        public DataSet DeleteAddress()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@FK_UserId",Fk_UserId),
                new SqlParameter("@PK_AddressId",AddressId)


            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteAddress", para);
            return ds;
        }
        public DataSet GetOrders()
        {
            SqlParameter[] para =
            {
                 new SqlParameter("@FK_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetOrders", para);
            return ds;
        }
    }
    public class AddressBook
    {
        public string PK_AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string CompleteAddress { get; set; }
        public string IsRecentlyUsed { get; set; }
    }
    public class productimagelst
    {
        public string ProductImage { get; set; }
    }
    }