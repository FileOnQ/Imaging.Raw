using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public unsafe class ProcessedImage : IDisposable
	{
		List<GCHandle> handles = new List<GCHandle>();

		public byte[] Buffer { get; set; }
		public ImageType ImageType { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public int Colors { get; set; }
		public int Bits { get; set; }

		internal void AddHandle(GCHandle handle) => handles.Add(handle);

		~ProcessedImage() => Dispose(false);

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

			// free unmanaged resources
			foreach (var handle in handles)
			{
				if (handle != default && handle.IsAllocated)
					handle.Free();
			}

			isDisposed = true;
		}
	}
}
