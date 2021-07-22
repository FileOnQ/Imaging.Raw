using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public partial class LibRaw
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct LibRawData
		{
			[FieldOffset(12)]
			public LibRawThumbnailData Thumbnail;
		}
	}
}