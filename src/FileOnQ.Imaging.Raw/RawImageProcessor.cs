using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawImageProcessor : IImageProcessor
	{
		IntPtr libraw;
		LibRaw.ProcessedImage* image;
		public RawImageProcessor(IntPtr libraw)
		{
			this.libraw = libraw;
		}

		public void Process(IImageProcessorProperties properties)
		{
			// TODO - 7/27/2021 - @ahoefling - get properties from parameters.
			var errorCode = LibRaw.DcrawProcess(libraw);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);
		}

		public void Write(string file)
		{
			// TODO - 7/27/2021 - @ahoefling - Add validation
			LibRaw.DcrawWriter(libraw, file);
		}

		public ProcessedImage AsProcessedImage()
		{
			if ((IntPtr)image == IntPtr.Zero)
			{
				var error = LibRaw.Error.Success;
				image = LibRaw.MakeMemoryImage(libraw, ref error);
				if (error != LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}

			if (image->Type == LibRaw.ImageFormat.Bitmap)
			{
				var e = LibRaw.Error.Success;
				if (image->Width <= 0)
				{

				}

				if (image->Height <= 0)
				{

				}

				if (image-> Colors <= 0)
				{

				}

				if (image->Bits <= 0)
				{

				}

				if (image->DataSize <= 0)
				{

				}
			}

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

		~RawImageProcessor() => Dispose(false);

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

			if (libraw != IntPtr.Zero)
			{
				// Clear pointer, but don't clear memory, let the owner clear the memory
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}
