using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IImageWriter : IDisposable
	{
		void Write(string file);
		ProcessedImage AsProcessedImage();
	}

	public interface IImageProcessor : IImageWriter
	{

	}
}