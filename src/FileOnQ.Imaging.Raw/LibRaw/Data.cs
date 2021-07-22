using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public partial class LibRaw
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct Data
		{
			[FieldOffset(12)]
			public ThumbnailData Thumbnail;
		}
	}
}