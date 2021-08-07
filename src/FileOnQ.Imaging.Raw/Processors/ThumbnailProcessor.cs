using System;

namespace FileOnQ.Imaging.Raw.Processors
{
	public class ThumbnailProcessor : IImageProcessor
	{
		public virtual void Process(RawImageData data)
		{
			// left empty by design. The standard thumbnail processor
			// doesn't add additional functionality to this api.
		}

		public virtual void Write(RawImageData data, string file)
		{
			var error = LibRaw.ThumbnailWriter(data.LibRawData, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(error);
		}

		public virtual ProcessedImage AsProcessedImage(RawImageData data) =>
			new ProcessedImage
			{
				//ImageFormat = (ImageFormat)thumbnail->Type,
				Buffer = data.Buffer.ToArray(),
				Height = data.Height,
				Width = data.Width,
				Colors = data.Colors,
				Bits = data.Bits
			};

		~ThumbnailProcessor() => Dispose(false);

		protected bool IsDisposed { get; set; }
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
				return;

			if (disposing)
			{
				// free managed resources
			}

			// free unmanaged resources

			IsDisposed = true;
		}
	}
}
