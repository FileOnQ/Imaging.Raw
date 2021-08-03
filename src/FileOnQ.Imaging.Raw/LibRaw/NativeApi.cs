﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace FileOnQ.Imaging.Raw
{
	public unsafe partial class LibRaw
	{
#if NET5_0_OR_GREATER
		static LibRaw()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					path = Path.Combine(path, "runtimes\\win-x86\\native");
					break;
				case Architecture.X64:
					path = Path.Combine(path, "runtimes\\win-x64\\native");
					break;
			}

			SetDllDirectory(path);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool SetDllDirectory(string path);
#endif

		internal static IntPtr Initialize(uint flags)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_init(flags);
				case Architecture.X64:
					return X64.libraw_init(flags);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void Close(IntPtr libraw)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					X86.libraw_close(libraw);
					break;
				case Architecture.X64:
					X64.libraw_close(libraw);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void Recycle(IntPtr libraw)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					X86.libraw_recycle(libraw);
					break;
				case Architecture.X64:
					X64.libraw_recycle(libraw);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static Error OpenFile(IntPtr libraw, string filename)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_open_file(libraw, filename);
				case Architecture.X64:
					return X64.libraw_open_file(libraw, filename);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static Error UnpackThumbnail(IntPtr libraw)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_unpack_thumb(libraw);
				case Architecture.X64:
					return X64.libraw_unpack_thumb(libraw);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static Error ThumbnailWriter(IntPtr libraw, string filename)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_dcraw_thumb_writer(libraw, filename);
				case Architecture.X64:
					return X64.libraw_dcraw_thumb_writer(libraw, filename);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static ProcessedImage* MakeMemoryThumbnail(IntPtr libraw, ref Error errorCode)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_dcraw_make_mem_thumb(libraw, ref errorCode);
				case Architecture.X64:
					return X64.libraw_dcraw_make_mem_thumb(libraw, ref errorCode);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static ProcessedImage* MakeMemoryImage(IntPtr libraw, ref Error errorCode)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_dcraw_make_mem_image(libraw, ref errorCode);
				case Architecture.X64:
					return X64.libraw_dcraw_make_mem_image(libraw, ref errorCode);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static Error Unpack(IntPtr libraw)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_unpack(libraw);
				case Architecture.X64:
					return X64.libraw_unpack(libraw);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static Error DcrawProcess(IntPtr libraw)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					return X86.libraw_dcraw_process(libraw);
				case Architecture.X64:
					return X64.libraw_dcraw_process(libraw);
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void SetOutputTiff(IntPtr libraw, int value)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					X86.libraw_set_output_tif(libraw, value);
					break;
				case Architecture.X64:
					X64.libraw_set_output_tif(libraw, value);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void DcrawWriter(IntPtr libraw, string filename)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					X86.libraw_dcraw_ppm_tiff_writer(libraw, filename);
					break;
				case Architecture.X64:
					X64.libraw_dcraw_ppm_tiff_writer(libraw, filename);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}

		internal static void ClearMemory(ProcessedImage* image)
		{
			switch (RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X86:
					X86.libraw_dcraw_clear_mem((IntPtr)image);
					break;
				case Architecture.X64:
					X64.libraw_dcraw_clear_mem((IntPtr)image);
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
				default:
					throw new NotSupportedException($"Current platform ({RuntimeInformation.ProcessArchitecture}) is not supported");
			}
		}
	}
}
