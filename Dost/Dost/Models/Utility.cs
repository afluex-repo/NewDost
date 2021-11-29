using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Utility
    {
        public string GenerateAlphanumeric(int Size)
        {
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[Size];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            return new String(stringChars1);
        }
    }
}