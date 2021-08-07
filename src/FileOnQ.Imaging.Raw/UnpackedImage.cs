using System;

namespace FileOnQ.Imaging.Raw
{
	unsafe class UnpackedImage : IUnpackedImage
	{
		IntPtr libraw;
		IImageProcessor processor;

		internal UnpackedImage(IntPtr libraw) =>
			this.libraw = libraw;

		public void Process(IImageProcessor newProcessor)
		{
			if (processor != null)
				processor.Dispose();

			processor = newProcessor;
			processor.Process(new RawImageData { LibRawData = libraw });
		}

		public void Write(string file)
		{
			if (processor == null)
				throw new NullReferenceException("Call Process(IImageProcessor) first");

			processor.Write(new RawImageData
			{
				LibRawData = libraw
			}, file);
		}

		public ProcessedImage AsProcessedImage()
		{
			if (processor == null)
				throw new NullReferenceException("Call Process(IImageProcessor) first");

			return processor.AsProcessedImage(new RawImageData
			{
				LibRawData = libraw
			});
		}

		~UnpackedImage() => Dispose(false);

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
			if (processor != null)
			{
				// The processor has unmanaged memory so we are clearing it here
				// instead of in the managed area
				processor.Dispose();
				processor = null;
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
