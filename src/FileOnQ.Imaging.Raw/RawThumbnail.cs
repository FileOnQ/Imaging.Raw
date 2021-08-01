using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawThumbnail : IImageWriter
	{
		IntPtr libraw;
		LibRaw.ProcessedImage* thumbnail;

		public RawThumbnail(IntPtr libraw)
		{
			this.libraw = libraw;
		}

		public void Write(string file)
		{
			var error = LibRaw.ThumbnailWriter(libraw, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(error);
		}

		public ProcessedImage AsProcessedImage()
		{
			if ((IntPtr)thumbnail == IntPtr.Zero)
			{
				var error = LibRaw.Error.Success;
				thumbnail = LibRaw.MakeMemoryThumbnail(libraw, ref error);
				if (error != LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}

			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			var address = (IntPtr)thumbnail + memoryOffset;

			// copy values from native memory to managed memory
			var buffer = new byte[thumbnail->DataSize];
			var ptr = (byte*)address;
			for (int position = 0; position < thumbnail->DataSize; position++)
				buffer[position] = ptr[position];

			return new ProcessedImage
			{
				ImageFormat = (ImageFormat)thumbnail->Type,
				Buffer = buffer,
				Height = thumbnail->Height,
				Width = thumbnail->Width,
				Colors = thumbnail->Colors,
				Bits = thumbnail->Bits
			};
		}

		~RawThumbnail() => Dispose(false);

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

			if (thumbnail != null)
			{
				LibRaw.ClearMemory(thumbnail);
				thumbnail = (LibRaw.ProcessedImage*)IntPtr.Zero;
			}

			if (libraw == IntPtr.Zero)
			{
				// clear the pointer, but don't clear the memory. Let the pointer owner clear the memory
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}