using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dost.Models
{
    public class Master:Common
    {
     public DataTable dtImage { get; set; }

        public  List<Master> lstBanner { get; set; }

        public List<Master> lst { get; set; }

        public string AddedBy { get; set; }
        public string BinaryPercent { get;  set; }
        public string Blaze { get;  set; }
        public string Brand { get;  set; }
        public string Catalyst { get;  set; }
        public string CGST {get;  set;}
        public string DeliveryCharge {get; set;}
        public string Description {get;  set;}
        public string Discount {get;  set; }
        public decimal DistributorCommission {get;  set;}
        public string EventDescription { get;  set; }
        public string EventImage {get;  set;}
        public string EventName { get;  set; }
        public string Fk_CategoryId { get;  set; }
        public string IGST {get;  set;}
        public string Maverick { get;  set; }
        public string NFCType {get;  set;}
        public int ? OfferId {get;  set;}
        public string OfferName { get; set; }
        public string OfferPrice {get;  set;}
        public string Phoenix { get;  set; }
        public int ? PK_NFcId {get;  set;}
        public string ProductPrice {get;  set;}
        public string ReferralPercent {get;  set;}
        public string SGST {get;  set;}
        public decimal SponsorCommission {get;  set;}
        public string SubCategoryId {get;  set;}
        public string Symphony { get;  set;}
        public string Quantity { get; set; }
        public string  OfferValidity { get;  set; }
        public string CouponCode { get;  set; }
        public string BannerImage { get;  set; }
        public int ? BannerId { get;  set; }
        public string BannerTitle { get;  set; }
        public string Trailer { get;  set; }
        public bool IsAddFav { get;  set; }
        public string FavStatus { get;  set; }
        public string Fk_ProductId { get; set; }
        public string Product { get; set; }
        public string Pk_ProductImageId { get; set; }

        public List<SelectListItem> ddlCouponType { get; set; }
        public string PK_CouponId { get; set; }
        public string CouponType { get; set; }
        public string Pk_CouponTypeId { get; set; }
        public string Coupon { get; set; }
        public string Price { get; set; }
        public string ValidityDate { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public List<Master> lstCoupon { get; set; }

        public DataSet categorylist()
        {
            DataSet ds = DBHelper.ExecuteQuery("CategoryList");
            return ds;
        }
        public DataSet NFCList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_NFcId",PK_NFcId)
            };
            DataSet ds = DBHelper.ExecuteQuery("NFCList", para);
            return ds;
        }
        public DataSet DeleteNFC()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@PK_NFcId",PK_NFcId),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteNFC", para);
            return ds;
        }
        public DataSet SaveNFCDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@NFCType",NFCType),
                new SqlParameter("@Description",Description),
                new SqlParameter("@Image",EventImage),
                new SqlParameter("@OfferId",OfferId),
                new SqlParameter("@Price",ProductPrice),
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_CategoryId",Fk_CategoryId),
                new SqlParameter("@ReferralPercentage",ReferralPercent),
                new SqlParameter("@IGST",IGST),
                new SqlParameter("@CGST",CGST),
                new SqlParameter("@SGST",SGST),
                 new SqlParameter("@OfferPrice",OfferPrice),
                  new SqlParameter("@Discount",Discount),
                  new SqlParameter("@DeliveryCharge",DeliveryCharge),
                  new SqlParameter("@DistributorCommission",DistributorCommission),
                  new SqlParameter("@SponsorCommission",SponsorCommission),
                  new SqlParameter("@Catalyst",Catalyst),
                  new SqlParameter("@Blaze",Blaze),
                  new SqlParameter("@Maverick",Maverick),
                  new SqlParameter("@Symphony",Symphony),
                  new SqlParameter("@Phoenix",Phoenix),
                  new SqlParameter("@Brand",Brand),
                  new SqlParameter("@NoofSeats",Quantity)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveNFC", para);
            return ds;
        }
        public DataSet UpdateNFCDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_NFcId",PK_NFcId),
                new SqlParameter("@NFCType",NFCType),
                new SqlParameter("@Description",Description),
                new SqlParameter("@Image",EventImage),
                new SqlParameter("@OfferId",OfferId),
                new SqlParameter("@Price",ProductPrice),
                 new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Fk_CategoryId",Fk_CategoryId),
                new SqlParameter("@ReferralPercentage",ReferralPercent),
                 new SqlParameter("@IGST",IGST),
                    new SqlParameter("@CGST",CGST),
                new SqlParameter("@SGST",SGST),
                 new SqlParameter("@OfferPrice",OfferPrice),
                  new SqlParameter("@Discount",Discount),
                  new SqlParameter("@DeliveryCharge",DeliveryCharge),
                   new SqlParameter("@DistributorCommission",DistributorCommission),
                  new SqlParameter("@SponsorCommission",SponsorCommission),
                  new SqlParameter("@Catalyst",Catalyst),
                  new SqlParameter("@Blaze",Blaze),
                  new SqlParameter("@Maverick",Maverick),
                  new SqlParameter("@Symphony",Symphony),
                  new SqlParameter("@Phoenix",Phoenix),
                  new SqlParameter("@Brand",Brand),
                  new SqlParameter("@NoofSeats",Quantity)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateNFC", para);
            return ds;
        }
        #region Offer
        public DataSet SaveOffer()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OfferName",OfferName),
                new SqlParameter("@OfferValidity",OfferValidity),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveOffer", para);
            return ds;
        }
        public DataSet UpdateOffer()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OfferId",OfferId),
                new SqlParameter("@OfferName",OfferName),
                new SqlParameter("@CouponCode",CouponCode),
                new SqlParameter("@OfferValidity",OfferValidity),
                new SqlParameter("@UpdatedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateOffer", para);
            return ds;
        }
        public DataSet OfferList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OfferId",OfferId)
            };
            DataSet ds = DBHelper.ExecuteQuery("OfferList", para);
            return ds;
        }
        public DataSet DeleteOffer()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@OfferId",OfferId),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteOffer", para);
            return ds;
        }

        #endregion
        #region Banner
        public DataSet SaveBannerDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@BannerImage",BannerImage),
                new SqlParameter("@BannerTitle",BannerTitle),
                 new SqlParameter("@Description",Description),
                 new SqlParameter("@TrailarLink",Trailer),
                 new SqlParameter("@IsAddFav",IsAddFav),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveBanner", para);
            return ds;
        }
        public DataSet UpdateBannerDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@BannerId",BannerId),
            new SqlParameter("@BannerImage", BannerImage),
            new SqlParameter("@BannerTitle",BannerTitle),
            new SqlParameter("@Description",Description),
            new SqlParameter("@Trailer",Trailer),
            new SqlParameter("@IsAddFav",IsAddFav),
            new SqlParameter("@UpdatedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBannerDetails", para);
            return ds;
        }
        public DataSet BannerList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@BannerId",BannerId)
            };
            DataSet ds = DBHelper.ExecuteQuery("BannerList", para);
            return ds;
        }
        public DataSet BannerDelete()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@BannerId",BannerId),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteBannerDetails", para);
            return ds;
        }
        public DataSet ProductImageList()
        {
            SqlParameter[] para =
              {
                                         new SqlParameter("@Pk_ProductImageId",Pk_ProductImageId),
                                         new SqlParameter("@Fk_ProductId",Fk_ProductId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ProductImageList", para);
            return ds;

        }
        public DataSet SaveProductImage()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@dtImage",dtImage),
                new SqlParameter("@Fk_ProductId",Fk_ProductId),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveProductImage", para);
            return ds;
        }
        #endregion
        public DataSet GetRunningEvents()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetRunningEvents");
            return ds;
        }
        public DataSet GetUpcomingEvents()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetUpcomingEvents");
            return ds;
        }

        public DataSet GetCouponTypeDetails()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetCouponTypeDetails");
            return ds;
        }


        public DataSet SaveCoupon()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Fk_CouponTypeId",Pk_CouponTypeId),
             new SqlParameter("@Coupon",Coupon),
             new SqlParameter("@Price",Price),
             new SqlParameter("@ValidityDate",ValidityDate),
             new SqlParameter("@RangeFrom",RangeFrom),
             new SqlParameter("@RangeTo",RangeTo),
             new SqlParameter("@CouponCode",CouponCode),
             new SqlParameter("@AddedBy",AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveCoupon", para);
            return ds;
        }

        public DataSet UpdateCoupon()
        {
            SqlParameter[] para =
           {   
             new SqlParameter("@CouponId",PK_CouponId),
             new SqlParameter("@Fk_CouponTypeId",Pk_CouponTypeId),
             new SqlParameter("@Coupon",Coupon),
             new SqlParameter("@Price",Price),
             new SqlParameter("@ValidityDate",ValidityDate),
             new SqlParameter("@RangeFrom",RangeFrom),
             new SqlParameter("@RangeTo",RangeTo),
             new SqlParameter("@CouponCode",CouponCode),
             new SqlParameter("@AddedBy",AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCoupon", para);
            return ds;
        }



        public DataSet SelectCouponList()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@CouponId",PK_CouponId),
                 new SqlParameter("@CouponCode",CouponCode),
                  new SqlParameter("@CouponType",CouponType),
                   new SqlParameter("@Coupon",Coupon),
                    new SqlParameter("@Price",Price),
                     new SqlParameter("@ValidityDate",ValidityDate),
                      new SqlParameter("@RangeFrom",RangeFrom),
                       new SqlParameter("@RangeTo",RangeTo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetCouponDetailsNew", para);
            return ds;
        }

        public DataSet DeleteCoupon()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@CouponID",PK_CouponId),
                 new SqlParameter("@DeletedBy",AddedBy),

            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteCoupon", para);
            return ds;
        }

        


    }
}