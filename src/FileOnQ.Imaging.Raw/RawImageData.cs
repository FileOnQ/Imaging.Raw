using System;

namespace FileOnQ.Imaging.Raw
{
	// NOTE - 8/4/2021 - @ahoefling - This wrapper exists so we can add buffer pointers to allow further customization in IImageProcessors
	public ref struct RawImageData
	{
		public IntPtr LibRawData { get; set; }
		public int Bits { get; set; }
		public int Colors { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public ReadOnlySpan<byte> Buffer { get; set; }
	}
}
