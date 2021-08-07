using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IRawImage : IDisposable
	{
		IUnpackedImage UnpackThumbnail();
		IUnpackedImage UnpackRaw();
	}
}