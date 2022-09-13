using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KotClassLibrary.Helpers
{
    public class FileHelper
    {
        public static byte[] CreateByteArrayFromFile(string fileName)
        {
            byte[] returnValue = null;
            if (File.Exists(fileName))
            {
                using(var ms = new MemoryStream())
                {
                    using(var fs = File.OpenRead(fileName))
                    {
                        fs.CopyTo(ms);
                    }
                    returnValue = ms.ToArray();
                }
            }
            return returnValue;
        }

        public static string CreateBase64StringFromByteArray(byte[] byteArray)
        {
            string imageBase64Data = Convert.ToBase64String(byteArray);
            string imageDataURL = string.Format("Data:image/jpg;base64,{0}", imageBase64Data);
            return imageDataURL;
        }
        public static string GeneratePdf(byte[] byteArray)
        {
            string pdfBase64Data = Convert.ToBase64String(byteArray);
            string pdfDataURL = $"Data:application/pdf;base64,{pdfBase64Data}";
            return pdfDataURL;
        }
        public static byte[] GenerateContract()
        {
            byte[] fileAsBytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                //document.Save(stream, false);
                fileAsBytes = stream.ToArray();
            }
            return fileAsBytes;

        }
    }
}
