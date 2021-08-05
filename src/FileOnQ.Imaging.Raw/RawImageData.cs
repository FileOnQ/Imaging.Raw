using System;

namespace FileOnQ.Imaging.Raw
{
	// NOTE - 8/4/2021 - @ahoefling - This wrapper exists so we can add buffer pointers to allow further customization in IImageProcessors
	public class RawImageData
	{
		public IntPtr LibRawData { get; set; }
	}
}
