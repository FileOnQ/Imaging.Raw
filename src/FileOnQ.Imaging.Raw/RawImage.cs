using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class RawImage : IDisposable
	{
		string file;
		IntPtr libraw;
		LibRaw.LibRawProcessedImage* thumbnailPointer;
		
		public RawImage(string file)
		{
			this.file = file;
			libraw = LibRaw.libraw_init(0);
			//LibRaw.libraw_open_file(libraw, file);
		}

		public void UnpackThumbnail()
		{
			// if (thumbnailPointer != IntPtr.Zero)
			// {
			// 	LibRaw.libraw_dcraw_clear_mem(libraw);
			// 	thumbnailPointer = IntPtr.Zero;
			// }
				
			LibRaw.libraw_unpack_thumb(libraw);
			
			// Loading the thumbnail into memory adds double the processing time
			var errorCode = 0;
			thumbnailPointer = LibRaw.libraw_dcraw_make_mem_thumb(libraw, ref errorCode);
			// TODO - check error codes and throw exception if there is a problem.
			
			
		}

		// strategy 0 - Buffer.MemoryCopy
		// strategy 1 = Marshal.Copy
		// strategy 2 = Unsafe.CopyBlock
		// strategy 3 = Unsafe.CopyBlockUnaligned
		// strateg 4 = reassign memory address for each index
		public byte[] GetThumbnailAsByteArray(int strategy = 0)
		{
			// PtrToStructure is slow
			//var thumbnail = Marshal.PtrToStructure<LibRaw.LibRawProcessedImage>(thumbnailPointer);

			var buffer = new byte[thumbnailPointer->DataSize];
			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			var destination = handle.AddrOfPinnedObject();
			
			// Array.Resize(ref thumbnail.Data, (int)thumbnail.DataSize);

			var address = (IntPtr)thumbnailPointer + Marshal.OffsetOf(typeof(LibRaw.LibRawProcessedImage), "Data").ToInt32();
			var byteCount = buffer.Length;

			if (strategy == 0)
			{
				System.Buffer.MemoryCopy(address.ToPointer(), destination.ToPointer(), byteCount, byteCount);
			}
			else if (strategy == 1)
			{
				Marshal.Copy(address, buffer, 0, buffer.Length);
			}
#if NET5_0_OR_GREATER
			else if (strategy == 2)
			{
				Unsafe.CopyBlock(address.ToPointer(), destination.ToPointer(), (uint) byteCount);
			}
			else if (strategy == 3)
			{
				Unsafe.CopyBlockUnaligned(address.ToPointer(), destination.ToPointer(), (uint) byteCount);
			}
#endif
			else if (strategy == 4)
			{
				var d = (int*) destination.ToPointer();
				var s = (int*) address.ToPointer();
				var index = 0;
				do
				{
					*d = *s;
					d++;
					s++;
					index += 4; // might be 8 for 64 bit

				} while (index < byteCount);
			}


			return buffer;
			
			//
			// File.WriteAllBytes(@"D:\DigitalOnQ\DigitalOnQ\src\FileOnQ.DigitalOnQ.Imaging.Console\bin\Debug\net48\test.jpeg", thumbnail.Data);
			// LibRaw.libraw_dcraw_clear_mem(libraw);
		}
		
		public Span<byte> GetThumbnailAsSpan()
		{
			var address = (IntPtr)thumbnailPointer + Marshal.OffsetOf(typeof(LibRaw.LibRawProcessedImage), "Data").ToInt32();
			return new Span<byte>(address.ToPointer(), (int)thumbnailPointer->DataSize);
		}

		public void WriteTiff(string path)
		{
			LibRaw.libraw_open_file(libraw, file);
			var error = LibRaw.libraw_unpack(libraw);
			error = LibRaw.libraw_dcraw_process(libraw);
			LibRaw.libraw_set_output_tif(libraw, 1);
			
			LibRaw.libraw_dcraw_ppm_tiff_writer(libraw, path);
		}
		
		public void WriteThumbnail(string file) =>
			LibRaw.libraw_dcraw_thumb_writer(libraw, file);

		~RawImage() => Dispose(false);

		bool isDisposed;
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed)
				return;

			if (disposing)
			{
				// free managed resources
			}

			if (thumbnailPointer != null)
			{
				LibRaw.libraw_dcraw_clear_mem((IntPtr)thumbnailPointer);
				thumbnailPointer = null;
			}
			
			if (libraw != IntPtr.Zero)
			{
				LibRaw.libraw_close(libraw);
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}
