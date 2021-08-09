using System;

namespace FileOnQ.Imaging.Raw
{
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
		void Write(RawImageData data, string file);
		ProcessedImage AsProcessedImage(RawImageData data);
	}
}
