using System.Runtime.InteropServices;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// Define the image format space
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct ASVL_OFFSCREEN
    {
        /// <summary>
        /// 0x201 (513)
        /// </summary>
        public uint u32PixelArrayFormat;

        /// <summary>
        /// </summary>
        public int i32Width;

        /// <summary>
        /// </summary>
        public int i32Height;

        /// <summary>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
        public System.IntPtr[] ppu8Plane;

        /// <summary>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] pi32Pitch;
    }
}