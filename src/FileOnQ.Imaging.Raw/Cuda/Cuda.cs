using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal unsafe partial class Cuda
	{
		internal static bool IsCudaCapable()
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X64:
					return x64.is_cuda_capable();
				case Architecture.X86:
					return x86.is_cuda_capable();
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static IntPtr ProcessBitmap(IntPtr data, int size, int width, int height, ref int length, ref Error error)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X64:
					return x64.process_bitmap(data, size, width, height, ref length, ref error);
				case Architecture.X86:
					return x86.process_bitmap(data, size, width, height, ref length, ref error);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void FreeMemory(IntPtr pointer)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X64:
					x64.free_memory(pointer);
					break;
				case Architecture.X86:
					x86.free_memory(pointer);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");

			}
		}

	}
}
