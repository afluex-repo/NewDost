using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class EmployeeRegistrations
    {
        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "DOB")]
        public string DOB { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }

        [Display(Name = "EducationQualification")]
        public string EducationQualififcation { get; set; }

        public string Fk_UserTypeId { get; set; }
        public string Fk_BranchId { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }

        [NotMapped]
        public List<EmployeeRegistrations> lstemp { get; set; }
        public string LoginId { get; set; }
        public DataSet SaveEmpoyeeData()
        {
            SqlParameter[] para = {
            new SqlParameter("@Name", Name),
            new SqlParameter("@Mobile", Mobile),
            new SqlParameter("@Email", Email),
            new SqlParameter("@Address", Address),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@Qualification", EducationQualififcation),
            new SqlParameter("@FathName", FathersName),
            new SqlParameter("@Fk_UserTypeId", Fk_UserTypeId),
            new SqlParameter("@Fk_BranchId", Fk_BranchId),
            new SqlParameter("@CreatedBy", CreatedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }
        public DataSet GetEmployeeData()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeDetails");
            return ds;
        }
    }
}