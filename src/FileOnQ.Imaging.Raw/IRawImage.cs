using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IRawImage : IDisposable
	{
		IImageWriter UnpackThumbnail();
		IUnpackedImage UnpackRaw();
	}
}