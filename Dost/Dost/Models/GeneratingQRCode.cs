using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class GeneratingQRCode
    {
        public class QRCodeModel
        {
            //[Display(Name = "Enter QRCode Text")]
            public string QRCodeText { get; set; }
            [Display(Name = "QRCode Image")]
            public string QRCodeImagePath { get; set; }
            public string imagePath { get; set; }

        }
   
    }
}