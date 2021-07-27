using System;

namespace FileOnQ.Imaging.Raw
{
	unsafe class RawImageProcessor : IImageProcessor
	{
		readonly IntPtr libraw;
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

		public ProcessedImage AsProcessedImage() => throw new NotImplementedException();

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
