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
			if (imageData.ImageFormat == ImageFormat.Bitmap)
			{
				if (imageData.Bits != 8)
					throw new NotSupportedException($"Only 8-bit Bitmaps are supported. Input image is using {imageData.Bits}-bit Bitmap.");

				if (imageData.Colors != 3)
					throw new NotSupportedException($"Only 3 color channels (RGB) are supported. Input image is using {imageData.Colors} channel Bitmap.");

				if (useAcceleratedGraphics && !Cuda.IsCudaCapable())
					useAcceleratedGraphics = false;

				// TODO - 7/29/2021 - This calculation fails for 32 bit assemblies. I think we may need to change our calculation to handle the different memory address space
				if (useAcceleratedGraphics)
				{
					var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
					var address = (IntPtr)imageData.Image + memoryOffset;

					Cuda.Error errorCode = Cuda.Error.Success;
					IntPtr bitmapAddress = Cuda.ProcessBitmap(address, imageData.Buffer.Length, imageData.Width, imageData.Height, ref errorCode);
					if (errorCode != Cuda.Error.Success)
						throw new RawImageException<Cuda.Error>(errorCode);

					var properties = new ImageProperties(imageData.Width, imageData.Bits, imageData.Colors);

					var bitmap = new Bitmap(imageData.Width, imageData.Height,
						properties.Stride,
						System.Drawing.Imaging.PixelFormat.Format24bppRgb,
						bitmapAddress);

					return bitmap;
				}
				else
				{
					var properties = new ImageProperties(imageData.Width, imageData.Bits, imageData.Colors);
					var additionalBytes = properties.Offset * imageData.Height;
					var buffer = new byte[imageData.Buffer.Length + additionalBytes];

					var bitmapPosition = 0;

					// TODO - 7/29/2021 - @ahoefling - It may be safer to use the bitmap pointer instead of our own
					//var bitmap = new Bitmap(imageData.Width, imageData.Height);
					//var data = bitmap.LockBits(new Rectangle(0, 0, imageData.Width, imageData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
					//var pointer = (byte*)data.Scan0;

					for (int position = 0; position < imageData.Buffer.Length; position += 3)
					{
						if (position > 0 && position % properties.StrideWithoutOffset == 0)
						{
							for (int j = 0; j < properties.Offset; j++)
							{
								buffer[bitmapPosition] = 0;
								bitmapPosition++;
							}
						}

						buffer[bitmapPosition + 2] = imageData.Buffer[position];
						buffer[bitmapPosition + 1] = imageData.Buffer[position + 1];
						buffer[bitmapPosition] = imageData.Buffer[position + 2];

						bitmapPosition += 3;
					}

					//bitmap.UnlockBits(data);

					var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					var address = handle.AddrOfPinnedObject();

					var bitmap = new Bitmap(imageData.Width, imageData.Height,
						properties.Stride,
						System.Drawing.Imaging.PixelFormat.Format24bppRgb,
						address);

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
