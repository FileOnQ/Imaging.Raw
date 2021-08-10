using System.Text;

namespace FileOnQ.Imaging.Raw
{
	static class BitmapUtilities
	{
		internal static byte[] CreateHeader(int width, int height) =>
			Encoding.ASCII.GetBytes($"P6\n{width} {height}\n255\n");
	}
}
