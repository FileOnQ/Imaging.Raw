using System;

namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// A raw unprocessed image.
	/// </summary>
	public interface IRawImage : IDisposable
	{
		/// <summary>
		/// Unpacks the embedded thumbnail if exists.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IUnpackedImage"/> which
		/// contains the embedded thumbnail.
		/// </returns>
		/// <exception cref="RawImageException{T}">
		/// If an error occurs during unpacking, an exception will be
		/// thrown which contains an error code explaining the problem.
		/// </exception>
		IUnpackedImage UnpackThumbnail();
		
		/// <summary>
		/// Unpacks the full raw image details.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IUnpackedImage"/> which
		/// contains the raw image details.
		/// </returns>
		/// <exception cref="RawImageException{T}">
		/// If an error occurs during unpacking, an exception will be
		/// thrown which contains an error code explaining the problem.
		/// </exception>
		IUnpackedImage UnpackRaw();
	}
}