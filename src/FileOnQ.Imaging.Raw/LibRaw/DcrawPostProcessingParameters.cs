using System.Runtime.InteropServices;

namespace FileOnQ.Imaging.Raw
{
	public unsafe partial class LibRaw
	{
		/// <summary>
		/// Fields of this struct map to CLI parameters of dcraw.exe.
		/// </summary>
		/// <remarks>
		/// This struct maps to the LibRaw native struct libraw_ouput_params_t.
		/// The documentation is pulled from 
		/// https://www.libraw.org/docs/API-datastruct.html#libraw_output_params_t
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DcrawPostProcessingParameters
		{
			/// <summary>
			/// 4 numbers corresponding to the coordinates (in pixels) of the 
			/// rectangle that is used to calculate the white balance. X and Y 
			/// are coordinates of the left-top rectangle corner; w and h are 
			/// the rectangle's width and height, respectively.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -A x y w h
			/// </remarks>
			public uint Greybox_0;   /* -A  x1 y1 x2 y2 */

			/// <summary>
			/// 4 numbers corresponding to the coordinates (in pixels) of the 
			/// rectangle that is used to calculate the white balance. X and Y 
			/// are coordinates of the left-top rectangle corner; w and h are 
			/// the rectangle's width and height, respectively.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -A x y w h
			/// </remarks>
			public uint Greybox_1;

			/// <summary>
			/// 4 numbers corresponding to the coordinates (in pixels) of the 
			/// rectangle that is used to calculate the white balance. X and Y 
			/// are coordinates of the left-top rectangle corner; w and h are 
			/// the rectangle's width and height, respectively.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -A x y w h
			/// </remarks>
			public uint Greybox_2;

			/// <summary>
			/// 4 numbers corresponding to the coordinates (in pixels) of the 
			/// rectangle that is used to calculate the white balance. X and Y 
			/// are coordinates of the left-top rectangle corner; w and h are 
			/// the rectangle's width and height, respectively.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -A x y w h
			/// </remarks>
			public uint Greybox_3;

			/// <summary>
			/// This field sets the image cropping rectangle. Cropbox[0] and 
			/// cropbox[1] are the rectangle's top-left corner coordinates,
			/// remaining two values are width and height respectively. All 
			/// coordinates are applied before any image rotation.
			/// </summary>
			/// <remarks>
			/// dcraw keys: none
			/// </remarks>
			public uint Cropbox_0;   /* -B x1 y1 x2 y2 */

			/// <summary>
			/// This field sets the image cropping rectangle. Cropbox[0] and 
			/// cropbox[1] are the rectangle's top-left corner coordinates,
			/// remaining two values are width and height respectively. All 
			/// coordinates are applied before any image rotation.
			/// </summary>
			/// <remarks>
			/// dcraw keys: none
			/// </remarks>
			public uint Cropbox_1;

			/// <summary>
			/// This field sets the image cropping rectangle. Cropbox[0] and 
			/// cropbox[1] are the rectangle's top-left corner coordinates,
			/// remaining two values are width and height respectively. All 
			/// coordinates are applied before any image rotation.
			/// </summary>
			/// <remarks>
			/// dcraw keys: none
			/// </remarks>
			public uint Cropbox_2;

			/// <summary>
			/// This field sets the image cropping rectangle. Cropbox[0] and 
			/// cropbox[1] are the rectangle's top-left corner coordinates,
			/// remaining two values are width and height respectively. All 
			/// coordinates are applied before any image rotation.
			/// </summary>
			/// <remarks>
			/// dcraw keys: none
			/// </remarks>
			public uint Cropbox_3;

			/// <summary>
			/// Correction of chromatic aberrations; the only specified values are
			/// CorrectionOfChromaticAberrations_0, the red multiplier
			/// CorrectionOfChromaticAberrations_2, the blue multiplier.
			/// For some formats, it affects RAW data reading , since correction of aberrations changes the output size.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -C
			/// </remarks>
			public double CorrectionOfChromaticAberrations_0;        /* -C */

			/// <summary>
			/// Correction of chromatic aberrations; the only specified values are
			/// CorrectionOfChromaticAberrations_0, the red multiplier
			/// CorrectionOfChromaticAberrations_2, the blue multiplier.
			/// For some formats, it affects RAW data reading , since correction of aberrations changes the output size.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -C
			/// </remarks>
			public double CorrectionOfChromaticAberrations_1;

			/// <summary>
			/// Correction of chromatic aberrations; the only specified values are
			/// CorrectionOfChromaticAberrations_0, the red multiplier
			/// CorrectionOfChromaticAberrations_2, the blue multiplier.
			/// For some formats, it affects RAW data reading , since correction of aberrations changes the output size.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -C
			/// </remarks>
			public double CorrectionOfChromaticAberrations_2;

			/// <summary>
			/// Correction of chromatic aberrations; the only specified values are
			/// CorrectionOfChromaticAberrations_0, the red multiplier
			/// CorrectionOfChromaticAberrations_2, the blue multiplier.
			/// For some formats, it affects RAW data reading , since correction of aberrations changes the output size.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -C
			/// </remarks>
			public double CorrectionOfChromaticAberrations_3;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_0;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_1;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_2;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_3;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_4;

			/// <summary>
			/// Sets user gamma-curve. Library user should set first two fields of gamm array:
			/// Gamma_0 - inverted gamma value)
			/// Gamma_1 - slope for linear part(so called toe slope). Set to zero for simple power curve.
			/// Remaining 4 values are filled automatically.
			/// 
			/// By default settings for rec.BT.709 are used: power 2.222 (i.e.gamm[0]= 1 / 2.222) and 
			/// slope 4.5. For sRGB curve use gamm[0]=1/2.4 and gamm[1] = 12.92, for linear curve set gamm[0]/gamm[1] to 1.0.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -g power toe_slope
			/// </remarks>
			public double Gamma_5;

			/// <summary>
			/// 4 multipliers (r,g,b,g) of the user's white balance.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -r mul0 mul1 mul2 mul3
			/// </remarks>
			public float UserWhiteBalanceMultiplyer_0;

			/// <summary>
			/// 4 multipliers (r,g,b,g) of the user's white balance.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -r mul0 mul1 mul2 mul3
			/// </remarks>
			public float UserWhiteBalanceMultiplyer_1;

			/// <summary>
			/// 4 multipliers (r,g,b,g) of the user's white balance.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -r mul0 mul1 mul2 mul3
			/// </remarks>
			public float UserWhiteBalanceMultiplyer_2;

			/// <summary>
			/// 4 multipliers (r,g,b,g) of the user's white balance.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -r mul0 mul1 mul2 mul3
			/// </remarks>
			public float UserWhiteBalanceMultiplyer_3;

			/// <summary>
			/// Selection of image number for processing (for formats 
			/// that contain several RAW images in one file).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -s
			/// </remarks>
			public uint SelectedRawImage;

			/// <summary>
			/// Brightness (default 1.0).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -b
			/// </remarks>
			public float Brightness;

			/// <summary>
			/// Parameter for noise reduction through wavelet denoising.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -n
			/// </remarks>
			public float NoiseReduction;

			/// <summary>
			/// Outputs the image in 50% size. For some formats, it affects 
			/// RAW data reading.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -h
			/// </remarks>
			public int HalfSize;

			/// <summary>
			/// Switches on separate interpolations for two green components.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -f
			/// </remarks>
			public int FourColorRgb;

			/// <summary>
			/// 0-9: Highlight mode (0=clip, 1=unclip, 2=blend, 3+=rebuild).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -H
			/// </remarks>
			public int Highlight;

			/// <summary>
			/// Use automatic white balance obtained after averaging over 
			/// the entire image.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -a
			/// </remarks>
			public int UseAutoWhiteBalance;

			/// <summary>
			/// If possible, use the white balance from the camera.
			/// If camera-recorded WB is not available, dcraw_process() will fallback to:
			/// * Auto-WB if bit LIBRAW_PROCESSING_CAMERAWB_FALLBACK_TO_DAYLIGHT is not 
			///   set in params.raw_processing_options (or for the rare specific case: 
			///   no valid WB index was parsed from CRW file)
			/// * Daylight-WB if abovementioned bit is not set.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -w
			/// </remarks>
			public int UseCameraWhiteBalance;

			/// <summary>
			/// * 0: do not use embedded color profile
			/// * 1 (default): use embedded color profile (if present) for DNG files (always); 
			///   for other files only if use_camera_wb is set;
			/// * 3: use embedded color data (if present) regardless of white balance setting.
			/// </summary>
			/// <remarks>
			/// dcraw keys: +M/-M
			/// </remarks>
			public int UseCameraMatrix;

			/// <summary>
			/// [0-6] Output colorspace (raw, sRGB, Adobe, Wide, ProPhoto, XYZ, ACES).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -o
			/// </remarks>
			public int OutputColor;

			/// <summary>
			/// Path to output profile ICC file (used only if LibRaw compiled with LCMS support)
			/// </summary>
			/// <remarks>
			/// dcraw keys: -o filename
			/// </remarks>
			public char* OutputProfile;

			/// <summary>
			/// Path to input (camera) profile ICC file (or 'embed' for embedded profile). Used 
			/// only if LCMS support compiled in.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -p file
			/// </remarks>
			public char* CameraProfile;

			/// <summary>
			/// Path to file with bad pixels map (in dcraw format: 
			/// "column row date-of-pixel-death-in-UNIX-format", one pixel per row).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -P file
			/// </remarks>
			public char* BadPixels;

			/// <summary>
			/// Path to dark frame file (in 16-bit PGM format)
			/// </summary>
			/// <remarks>
			/// dcraw keys: -K file
			/// </remarks>
			public char* DarkFrame;

			/// <summary>
			/// 8 bit (default)/16 bit (key -4).
			/// </summary>
			/// <remarks>
			/// dcraw keys: -4
			/// </remarks>
			public int OutputBps;

			/// <summary>
			/// 0/1: output PPM/TIFF.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -T
			/// </remarks>
			public int OutputTiff;

			public int OutputFlags;

			/// <summary>
			/// [0-7] Flip image (0=none, 3=180, 5=90CCW, 6=90CW). Default -1, which means 
			/// taking the corresponding value from RAW.
			/// 
			/// For some formats, affects RAW data reading, e.g., unpacking of thumbnails 
			/// from Kodak cameras.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -t
			/// </remarks>
			public int FlipImage;

			/// <summary>
			/// 0-10: interpolation quality:
			/// * 0 - linear interpolation
			/// * 1 - VNG interpolation
			/// * 2 - PPG interpolation
			/// * 3 - AHD interpolation
			/// * 4 - DCB interpolation
			/// * 11 - DHT intepolation
			/// * 12 - Modified AHD intepolation(by Anton Petrusevich)
			/// </summary>
			/// <remarks>
			/// dcraw keys: -q
			/// </remarks>
			public int InterpolationQuality;

			/// <summary>
			/// User black level.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -k
			/// </remarks>
			public int BlackLevel;

			/// <summary>
			/// Per-channel corrections to <see cref="BlackLevel"/>.
			/// </summary>
			public int UserCblack_0;

			/// <summary>
			/// Per-channel corrections to <see cref="BlackLevel"/>.
			/// </summary>
			public int UserCblack_1;

			/// <summary>
			/// Per-channel corrections to <see cref="BlackLevel"/>.
			/// </summary>
			public int UserCblack_2;

			/// <summary>
			/// Per-channel corrections to <see cref="BlackLevel"/>.
			/// </summary>
			public int UserCblack_3;

			/// <summary>
			/// Saturation Adjustment.
			/// </summary>
			/// <remarks>
			/// dcraw keys; -S
			/// </remarks>
			public int SaturationAdjustment;

			/// <summary>
			/// Number of median filter passes.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -m
			/// </remarks>
			public int MedianFilterPasses;

			/// <summary>
			/// Portion of clipped pixels when auto brighness increase is 
			/// used. Default value is 0.01 (1%) for dcraw compatibility. 
			/// Recommended value for modern low-noise multimegapixel 
			/// cameras depends on shooting style. Values in 0.001-0.00003 
			/// range looks reasonable.
			/// </summary>
			public float AutoBrightThreshold;

			/// <summary>
			/// This parameters controls auto-adjusting of maximum value 
			/// based on channel_maximum[] data, calculated from real frame
			/// data. If calculated maximum is greater than 
			/// adjust_maximum_thr*maximum, than maximum is set to 
			/// calculated_maximum.
			/// 
			/// Default: 0.75. If you set this value above 0.99999, than 
			/// default value will be used. If you set this value below 0.00001, 
			/// than no maximum adjustment will be performed.
			/// 
			/// Adjusting maximum should not damage any picture (esp. if you use 
			/// default value) and is very useful for correcting channel overflow 
			/// problems (magenta clouds on landscape shots, green-blue highlights 
			/// for indoor shots).
			/// </summary>
			public float AdjustMaximumThreshold;

			/// <summary>
			/// Don't use automatic increase of brightness by histogram.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -W
			/// </remarks>
			public int NoAutoBright;

			/// <summary>
			/// Default -1 (use), 0 - don't use rotation for cameras on a Fuji sensor.
			/// </summary>
			/// <remarks>
			/// dcraw keys: -j
			/// </remarks>
			public int UseFujiRotate;

			/// <summary>
			/// Turns on fixing of green channels disbalance. dcraw keys: none
			/// 
			/// Default: 0 (not use), 1 - turns on this postprocessing 
			/// stage.green_matching requires additional memory for image data.
			/// </summary>
			public int GreenMatching;

			/// <summary>
			/// Number of DCB correction passes. Default is -1 (no correction). 
			/// Useful only for DCB interpolation.
			/// </summary>
			public int DcbIterations;

			/// <summary>
			/// nonzero: DCB interpolation with enhance interpolated colors.
			/// </summary>
			public int DcbEnhanceFl;

			/// <summary>
			/// Controls FBDD noise reduction before demosaic.
			/// * 0 - do not use FBDD noise reduction
			/// * 1 - light FBDD reduction
			/// * 2 (and more) - full FBDD reduction
			/// </summary>
			public int FbddNoiseReduction;

			/// <summary>
			/// Exposure correction before demosaic.
			/// * ExposureCorrection: positive value turns the feature on(default: off).
			/// * ExposureShift: exposure shift in linear scale.Usable range from 0.25 
			///   (2-stop darken) to 8.0 (3-stop lighter). Default: 1.0 (no exposure shift).
			/// * ExposurePreservation: preserve highlights when lighten the image.Usable 
			///   range from 0.0 (no preservation) to 1.0 (full preservation). 0.0 is the 
			///   default value.
			/// </summary>
			public int ExposureCorrection;

			/// <summary>
			/// Exposure correction before demosaic.
			/// * ExposureCorrection: positive value turns the feature on(default: off).
			/// * ExposureShift: exposure shift in linear scale.Usable range from 0.25 
			///   (2-stop darken) to 8.0 (3-stop lighter). Default: 1.0 (no exposure shift).
			/// * ExposurePreservation: preserve highlights when lighten the image.Usable 
			///   range from 0.0 (no preservation) to 1.0 (full preservation). 0.0 is the 
			///   default value.
			/// </summary>
			public float ExposureShift;

			/// <summary>
			/// Exposure correction before demosaic.
			/// * ExposureCorrection: positive value turns the feature on(default: off).
			/// * ExposureShift: exposure shift in linear scale.Usable range from 0.25 
			///   (2-stop darken) to 8.0 (3-stop lighter). Default: 1.0 (no exposure shift).
			/// * ExposurePreservation: preserve highlights when lighten the image.Usable 
			///   range from 0.0 (no preservation) to 1.0 (full preservation). 0.0 is the 
			///   default value.
			/// </summary>
			public float ExposurePreservation;

			/// <summary>
			/// Disables pixel values scaling (call to LibRaw::scale_colors()) in 
			/// LibRaw::dcraw_process().
			/// 
			/// This is special use value because white balance is performed in 
			/// scale_colors(), so skipping it will result in non-balanced image.
			/// 
			/// This setting is targeted to use with no_interpolation, or with own 
			/// interpolation callback call.
			/// </summary>
			public int NoAutoScale;

			/// <summary>
			/// Disables call to demosaic code in LibRaw::dcraw_process()
			/// </summary>
			public int NoInterpolation;
		}
	}
}