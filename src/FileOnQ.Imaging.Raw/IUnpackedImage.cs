namespace FileOnQ.Imaging.Raw
{
	public interface IUnpackedImage : IImageWriter
	{
		void Process(IImageProcessor properties);
	}
}