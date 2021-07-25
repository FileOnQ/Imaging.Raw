using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IRawImage : IDisposable
	{
		IImageWriter UnpackThumbnail();
		IImageProcessor UnpackRaw();
	}
}