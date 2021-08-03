using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal partial class Cuda
	{
		private unsafe class x64
		{
#if NETFRAMEWORK
			const string DllName = "FileOnQ.Imaging.Raw.Gpu.Cuda.dll";
#else
			const string DllName = "runtimes/win-x64/FileOnQ.Imaging.Raw.Gpu.Cuda.dll";
#endif

			[DllImport(DllName)]
			internal static extern bool is_cuda_capable();

			[DllImport(DllName)]
			internal static extern IntPtr process_bitmap(IntPtr data, int size, int width, int height, ref int length, ref Error error);
			
			[DllImport(DllName)]
			internal static extern void free_memory(IntPtr pointer);
		}
	}
}
