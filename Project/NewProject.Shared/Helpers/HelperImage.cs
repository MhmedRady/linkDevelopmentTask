using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace VacanciesTask.Shared.Helpers;

public static partial class Helper
{
    public static string? checkImageExt(IFormFile img)
    {
        var format_ext_image = Path.GetExtension(img.FileName);
        string[] format_images = new string[] { ".jpg", ".jpeg", ".png" };
        if (!format_images.Contains(format_ext_image.ToLower()))
        {
            return $"Only these images file types are accepted : [ {string.Join(" ", format_images)} ]";
        }
        return null;
    }
    public static string? uploadImage(IFormFile img, string? dirName)
    {
        Random rand = new Random();
        var format_ext_image = Path.GetExtension(img.FileName);
        var r = rand.Next(1000, int.Parse(DateTime.Now.ToString("yyyyyMMmmss"))).ToString();
        // string uniqueImge = r + "." + img.FileName.Split(format_ext_image)[1];
        string uniqueImge = r + format_ext_image;
        if (!System.IO.Directory.Exists(@$".\wwwroot\images\uploaded\{dirName}"))
        {
            System.IO.Directory.CreateDirectory(@$".\wwwroot\images\uploaded\{dirName}");
        }
        using (var obj = new FileStream(@$".\wwwroot\images\uploaded\{dirName}\" + uniqueImge, FileMode.Create))
        {
            img.CopyTo(obj);
        }
        return dirName != null ? @$"{dirName}/{uniqueImge}" : uniqueImge;
    }

    public static string? imageUrl(string? image)
    {
        if (image == null)
            return @"/images/DefaultImage.jpeg";
        if (image.StartsWith("http") || image.StartsWith("https"))
        {
            return image;
        }
        return $@"/images/uploaded/{image}";
    }

    public static byte[] BitmapToByteArray(Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
    
}