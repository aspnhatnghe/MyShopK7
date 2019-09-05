using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class MyTools
    {
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
    }
}
