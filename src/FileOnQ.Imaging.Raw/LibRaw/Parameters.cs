using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public unsafe partial class LibRaw
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Parameters
		{
			public uint Greybox_0;   /* -A  x1 y1 x2 y2 */
			public uint Greybox_1;
			public uint Greybox_2;
			public uint Greybox_3;
			public uint Cropbox_0;   /* -B x1 y1 x2 y2 */
			public uint Cropbox_1;
			public uint Cropbox_2;
			public uint Cropbox_3;
			public double Aber_0;        /* -C */
			public double Aber_1;
			public double Aber_2;
			public double Aber_3;
			public double Gamma_0;        /* -g */
			public double Gamma_1;
			public double Gamma_2;
			public double Gamma_3;
			public double Gamma_4;
			public double Gamma_5;
			public float UserMul_0;     /* -r mul0 mul1 mul2 mul3 */
			public float UserMul_1;
			public float UserMul_2;
			public float UserMul_3;
			public uint ShotSelect;  /* -s */
			public float Bright;          /* -b */
			public float Threshold;       /* -n */
			public int HalfSize;         /* -h */
			public int FourColorRgb;    /* -f */
			public int Highlight;         /* -H */
			public int UseAutoWhitebalance;       /* -a */
			public int UseCameraWhitebalance;     /* -w */
			public int UseCameraMatrix; /* +M/-M */
			public int OutputColor;      /* -o */
			public char* OutputProfile;  /* -o */
			public char* CameraProfile;  /* -p */
			public char* BadPixels;      /* -P */
			public char* DarkFrame;      /* -K */
			public int OutputBps;        /* -4 */
			public int OutputTiff;       /* -T */
			public int OutputFlags;
			public int UserFlip;         /* -t */
			public int UserQual;         /* -q */
			public int UserBlack;        /* -k */
			public int UserCblack_0;
			public int UserCblack_1;
			public int UserCblack_2;
			public int UserCblack_3;
			public int UserSat;          /* -S */
			public int MedPasses;        /* -m */
			public float AutoBrightThreshold;
			public float AdjustMaximumThreshold;
			public int NoAutoBright;    /* -W */
			public int UseFujiRotate;   /* -j */
			public int GreenMatching;
			/* DCB parameters */
			public int DcbIterations;
			public int DcbEnhanceFl;
			public int FbddNoiserd;
			public int ExpCorrec;
			public float ExpShift;
			public float ExpPreser;
			/* Disable Auto-scale */
			public int NoAutoScale;
			/* Disable intepolation */
			public int NoInterpolation;
		}
	}
}