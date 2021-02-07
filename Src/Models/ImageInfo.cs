using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// 图片的像素数据
        /// </summary>
        public IntPtr ImgData { get; set; }

        /// <summary>
        /// 图片像素宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 图片像素高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 图片格式
        /// </summary>
        public ASF_ImagePixelFormat Format { get; set; }
    }
}