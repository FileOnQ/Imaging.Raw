using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public partial class LibRaw
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct LibRawProcessedImage
		{
			public ImageFormats Type;

			public ushort Height;

			public ushort Width;

			public ushort Colors;

			public ushort Bits;

			public uint DataSize;

			public IntPtr Data;
		}
	}
}