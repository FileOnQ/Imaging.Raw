using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

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
		/// <param name="imageWr">
		/// A proccessed raw image or unpacked thumbnail.
		/// </param>
		/// <returns>
		/// A managed <see cref="Bitmap"/>.
		/// </returns>
		public static Bitmap AsBitmap(this IImageWriter imageWriter, bool useAcceleratedGraphics = false) =>
			imageWriter.AsProcessedImage().AsBitmap(useAcceleratedGraphics);

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
		public static unsafe Bitmap AsBitmap(this ProcessedImage imageData, bool useAcceleratedGraphics = false)
		{
			// TODO - 7/24/2021 - @ahoefling
			// We need to experiment with GPU parallization tools
			// such as CUDA and OpenCL. We should be able to bui	ld
			// the bitmap much faster natively on the GPU and then
			// put the stream together in a Bitmap object
			if (imageData.ImageFormat == ImageFormat.Bitmap)
			{
				if (imageData.Bits != 8)
					throw new NotSupportedException($"Only 8-bit Bitmaps are supported. Input image is using {imageData.Bits}-bit Bitmap.");

				if (useAcceleratedGraphics)
				{
					var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
					var address = (IntPtr)imageData.Image + memoryOffset;

					Cuda.Error errorCode = Cuda.Error.Success;
					IntPtr bitmapAddress = Cuda.process_bitmap(address, (int)imageData.Image->DataSize, ref errorCode);
					if (errorCode != Cuda.Error.Success)
						throw new RawImageException<Cuda.Error>(errorCode);

					var stride = imageData.Width * 3 + (imageData.Width % 4);
					var bitmap = new Bitmap(imageData.Width, imageData.Height,
						stride,
						System.Drawing.Imaging.PixelFormat.Format24bppRgb,
						bitmapAddress);

					return bitmap;
				}
				else
				{
					var bitmap = new Bitmap(imageData.Width, imageData.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

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

					var b = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					return bitmap;
				}
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
