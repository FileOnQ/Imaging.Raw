using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal partial class Cuda
	{
		private unsafe class x64
		{
			[DllImport("FileOnQ.Imaging.Raw.Gpu.Cuda.dll")]
			internal static extern bool is_cuda_capable();

			[DllImport("FileOnQ.Imaging.Raw.Gpu.Cuda.dll")]
			internal static extern IntPtr process_bitmap(IntPtr data, int size, int width, int height, ref Error error);
			
			[DllImport("FileOnQ.Imaging.Raw.Gpu.Cuda.dll")]
			internal static extern void free_memory(IntPtr pointer);
		}
	}
}
