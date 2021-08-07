using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class DcrawProcessor : IImageProcessor
	{
		LibRaw.ProcessedImage* image;

		public void Process(RawImageData data)
		{
			// TODO - 7/27/2021 - @ahoefling - get properties from parameters.
			var errorCode = LibRaw.DcrawProcess(data.LibRawData);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);
		}

		// I might want to keep this on the processed image
		public void Write(RawImageData data, string file)
		{
			// TODO - 7/27/2021 - @ahoefling - Add validation
			LibRaw.DcrawWriter(data.LibRawData, file);
		}

		// this should remain on the processed image
		public ProcessedImage AsProcessedImage(RawImageData data)
		{
			if ((IntPtr)image == IntPtr.Zero)
			{
				var error = LibRaw.Error.Success;
				image = LibRaw.MakeMemoryImage(data.LibRawData, ref error);
				if (error != LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}

			if (image->Type == LibRaw.ImageFormat.Bitmap && (image->Width <= 0 || image->Height <= 0 || image->Colors <= 0 || image->Bits <= 0 || image->DataSize <= 0))
				throw new RawImageException<LibRaw.Error>(LibRaw.Error.InsufficientMemory);

			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			var address = (IntPtr)image + memoryOffset;

			// copy values from native memory to managed memory
			var buffer = new byte[image->DataSize];
			var ptr = (byte*)address;
			for (int position = 0; position < image->DataSize; position++)
				buffer[position] = ptr[position];

			return new ProcessedImage
			{
				ImageFormat = (ImageFormat)image->Type,
				Buffer = buffer,
				Height = image->Height,
				Width = image->Width,
				Colors = image->Colors,
				Bits = image->Bits
			};
		}

		~DcrawProcessor() => Dispose(false);

		bool isDisposed;
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed)
				return;

			if (disposing)
			{
				// free managed resources
			}

			// free unmanaged resources
			if (image != null)
			{
				LibRaw.ClearMemory(image);
				image = (LibRaw.ProcessedImage*)IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}
