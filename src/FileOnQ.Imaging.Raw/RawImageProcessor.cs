using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawImageProcessor : IImageProcessor
	{
		readonly IntPtr libraw;
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

			// get the memory address of the data buffer.
			var memoryOffset = Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), nameof(LibRaw.ProcessedImage.Data)).ToInt32();
			var address = (IntPtr)image + memoryOffset;

			return new ProcessedImage
			{
				// TODO - 7/24/2021 - @ahoefling
				// Change Func<bool> IsDisposed to a weak event handler. The current
				// implementation creates a strong GC reference between ProcessedImage
				// and RawThumbnail. This means that RawThumbnail will not be GCed
				// until the ProcessedImage is.
				IsDisposed = () => isDisposed,
				Image = image,
				ImageFormat = (ImageFormat)image->Type,
				Buffer = new Span<byte>(address.ToPointer(), (int)image->DataSize),
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

			isDisposed = true;
		}
	}
}
