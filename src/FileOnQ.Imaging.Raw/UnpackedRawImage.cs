using System;

namespace FileOnQ.Imaging.Raw
{
	unsafe class UnpackedRawImage : UnpackedImage
	{
		public UnpackedRawImage(IntPtr libraw) : base(libraw) { }

		protected override void LoadImage()
		{
			if ((IntPtr)Image == IntPtr.Zero)
			{
				var error = Raw.LibRaw.Error.Success;
				Image = Raw.LibRaw.MakeMemoryImage(LibRaw, ref error);
				if (error != Raw.LibRaw.Error.Success)
					throw new RawImageException<LibRaw.Error>(error);
			}
		}
	}
}
