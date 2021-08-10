namespace FileOnQ.Imaging.Raw
{
	/// <summary>
	/// Unpacked image details which contains all the
	/// details about the image prior to processing.
	/// </summary>
	public interface IUnpackedImage : IImageWriter
	{
		/// <summary>
		/// Performs image processing algorithm from the
		/// <see cref="IImageProcessor"/>.
		/// </summary>
		/// <param name="imageProcessor">
		/// The image processor which contains all APIs
		/// for processing the image.
		/// </param>
		void Process(IImageProcessor imageProcessor);
	}
}