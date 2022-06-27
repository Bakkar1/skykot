using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using KotClassLibrary.Models;

namespace KotClassLibrary.Helpers
{
    public class PhotoHelper
    {
        public static List<string> ProcessUploadedFile(IWebHostEnvironment hostEnvironment, List<IFormFile> Photos ,string subPath = "Room")
        {
            List<string> paths = new List<string>();
            if (Photos != null && Photos.Count > 0)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, $"Images/{subPath}");
                foreach (IFormFile photo in Photos)
                {
                    if(IsImage(photo))
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(fs);
                        }
                        paths.Add(uniqueFileName);
                    }
                }
            }
            return paths;
        }
        public static void DeletePhotos(IWebHostEnvironment hostEnvironment, ICollection<RoomImage> Photos, string subPath = "Room")
        {
            // delete photos from images folder
            if(Photos.Any())
            {
                string absolutewwwRootPath = Path.Combine(hostEnvironment.WebRootPath, $"Images/{subPath}");
                foreach (RoomImage photo in Photos)
                {
                    string filePath = Path.Combine(absolutewwwRootPath, photo.Path);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
        }
        public static void DeleteProfilePhoto(IWebHostEnvironment hostEnvironment, string url)
        {
            // delete photos from images folder
            if (!string.IsNullOrEmpty(url))
            {
                string filePath = Path.Combine(hostEnvironment.WebRootPath, $"Images/Profile/{url}");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        public static string UploadProfilePhoto(IWebHostEnvironment hostEnvironment, IFormFile comingPhoto)
        {
            string uniqueFileName = null;
            if (comingPhoto != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "Images/Profile");

                IFormFile photo = comingPhoto;
                if (IsImage(photo))
                {

                    uniqueFileName = Guid.NewGuid().ToString() + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fs);
                    }
                }
            }
            return uniqueFileName;
        }


        public static bool IsImage(IFormFile postedFile)
        {
            if (postedFile.ContentType.ToLower() != "image/jpg" &&

                postedFile.ContentType.ToLower() != "image/jpeg" &&

                postedFile.ContentType.ToLower() != "image/pjpeg" &&

                postedFile.ContentType.ToLower() != "image/gif" &&

                postedFile.ContentType.ToLower() != "image/x-png" &&

                postedFile.ContentType.ToLower() != "image/png")

            {

                return false;

            }
            else
            {
                return true;
            }
        }
    }
}