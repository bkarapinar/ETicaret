using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.COMMON.MyTools
{
    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();

                serverPath = serverPath.Replace("~", string.Empty); //Gelen serverPath parametresinde tilda işareti varsa boşlukla değştiriyoruz.

                string[] fileArray = file.FileName.Split('.'); // Gelen veriyi '.' olan yerlerden ayırarak elemanları string dizisine dönüştürdük.

                string extension = fileArray[fileArray.Length - 1].ToLower(); // Dosya uzantısını yakaladık ve küçük harflere çevirdik.

                string fileName = $"{uniqueName}.{extension}"; // Oluşturduğumuz Guid tipindeki değişkenimizle yakaladığımız extension u "dosyaadi.uzantisi" şekline dönüştürdük.

                if (extension == "jpg" || extension == "gif" || extension == "png" || extension == "jpeg") //Uzantısını kontrol ederek resim formatında olup olmadığına bakıyoruz.
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName))) //File.Exists ile dosyanın olup olmadığını kontrol ettik
                    {
                        return "Dosya Daha Önce Kaydedilmiş";
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "Lütfen resim formatında bir dosya seçin!";
                }

            }
            else
            {
                return "Dosya Boş";
            }
        }
    }
}