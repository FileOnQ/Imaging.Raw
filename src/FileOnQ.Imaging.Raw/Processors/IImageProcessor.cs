using System;

namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// The Image Processor contract which defines APIs for
	/// processing an imaging, writing and retrieving a
	/// memory buffer.
	/// </summary>
	public interface IImageProcessor : IDisposable
	{
		/// <summary>
		/// Applies a custom image processing algorithm on
		/// the <see cref="RawImageData"/>. When complete
		/// it returns true or false determining if the
		/// current image buffer needs to be reloaded.
		/// </summary>
		/// <param name="data">
		/// The <see cref="RawImageData"/> to perform the
		/// processing on.
		/// </param>
		/// <returns>
		/// If true, the image loaded in memory will be reset,
		/// else it will use the existing data loaded into memory.
		/// </returns>
		/// <remarks>
		/// When processing an image using LibRaw APIs you must
		/// return true otherwise the new buffer won't be loaded.
		/// If you are processing the image using anything other
		/// than LibRaw, you will want to return false.
		/// </remarks>
		bool Process(RawImageData data);
		
		/// <summary>
		/// Writes the processed image to the specified file path.
		/// </summary>
		/// <param name="data">
		///The <see cref="RawImageData"/> which has already been
		/// processed.
		/// </param>
		/// <param name="file">
		/// The file path to write the processed image to.
		/// </param>
		/// <exception cref="NullReferenceException">
		/// If <see cref="Process"/> isn't called first this API
		/// will throw a <see cref="NullReferenceException"/>.
		/// </exception>
		void Write(RawImageData data, string file);
		
		/// <summary>
		/// Gets a <see cref="ProcessedImage"/> which is a
		/// memory buffer of the data.
		/// </summary>
		/// <param name="data">
		/// The <see cref="RawImageData"/> which has already been
		/// processed.
		/// </param>
		/// <returns>
		/// The memory buffer of a processed image as type
		/// <see cref="ProcessedImage"/>
		/// </returns>
		/// <exception cref="NullReferenceException">
		/// If <see cref="Process"/> isn't called first this API
		/// will throw a <see cref="NullReferenceException"/>.
		/// </exception>
		ProcessedImage AsProcessedImage(RawImageData data);
	}
}
