using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal partial class Cuda
	{
		private unsafe class x86
		{
			[DllImport("FileOnQ.Imaging.Raw.Gpu.Cuda32.dll")]
			internal static extern IntPtr process_bitmap(IntPtr data, int size, int width, int height, ref Error error);
		}
	}
}
