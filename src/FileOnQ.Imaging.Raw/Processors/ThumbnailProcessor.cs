using System;

namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// Processes LibRaw thumbnails so they can be written
	/// to memory or to a storage location.
	/// </summary>
	/// <remarks>
	/// It is expected that the instance of <see cref="IUnpackedImage"/>
	/// was instantiated by `raw.UnpackThumbnail()`. This API uses
	/// specific internal LibRaw APIs that will only work on a thumbnail.
	/// </remarks>
	public class ThumbnailProcessor : IImageProcessor
	{
		/// <inheritdoc cref="IImageProcessor"/>
		public virtual bool Process(RawImageData data)
		{
			// left empty by design. The standard thumbnail processor
			// doesn't add additional functionality to this api. This
			// is designed so a downstream processor can extend the
			// base functionality and wouldn't need to implement
			// all the methods.
			return false;
		}

		/// <inheritdoc cref="IImageProcessor"/>
		public virtual void Write(RawImageData data, string file)
		{
			var error = LibRaw.ThumbnailWriter(data.LibRawData, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(error);
		}

		/// <inheritdoc cref="IImageProcessor"/>
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
		
		/// <inheritdoc cref="IDisposable"/>
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
