using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UI.Models
{
    public class MyTools
    {
        public static int PAGE_SIZE = 3;

        public static string SaveFileToFolder(IFormFile file, string folderName, string productId)
        {
            string fileName = $"{DateTime.Now.Ticks}_{file.FileName}";
            
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName, productId);
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fullName = Path.Combine(folderPath, fileName);
            using (var myFile = new FileStream(fullName, FileMode.Create))
            {
                file.CopyTo(myFile);
            }
            return fileName;
        }

        public static string GenerateRandomKey()
        {
            Random rd = new Random();
            string pattern = @"zaqwsxedcrfvtgbyhnujmik1234567890~!@#$%^&*()_+|<>?:";

            int length = rd.Next(3, 11);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append(pattern[rd.Next(0, pattern.Length)]);

            return sb.ToString();
        }
    }

    public static class StaticClass
    {
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash)
                sbHash.Append(String.Format("{0:x2}", b));

            return sbHash.ToString();
        }

        public static string ToVND(this double dongia)
        {
            return $"{dongia.ToString("#,##0")} đ";
        }

        public static string ToUrlFriendly(this string url)
        {
            url = url.ToLower();

            //Lọc bỏ từ tiếng Việt
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"đ", "d");
            url = Regex.Replace(url, @"[ýỳỵỷỹ]", "y");

            //thay thế theo chuẩn URL friendly
            url = Regex.Replace(url, @"[^a-z0-9\s-]", "");
            url = Regex.Replace(url, @"\s+", "-");
            url = Regex.Replace(url, @"\s", "-");
            url = Regex.Replace(url, @"(-)+", "-");

            return url;
        }
    }
}
