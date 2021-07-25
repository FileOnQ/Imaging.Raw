using System;
using System.Drawing;
using System.IO;

namespace FileOnQ.Imaging.Raw
{
	public static class ThumbnailExtensions
	{
		// Question - Should we move this into a new library such as
		// FileOnQ.Imaging.Raw.Drawing so we don't need the core lib
		// to take a dependency on System.Drawing
		//
		// If we go with that approach we could create additional NuGets
		// for each downstream libray
		// - FileOnQ.Imaging.Raw.Forms (ImageSource)
		// - FileONQ.Imaging.Raw.Uwp (Any UWP specific object, I think Bitmap works)

		/// <summary>
		/// Gets a <see cref="Bitmap"/> from the current
		/// <see cref="ProcessedImage"/> in the most memory
		/// efficient way possible.
		/// </summary>
		/// <param name="imageData">
		/// A proccessed raw image or unpacked thumbnail.
		/// </param>
		/// <returns>
		/// A managed <see cref="Bitmap"/>.
		/// </returns>
		public static Bitmap AsBitmap(this ProcessedImage imageData)
		{
			if (imageData.ImageFormat == ImageFormat.Bitmap)
			{
				if (imageData.Bits != 8)
					throw new NotSupportedException($"Only 8-bit Bitmaps are supported. Input image is using {imageData.Bits}-bit Bitmap.");

				var bitmap = new Bitmap(imageData.Width, imageData.Height);

				int position = 0;

				// should be able to change this to thumbnail.Height * thumbnail.Width
				for (int y = 0; y < imageData.Height; y++)
				{
					for (int x = 0; x < imageData.Width; x++)
					{
						if (position > imageData.Buffer.Length)
							throw new InvalidOperationException("The bitmap buffer position is larger than the binary data. The width or heigh were not read in correctly.");

						// NOTE - 7/23/2021 - @ahoefling
						// This only works with 8-bit Bitmaps. If we
						// are reading a 16-bit Bitmap we will need
						// to update this code.
						bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(
							imageData.Buffer[position],
							imageData.Buffer[position + 1],
							imageData.Buffer[position + 2]));

						position += 3;
					}
				}

				return bitmap;
			}
			else
			{
				return new Bitmap(imageData.AsStream());
			}
		}

		/// <summary>
		/// Gets the processed image buffer as a <see cref="Stream"/>.
		/// </summary>
		/// <param name="imageData">
		/// A proccessed raw image or unpacked thumbnail.
		/// </param>
		/// <returns>
		/// A <see cref="Stream"/> containing the processed image
		/// buffer. The <see cref="Stream"/> is reset to position 0
		/// and can be read immediately.
		/// </returns>
		/// <remarks>
		/// This is designed to be used with JPEG images. If your
		/// <see cref="ProcessedImage"/> is a bitmap, it will add
		/// the bitmap header to your stream for you.
		/// 
		/// If you are trying to create a <see cref="Bitmap"/> from
		/// <see cref="ProcessedImage"/>, it is fastest to use the
		/// <see cref="AsBitmap(ProcessedImage)"/> function as it is
		/// optimized to generate the bitmap. Using this method will
		/// be slower.
		/// </remarks>
		public static Stream AsStream(this ProcessedImage imageData)
		{
			var memory = new MemoryStream();
			if (imageData.ImageFormat == ImageFormat.Bitmap)
			{
				var bitmapHeader = BitmapUtilities.CreateHeader(imageData.Width, imageData.Height);
				memory.Write(bitmapHeader, 0, bitmapHeader.Length);
			}

			// TODO move this to a partial method
#if NET48 || NETSTANDARD2_0
			var data = imageData.Buffer.ToArray();
			memory.Write(data, 0, data.Length);
#elif NET5_0_OR_GREATER
			memory.Write(imageData.Buffer);
#endif

			memory.Seek(0, SeekOrigin.Begin);
			return memory;
		}
	}
}
