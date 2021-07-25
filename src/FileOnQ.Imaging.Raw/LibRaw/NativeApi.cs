using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public static unsafe partial class LibRaw
	{
#if NET5_0_OR_GREATER
		const string DllName = "libraw.dll";
#else
		const string DllName = "libraw32.dll";
#endif

		[DllImport(DllName)]
		internal static extern IntPtr libraw_init(uint flags);

		[DllImport(DllName)]
		internal static extern void libraw_close(IntPtr libraw);

		[DllImport(DllName)]
		internal static extern Error libraw_open_file(IntPtr libraw, string filename);

		[DllImport(DllName)]
		internal static extern Error libraw_unpack_thumb(IntPtr libraw);

		[DllImport(DllName)]
		internal static extern Error libraw_dcraw_thumb_writer(IntPtr libraw, string filename);

		[DllImport(DllName)]
		internal static extern ProcessedImage* libraw_dcraw_make_mem_thumb(IntPtr libraw, ref Error errorCode);

		[DllImport(DllName)]
		internal static extern Error libraw_unpack(IntPtr libraw);

		[DllImport(DllName)]
		internal static extern Error libraw_dcraw_process(IntPtr libraw);

		[DllImport(DllName)]
		internal static extern void libraw_set_output_tif(IntPtr libraw, int value);

		[DllImport(DllName)]
		internal static extern void libraw_dcraw_ppm_tiff_writer(IntPtr libraw, string filename);

		// This must pass the processed image so the native lib knows what to clear
		[DllImport(DllName)]
		internal static extern void libraw_dcraw_clear_mem(IntPtr image);
	}
}
