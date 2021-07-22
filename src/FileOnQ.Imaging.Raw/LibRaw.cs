using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	static unsafe class LibRaw
	{
		[DllImport("libraw.dll")]
		public static extern IntPtr libraw_init(uint flags);

		[DllImport("libraw.dll")]
		public static extern void libraw_close(IntPtr libraw);

		[DllImport("libraw.dll")]
		public static extern void libraw_open_file(IntPtr libraw, string filename);

		[DllImport("libraw.dll")]
		public static extern void libraw_unpack_thumb(IntPtr libraw);

		[DllImport("libraw.dll")]
		public static extern void libraw_dcraw_thumb_writer(IntPtr libraw, string filename);

		[DllImport("libraw.dll")]
		public static extern LibRawProcessedImage* libraw_dcraw_make_mem_thumb(IntPtr libraw, ref int errorCode);

		[DllImport("libraw.dll")]
		public static extern int libraw_unpack(IntPtr libraw);

		[DllImport("libraw.dll")]
		public static extern int libraw_dcraw_process(IntPtr libraw);

		[DllImport("libraw.dll")]
		public static extern void libraw_set_output_tif(IntPtr libraw, int value);

		[DllImport("libraw.dll")]
		public static extern void libraw_dcraw_ppm_tiff_writer(IntPtr libraw, string filename);

		// This must pass the processed image so the native lib knows what to clear
		[DllImport("libraw.dll")]
		public static extern void libraw_dcraw_clear_mem(IntPtr image);

		[StructLayout(LayoutKind.Explicit)]
		public struct LibRawData
		{
			[FieldOffset(12)]
			public LibRawThumbnailData Thumbnail;
		}

		[StructLayout(LayoutKind.Explicit)]
		public unsafe struct LibRawThumbnailData
		{
			[FieldOffset(3)]
			public uint Length;

			[FieldOffset(5)]
			public char* Thumb;
		}

		public enum ImageFormats
		{
			Jpeg = 1,
			Bitmap = 2
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct LibRawProcessedImage
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
