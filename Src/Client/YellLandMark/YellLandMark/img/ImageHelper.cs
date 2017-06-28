using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YellLandMark.img
{
    public class ImageHelper
    {
        private ImageHelper() { }
        public static Image ByteArrayToImage(byte[] bytes) {
            using (var memoryStream = new MemoryStream()) {
                var image = new Image
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(bytes))
                };
                return image;
            }
               
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        


    }
}
