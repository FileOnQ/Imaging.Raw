using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IImageProcessor : IDisposable
	{
		void Process(RawImageData data);
		void Write(RawImageData data, string file);
		ProcessedImage AsProcessedImage(RawImageData data);
	}
}
