using System;

namespace FileOnQ.Imaging.Raw
{
	public class ThumbnailProcessor : IImageProcessor
	{
		public virtual bool Process(RawImageData data)
		{
			// left empty by design. The standard thumbnail processor
			// doesn't add additional functionality to this api. This
			// is designed so a downstream processor can extend the
			// base functionality and wouldn't need to implement
			// all the methods.
			return false;
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
				ImageType = data.ImageType,
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
