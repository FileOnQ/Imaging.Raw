using System;

namespace FileOnQ.Imaging.Raw
{
	public ref struct RawImageData
	{
		public IntPtr LibRawData { get; set; }
		public ImageType ImageType { get; set; }
		public int Bits { get; set; }
		public int Colors { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public ReadOnlySpan<byte> Buffer { get; set; }
	}
}
