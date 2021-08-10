using System;

namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// Processes LibRaw images using the standard Dcraw algorithm.
	/// </summary>
	/// <remarks>
	/// It is expected that the instance of <see cref="IUnpackedImage"/>
	/// was instantiated by `raw.UnpackRaw()`. This API uses specific
	/// internal LibRaw APIs that will only work on an unpacked
	/// raw image.
	/// </remarks>
	public class DcrawProcessor : IImageProcessor
	{
		/// <inheritdoc cref="IImageProcessor"/>
		public virtual bool Process(RawImageData data)
		{
			var errorCode = LibRaw.DcrawProcess(data.LibRawData);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);

			return true;
		}

		/// <inheritdoc cref="IImageProcessor"/>
		public virtual void Write(RawImageData data, string file)
		{
			// TODO - 7/27/2021 - @ahoefling - Add validation
			// In comparison to ThumbnailWriter() there is no
			// status. We may want to catch exceptions. Not sure
			// what can be thrown from LibRaw
			LibRaw.DcrawWriter(data.LibRawData, file);
		}

		/// <inheritdoc cref="IImageProcessor"/>
		public virtual ProcessedImage AsProcessedImage(RawImageData data) =>
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
		
		/// <inheritdoc cref="IDisposable"/>
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
