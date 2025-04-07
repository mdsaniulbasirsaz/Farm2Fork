using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace Farm2Fork.Helpers
{
   public static class ImageHelper
    {
        public static string ConvertImageToBase64(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("Image URL is null or empty.");
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(imageUrl).Result; // Synchronous for simplicity
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid image path: " + ex.Message);
            }
        }
    }


}
