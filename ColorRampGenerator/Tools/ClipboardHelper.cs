using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ColorRampGenerator.Tools
{
    public static class ClipboardHelper
    {
        public static void CopyToClipboard(BitmapSource bitmapSource)
        {
            Clipboard.Clear();
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder {Interlace = PngInterlaceOption.On};
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                var bitmap = new Bitmap(stream);
                var data = new DataObject();
                using (var pngStream = new MemoryStream())
                using(var dibStream = new MemoryStream())
                {
                    data.SetData(DataFormats.Bitmap, bitmap, true);
                    
                    bitmap.Save(pngStream, ImageFormat.Png);
                    data.SetData("PNG", pngStream, false);

                    var dibData = BitmapHelper.ConvertToDib(bitmap);
                    dibStream.Write(dibData, 0, dibData.Length);
                    data.SetData(DataFormats.Dib, dibStream, false);
                    
                    Clipboard.SetDataObject(data, true);
                }
            }
        }
    }
}