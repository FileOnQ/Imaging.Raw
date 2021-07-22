using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IRawImage : IDisposable
	{
		IRawThumbnail UnpackThumbnail();
	}
}