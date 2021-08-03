﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using FileOnQ.Imaging.Raw.Interop;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class RawImage : IRawImage
	{
		string file;
		IntPtr libraw;
		
		// TODO - 7/25/2021 - @ahoefling
		// Add exception tests if file does not exist. If we
		// don't manage this in the C# code the problem will
		// persist in weird ways down stream such as OutOfOrderCall
		public RawImage(string file)
		{
			// TODO - 8/3/2021 - @ahoefling - This might make more sense to put in a library init layer.
			//        Currently this is the closest thing we have
			DllLoader.Configure();

			this.file = file;
			libraw = LibRaw.Initialize(0);
			if (libraw == IntPtr.Zero)
			{
				throw new Exception("Unable to initialize raw image");
			}

			var error = LibRaw.OpenFile(libraw, file);
			if (error != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(error);
		}

		public IImageWriter UnpackThumbnail()
		{
			var errorCode = LibRaw.UnpackThumbnail(libraw);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);

			return new RawThumbnail(libraw);
		}
		public IImageProcessor UnpackRaw()
		{
			var errorCode = LibRaw.Unpack(libraw);
			if (errorCode != LibRaw.Error.Success)
				throw new RawImageException<LibRaw.Error>(errorCode);

			return new RawImageProcessor(libraw);

			// TBD - this needs more design. We might just end up passing the libraw
			// pointer to the IImageProcessor

			throw new NotImplementedException();
			//var error = LibRaw.libraw_unpack(libraw);
			//error = LibRaw.libraw_dcraw_process(libraw);
			//LibRaw.libraw_set_output_tif(libraw, 1);

			//LibRaw.libraw_dcraw_ppm_tiff_writer(libraw, path);
		}


		// Keeping the code below as we need to run benchmarks with
		// various strategies.
		//
		// strategy 0 - Buffer.MemoryCopy
		// strategy 1 = Marshal.Copy
		// strategy 2 = Unsafe.CopyBlock
		// strategy 3 = Unsafe.CopyBlockUnaligned
		// strateg 4 = reassign memory address for each index
//		public byte[] GetThumbnailAsByteArray(int strategy = 0)
//		{
//			// PtrToStructure is slow
//			//var thumbnail = Marshal.PtrToStructure<LibRaw.LibRawProcessedImage>(thumbnailPointer);

//			var buffer = new byte[thumbnailPointer->DataSize];
//			var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
//			var destination = handle.AddrOfPinnedObject();
			
//			// Array.Resize(ref thumbnail.Data, (int)thumbnail.DataSize);

//			var address = (IntPtr)thumbnailPointer + Marshal.OffsetOf(typeof(LibRaw.ProcessedImage), "Data").ToInt32();
//			var byteCount = buffer.Length;

//			if (strategy == 0)
//			{
//				System.Buffer.MemoryCopy(address.ToPointer(), destination.ToPointer(), byteCount, byteCount);
//			}
//			else if (strategy == 1)
//			{
//				Marshal.Copy(address, buffer, 0, buffer.Length);
//			}
//#if NET5_0_OR_GREATER
//			else if (strategy == 2)
//			{
//				Unsafe.CopyBlock(address.ToPointer(), destination.ToPointer(), (uint) byteCount);
//			}
//			else if (strategy == 3)
//			{
//				Unsafe.CopyBlockUnaligned(address.ToPointer(), destination.ToPointer(), (uint) byteCount);
//			}
//#endif
//			else if (strategy == 4)
//			{
//				var d = (int*) destination.ToPointer();
//				var s = (int*) address.ToPointer();
//				var index = 0;
//				do
//				{
//					*d = *s;
//					d++;
//					s++;
//					index += 4; // might be 8 for 64 bit

//				} while (index < byteCount);
//			}


//			return buffer;
			
//			//
//			// File.WriteAllBytes(@"D:\DigitalOnQ\DigitalOnQ\src\FileOnQ.DigitalOnQ.Imaging.Console\bin\Debug\net48\test.jpeg", thumbnail.Data);
//			// LibRaw.libraw_dcraw_clear_mem(libraw);
//		}

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
			
			if (libraw != IntPtr.Zero)
			{
				LibRaw.Recycle(libraw);
				LibRaw.Close(libraw);
				libraw = IntPtr.Zero;
			}

			isDisposed = true;
		}
	}
}
