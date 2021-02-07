namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    ///
    /// </summary>
    public enum ASF_ImagePixelFormat
    {
        /// <summary>
        /// 8-bit Y 通道，8-bit 2x2 采样 V 与 U 分量交织通道
        /// </summary>
        ASVL_PAF_NV21 = 0x802,//2050

        /// <summary>
        /// 8-bit Y 通道，8-bit 2x2 采样 U 与 V 分量交织通道
        /// </summary>
        ASVL_PAF_NV12 = 0x801,//3059

        /// <summary>
        /// RGB 分量交织，按 B, G, R, B 字节序排布
        /// <para>R	R  R  R	 R	R  R  R  G  G  G  G  G  G  G  G  B  B  B  B  B  B  B  B</para>
        /// </summary>
        ASVL_PAF_RGB24_B8G8R8 = 0x201,//513

        /// <summary>
        /// YUV 分量交织， V 与 U 分量 2x1 采样，按 Y0, U0, Y1, V0 字节序排布
        /// </summary>
        ASVL_PAF_YUYV = 0x509,//1289

        /// <summary>
        /// 16-bit IR图像
        /// </summary>
        ASVL_PAF_DEPTH_U16 = 0xc02,//3074

        /// <summary>
        /// R  R  R  R  R  G  G  G  G  G  G  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB16_B5G6R5 = 0x101,

        /// <summary>
        /// X  R  R  R  R  R  G  G  G  G  G  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB16_B5G5R5 = 0x102,

        /// <summary>
        /// X  X  X  X  R  R  R  R  G  G  G  G  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB16_B4G4R4 = 0x103,

        /// <summary>
        /// T  R  R  R  R  R  G  G  G  G  G  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB16_B5G5R5T = 0x104,

        /// <summary>
        /// B  B  B  B  B  G  G  G  G  G  G  R  R  R  R  R
        /// </summary>
        ASVL_PAF_RGB16_R5G6B5 = 0x105,

        /// <summary>
        /// X  B  B  B  B  B  G  G  G  G  G  R  R  R  R  R
        /// </summary>
        ASVL_PAF_RGB16_R5G5B5 = 0x106,

        /// <summary>
        ///  X  X  X  X  B  B  B  B  G  G  G  G  R  R  R  R
        /// </summary>
        ASVL_PAF_RGB16_R4G4B4 = 0x107,

        /// <summary>
        /// X	X  X  X	 X	X  R  R  R  R  R  R  G  G  G  G  G  G  B  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB24_B6G6R6 = 0x202,

        /// <summary>
        /// X	X  X  X	 X	T  R  R  R  R  R  R  G  G  G  G  G  G  B  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB24_B6G6R6T = 0x203,

        /// <summary>
        ///  B  B  B  B  B  B  B  B  G  G  G  G  G  G  G  G  R	R  R  R	 R	R  R  R
        /// </summary>
        ASVL_PAF_RGB24_R8G8B8 = 0x204,

        /// <summary>
        /// X	X  X  X	 X	X  B  B  B  B  B  B  G  G  G  G  G  G  R  R  R  R  R  R
        /// </summary>
        ASVL_PAF_RGB24_R6G6B6 = 0x205,

        /// <summary>
        /// X	X  X  X	 X	X  X  X	 R	R  R  R	 R	R  R  R  G  G  G  G  G  G  G  G  B  B  B  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB32_B8G8R8 = 0x301,

        /// <summary>
        /// A	A  A  A	 A	A  A  A	 R	R  R  R	 R	R  R  R  G  G  G  G  G  G  G  G  B  B  B  B  B  B  B  B
        /// </summary>
        ASVL_PAF_RGB32_B8G8R8A8 = 0x302,

        /// <summary>
        /// X	X  X  X	 X	X  X  X	 B  B  B  B  B  B  B  B  G  G  G  G  G  G  G  G  R	R  R  R	 R	R  R  R
        /// </summary>
        ASVL_PAF_RGB32_R8G8B8 = 0x303,

        /// <summary>
        /// B    B  B  B  B  B  B  B  G  G  G  G  G  G  G  G  R  R  R  R  R  R  R  R  A	A  A  A  A	A  A  A
        /// </summary>
        ASVL_PAF_RGB32_A8R8G8B8 = 0x304,

        /// <summary>
        /// A    A  A  A  A  A  A  A  B  B  B  B  B  B  B  B  G  G  G  G  G  G  G  G  R  R  R  R  R	R  R  R
        /// </summary>
        ASVL_PAF_RGB32_R8G8B8A8 = 0x305,

        /// <summary>
        /// Y0, U0, V0
        /// </summary>
        ASVL_PAF_YUV = 0x401,

        /// <summary>
        /// Y0, V0, U0
        /// </summary>
        ASVL_PAF_YVU = 0x402,

        /// <summary>
        /// U0, V0, Y0
        /// </summary>
        ASVL_PAF_UVY = 0x403,

        /// <summary>
        /// V0, U0, Y0
        /// </summary>
        ASVL_PAF_VUY = 0x404,

        /// <summary>
        /// Y0, V0, Y1, U0
        /// </summary>
        ASVL_PAF_YVYU = 0x502,

        /// <summary>
        /// U0, Y0, V0, Y1
        /// </summary>
        ASVL_PAF_UYVY = 0x503,

        /// <summary>
        /// V0, Y0, U0, Y1
        /// </summary>
        ASVL_PAF_VYUY = 0x504,

        /// <summary>
        /// Y1, U0, Y0, V0
        /// </summary>
        ASVL_PAF_YUYV2 = 0x505,

        /// <summary>
        /// Y1, V0, Y0, U0
        /// </summary>
        ASVL_PAF_YVYU2 = 0x506,

        /// <summary>
        /// U0, Y1, V0, Y0
        /// </summary>
        ASVL_PAF_UYVY2 = 0x507,

        /// <summary>
        /// V0, Y1, U0, Y0
        /// </summary>
        ASVL_PAF_VYUY2 = 0x508,

        /// <summary>
        /// Y0, Y1, U0, V0
        /// </summary>
        ASVL_PAF_YYUV = 0x509,

        /// <summary>
        /// 8-bit Y 通道， 8-bit 2x2 采样 U 通道， 8-bit 2x2 采样 V 通道
        /// </summary>
        ASVL_PAF_I420 = 0x601,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 1x2 subsampled U and V planes
        /// </summary>
        ASVL_PAF_I422V = 0x602,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit ,2x1 subsampled U and V planes
        /// </summary>
        ASVL_PAF_I422H = 0x603,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit U and V planes
        /// </summary>
        ASVL_PAF_I444 = 0x604,

        /*8 bit Y plane followed by 8 bit 2x2 subsampled V and U planes*/

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_YV12 = 0x605,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 1x2 subsampled V and U planes
        /// </summary>
        ASVL_PAF_YV16V = 0x606,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 2x1 subsampled V and U planes
        /// </summary>
        ASVL_PAF_YV16H = 0x607,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit V and U planes
        /// </summary>
        ASVL_PAF_YV24 = 0x608,

        /// <summary>
        /// 8-bit IR图像
        /// </summary>
        ASVL_PAF_GRAY = 0x701,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 2x1 subsampled UV planes
        /// </summary>
        ASVL_PAF_LPI422H = 0x803,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 2x1 subsampled VU planes
        /// </summary>
        ASVL_PAF_LPI422H2 = 0x804,

        /// <summary>
        /// 8 bit Y plane followed by 8 bit 4x4 subsampled VU planes
        /// </summary>
        ASVL_PAF_NV41 = 0x805,

        /// <summary>
        /// Negative UYVY, U0, Y0, V0, Y1
        /// </summary>
        ASVL_PAF_NEG_UYVY = 0x901,

        /// <summary>
        /// Negative I420, 8 bit Y plane followed by 8 bit 2x2 subsampled U and V planes
        /// </summary>
        ASVL_PAF_NEG_I420 = 0x902,

        /// <summary>
        /// Mono UYVY, UV values are fixed, gray image in U0, Y0, V0, Y1
        /// </summary>
        ASVL_PAF_MONO_UYVY = 0xa01,

        /// <summary>
        /// Mono I420, UV values are fixed, 8 bit Y plane followed by 8 bit 2x2 subsampled U and V planes
        /// </summary>
        ASVL_PAF_MONO_I420 = 0xa02,

        /// <summary>
        /// P8_YUYV, 8 pixels a group, Y0Y1Y2Y3Y4Y5Y6Y7U0U1U2U3V0V1V2V3
        /// </summary>
        ASVL_PAF_P8_YUYV = 0xb03,

        /// <summary>
        /// P16_YUYV, 16*16 pixels a group, Y0Y1Y2Y3...U0U1...V0V1...
        /// </summary>
        ASVL_PAF_SP16UNIT = 0xc01,

        /// <summary>
        /// 10 bits RGGB CFA raw data, each data has 2 bytes
        /// </summary>
        ASVL_PAF_RAW10_RGGB_10B = 0xd01,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_GRBG_10B = 0xd02,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_GBRG_10B = 0xd03,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_BGGR_10B = 0xd04,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW12_RGGB_12B = 0xd05,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW12_GRBG_12B = 0xd06,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW12_GBRG_12B = 0xd07,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW12_BGGR_12B = 0xd08,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_RGGB_16B = 0xd09,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_GRBG_16B = 0xd0A,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_GBRG_16B = 0xd0B,

        /// <summary>
        ///
        /// </summary>
        ASVL_PAF_RAW10_BGGR_16B = 0xd0C,

        /// <summary>
        /// 10 bits gray raw data
        /// </summary>
        ASVL_PAF_RAW10_GRAY_10B = 0xe01,

        /// <summary>
        /// 10 bits gray raw data, each data has 2 bytes
        /// </summary>
        ASVL_PAF_RAW10_GRAY_16B = 0xe81,
    }
}