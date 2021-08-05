using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public static class ThumbnailExtensions
	{
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

				if (useAcceleratedGraphics)
				{
					var handle = GCHandle.Alloc(imageData.Buffer, GCHandleType.Pinned);
					imageData.AddHandle(handle);
					
					Cuda.Error errorCode = Cuda.Error.Success;
					int bitmapLength = 0;
					IntPtr bitmapAddress = Cuda.ProcessBitmap(handle.AddrOfPinnedObject(), imageData.Buffer.Length, imageData.Width, imageData.Height, ref bitmapLength, ref errorCode);
					if (errorCode != Cuda.Error.Success)
						throw new RawImageException<Cuda.Error>(errorCode);

					// TODO - 7/31/2021 - @ahoefling - determine th perf impact
					var buffer = new byte[bitmapLength];
					var data = (byte*)bitmapAddress;
					for (int i = 0; i < bitmapLength; i++)
						buffer[i] = data[i];

					// TODO - 7/31/2021 - @ahoefling - determine th perf impact
					Cuda.FreeMemory(bitmapAddress);

					var bitmapHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					imageData.AddHandle(bitmapHandle);

					var properties = new ImageProperties(imageData.Width, imageData.Bits, imageData.Colors);
					var bitmap = new Bitmap(imageData.Width, imageData.Height,
						properties.Stride,
						System.Drawing.Imaging.PixelFormat.Format24bppRgb,
						bitmapHandle.AddrOfPinnedObject());

					return bitmap;
				}
				else
				{
					var properties = new ImageProperties(imageData.Width, imageData.Bits, imageData.Colors);
					var additionalBytes = properties.Offset * imageData.Height;
					var buffer = new byte[imageData.Buffer.Length + additionalBytes];

					var bitmapPosition = 0;
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

					var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					imageData.AddHandle(handle);

					var bitmap = new Bitmap(imageData.Width, imageData.Height,
						properties.Stride,
						System.Drawing.Imaging.PixelFormat.Format24bppRgb,
						handle.AddrOfPinnedObject());

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

			memory.Write(imageData.Buffer, 0, imageData.Buffer.Length);
			memory.Seek(0, SeekOrigin.Begin);
			
			return memory;
		}
	}
}
