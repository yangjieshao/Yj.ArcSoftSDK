using System.Runtime.InteropServices;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    ///
    /// </summary>
    internal struct LPASF_ImageData
    {
        /// <summary>
        /// 颜色格式
        /// </summary>
        public ASF_ImagePixelFormat u32PixelArrayFormat;

        /// <summary>
        /// 图像宽度
        /// </summary>
        public int i32Width;

        /// <summary>
        /// 图像高度
        /// </summary>
        public int i32Height;

        /// <summary>
        /// 图像数据
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.SysUInt)]
        public System.IntPtr[] ppu8Plane;

        /// <summary>
        ///
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] pi32Pitch;
    }
}