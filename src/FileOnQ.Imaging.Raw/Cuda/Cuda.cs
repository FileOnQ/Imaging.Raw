using System;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	internal unsafe partial class Cuda
	{
		internal static IntPtr ProcessBitmap(IntPtr data, int size, ref Error error)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return x86.process_bitmap(data, size, ref error);
				case Architecture.X64:
					return x64.process_bitmap(data, size, ref error);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}
	}
}
