using System;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class DcrawProcessor : IImageProcessor
	{
		public void Process(RawImageData data)
		{
			var errorCode = LibRaw.DcrawProcess(data.LibRawData);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);
		}

		public void Write(RawImageData data, string file)
		{
			// TODO - 7/27/2021 - @ahoefling - Add validation
			// In comparison to ThumbnailWriter() there is no
			// status. We may want to catch exceptions. Not sure
			// what can be thrown from LibRaw
			LibRaw.DcrawWriter(data.LibRawData, file);
		}

		public ProcessedImage AsProcessedImage(RawImageData data) =>
			new ProcessedImage
			{
				ImageType = data.ImageType,
				Buffer = data.Buffer.ToArray(), // TODO - Update ProcessedImage to use Span<T>
				Height = data.Height,
				Width = data.Width,
				Colors = data.Colors,
				Bits = data.Bits
			};

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

			isDisposed = true;
		}
	}
}
