using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal unsafe partial class Cuda
	{
		[DllImport("FileOnQ.Imaging.Raw.Gpu.Cuda.dll")]
		internal static extern IntPtr process_bitmap(IntPtr data, int size, ref Error error);
	}
}
