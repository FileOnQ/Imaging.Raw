using System;

namespace FileOnQ.Imaging.Raw
{
	public interface IRawThumbnail
	{
		void Write(string file);
		Span<byte> GetSpan();
		byte[] CopyToByteArray();
	}
}