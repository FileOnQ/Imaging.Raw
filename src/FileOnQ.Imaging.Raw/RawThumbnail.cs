using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawThumbnail : IImageWriter
	{
		readonly IntPtr libraw;
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

			return new ProcessedImage
			{
				// TODO - 7/24/2021 - @ahoefling
				// Change Func<bool> IsDisposed to a weak event handler. The current
				// implementation creates a strong GC reference between ProcessedImage
				// and RawThumbnail. This means that RawThumbnail will not be GCed
				// until the ProcessedImage is.
				IsDisposed = () => isDisposed,
				Image = thumbnail,
				ImageFormat = (ImageFormat)thumbnail->Type,
				Buffer = new Span<byte>(address.ToPointer(), (int)thumbnail->DataSize),
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

			isDisposed = true;
		}
	}
}