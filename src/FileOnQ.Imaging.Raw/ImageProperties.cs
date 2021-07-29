using System;
using System.Drawing.Imaging;

namespace FileOnQ.Imaging.Raw
{
	class ImageProperties
	{
		public ImageProperties(int width, int bits, int colors)
		{
			var bitsPerPixel = bits * colors;
			var bytesPerPixel = bitsPerPixel / bits;

			Offset = width % 4;
			Stride = (width * bytesPerPixel) + Offset;

			if (bitsPerPixel == 24)
				PixelFormat = PixelFormat.Format24bppRgb;
			else
				throw new NotSupportedException($"Only 8-bit Bitmaps are supported. Input image is using {bits}-bit Bitmap.");
		}
		
		public int Stride { get; set; }
		public int Offset { get; set; }
		public int StrideWithoutOffset => Stride - Offset;
		public PixelFormat PixelFormat { get; set; }
	}
}
