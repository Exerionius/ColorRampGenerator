using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ColorRampGenerator.Models;
using Color = System.Windows.Media.Color;
using PixelFormat = System.Windows.Media.PixelFormat;

namespace ColorRampGenerator.Tools
{
    public static class BitmapHelper
    {
        private static PixelFormat _pixelFormat = PixelFormats.Pbgra32;
        
        public static BitmapSource CreateBitmapSource(List<HsbColor[]> colors)
        {
            if (!colors.Any()) return null;
            
            var width = colors.Max(c => c.Length);
            var height = colors.Count;
            
            var bytesPerPixel = _pixelFormat.BitsPerPixel / 8;
            var rawStride = (width * _pixelFormat.BitsPerPixel + 7) / 8;
            var rawData = new byte[rawStride * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (colors[y].Length > x)
                    {
                        SetPixel(ref rawData, x, y, bytesPerPixel, rawStride, ColorHelper.GetRgb(colors[y][x]));
                    }
                    else
                    {
                        ResetAlpha(ref rawData, x, y, bytesPerPixel, rawStride);
                    }
                }
            }

            var bitmapSource = BitmapSource.Create(width, height,
                96, 96, _pixelFormat, null,
                rawData, rawStride);

            return bitmapSource;
        }
        
        public static byte[] ConvertToDib(Bitmap image)
        {
            // https://stackoverflow.com/a/46424800/4657518
            byte[] bm32BData;
            var width = image.Width;
            var height = image.Height;
            
            using (var bm32B = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var gr = Graphics.FromImage(bm32B))
                    gr.DrawImage(image, new Rectangle(0, 0, bm32B.Width, bm32B.Height));
                bm32B.RotateFlip(RotateFlipType.Rotate180FlipX);
                bm32BData = GetImageData(bm32B);
            }

            const int hdrSize = 0x28;
            var fullImage = new byte[hdrSize + 12 + bm32BData.Length];
            WriteIntToByteArray(fullImage, 0x00, 4, true, hdrSize);
            WriteIntToByteArray(fullImage, 0x04, 4, true, (uint) width);
            WriteIntToByteArray(fullImage, 0x08, 4, true, (uint) height);
            WriteIntToByteArray(fullImage, 0x0C, 2, true, 1);
            WriteIntToByteArray(fullImage, 0x0E, 2, true, 32);
            WriteIntToByteArray(fullImage, 0x10, 4, true, 3);
            WriteIntToByteArray(fullImage, 0x14, 4, true, (uint) bm32BData.Length);
            WriteIntToByteArray(fullImage, hdrSize + 0, 4, true, 0x00FF0000);
            WriteIntToByteArray(fullImage, hdrSize + 4, 4, true, 0x0000FF00);
            WriteIntToByteArray(fullImage, hdrSize + 8, 4, true, 0x000000FF);
            Array.Copy(bm32BData, 0, fullImage, hdrSize + 12, bm32BData.Length);
            return fullImage;
        }
        
        private static byte[] GetImageData(Bitmap sourceImage)
        {
            var sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            var stride = sourceData.Stride;
            var data = new byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }
        
        private static void WriteIntToByteArray(byte[] data, int startIndex, int bytes, bool littleEndian, uint value)
        {
            var lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException();
            for (var index = 0; index < bytes; index++)
            {
                var offs = startIndex + (littleEndian ? index : lastByte - index);
                data[offs] = (byte)(value >> (8 * index) & 0xFF);
            }
        }

        private static void SetPixel(ref byte[] data, int x, int y, int bytesPerPixel, int stride, Color color)
        {
            data[x * bytesPerPixel + y * stride + 0] = color.B;
            data[x * bytesPerPixel + y * stride + 1] = color.G;
            data[x * bytesPerPixel + y * stride + 2] = color.R;
            data[x * bytesPerPixel + y * stride + 3] = color.A;
        }

        private static void ResetAlpha(ref byte[] data, int x, int y, int bytesPerPixel, int stride)
        {
            data[x * bytesPerPixel + y * stride + 3] = 50;
        }
    }
}