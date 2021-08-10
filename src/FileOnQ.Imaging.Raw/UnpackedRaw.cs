using System;

namespace FileOnQ.Imaging.Raw
{
	/// <inheritdoc cref="IUnpackedImage"/>
	unsafe class UnpackedRaw : UnpackedImage
	{
		/// <summary>
		/// Instantiates the default instance of
		/// <see cref="UnpackedRaw"/>.
		/// </summary>
		/// <param name="libraw">
		/// The pointer to the LibRaw data structure.
		/// </param>
		public UnpackedRaw(IntPtr libraw) : base(libraw) { }

		/// <inheritdoc cref="UnpackedImage"/>
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
