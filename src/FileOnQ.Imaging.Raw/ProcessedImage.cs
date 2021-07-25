using System;

namespace FileOnQ.Imaging.Raw
{
	public unsafe ref struct ProcessedImage
	{
		Span<byte> buffer;
		public Span<byte> Buffer
		{
			get
			{
				ValidateImageMemory();
				return buffer;
			}
			set
			{
				ValidateImageMemory();
				buffer = value;
			}
		}

		ImageFormat imageFormat;
		public ImageFormat ImageFormat
		{
			get
			{
				ValidateImageMemory();
				return imageFormat;
			}
			set
			{
				ValidateImageMemory();
				imageFormat = value;
			}
		}

		int height;
		public int Height
		{
			get
			{
				ValidateImageMemory();
				return height;
			}
			set
			{
				ValidateImageMemory();
				height = value;
			}
		}

		int width;
		public int Width
		{
			get
			{
				ValidateImageMemory();
				return width;
			}
			set
			{
				ValidateImageMemory();
				width = value;
			}
		}

		int colors;
		public int Colors
		{
			get
			{
				ValidateImageMemory();
				return colors;
			}
			set
			{
				ValidateImageMemory();
				colors = value;
			}
		}

		int bits;
		public int Bits
		{
			get
			{
				ValidateImageMemory();
				return bits;
			}
			set
			{
				ValidateImageMemory();
				bits = value;
			}
		}

		/// <summary>
		/// Gets or sets the pointer to the <see cref="LibRaw.ProcessedImage"/>.
		/// </summary>
		/// <remarks>
		/// There is no guarentee that this pointer will exist. Always perform
		/// a null check prior to dereferencing it and retrieving data.
		/// </remarks>
		internal LibRaw.ProcessedImage* Image { get; set; }
		internal Func<bool> IsDisposed { get; set; }

		void ValidateImageMemory()
		{
			if (IsDisposed())
				throw new RawImageDisposedException(nameof(ProcessedImage));
		}
	}
}
