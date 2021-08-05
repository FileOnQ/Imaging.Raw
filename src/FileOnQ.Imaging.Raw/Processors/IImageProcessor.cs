using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IImageProcessor : IDisposable
	{
		void Process(IntPtr data);
		void Write(IntPtr data, string file);
		ProcessedImage AsProcessedImage(IntPtr data);
	}
}
