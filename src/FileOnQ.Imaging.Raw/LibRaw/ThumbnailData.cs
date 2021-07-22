using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public partial class LibRaw
	{
		[StructLayout(LayoutKind.Explicit)]
		public unsafe struct LibRawThumbnailData
		{
			[FieldOffset(3)]
			public uint Length;

			[FieldOffset(5)]
			public char* Thumb;
		}
	}
}