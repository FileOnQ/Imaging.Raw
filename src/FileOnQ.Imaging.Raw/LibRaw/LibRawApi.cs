using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public static unsafe partial class LibRaw
	{
		[DllImport("libraw.dll")]
		internal static extern IntPtr libraw_init(uint flags);

		[DllImport("libraw.dll")]
		internal static extern void libraw_close(IntPtr libraw);

		[DllImport("libraw.dll")]
		internal static extern void libraw_open_file(IntPtr libraw, string filename);

		[DllImport("libraw.dll")]
		internal static extern LibRawError libraw_unpack_thumb(IntPtr libraw);

		[DllImport("libraw.dll")]
		internal static extern LibRawError libraw_dcraw_thumb_writer(IntPtr libraw, string filename);

		[DllImport("libraw.dll")]
		internal static extern LibRawProcessedImage* libraw_dcraw_make_mem_thumb(IntPtr libraw, ref LibRawError errorCode);

		[DllImport("libraw.dll")]
		internal static extern LibRawError libraw_unpack(IntPtr libraw);

		[DllImport("libraw.dll")]
		internal static extern LibRawError libraw_dcraw_process(IntPtr libraw);

		[DllImport("libraw.dll")]
		internal static extern void libraw_set_output_tif(IntPtr libraw, int value);

		[DllImport("libraw.dll")]
		internal static extern void libraw_dcraw_ppm_tiff_writer(IntPtr libraw, string filename);

		// This must pass the processed image so the native lib knows what to clear
		[DllImport("libraw.dll")]
		internal static extern void libraw_dcraw_clear_mem(IntPtr image);
	}
}
