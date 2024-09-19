using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 3D人脸角度检测结构体
    /// </summary>
    internal struct ASF_Face3DAngle
    {
        /// <summary>
        ///
        /// </summary>
        public IntPtr Roll;

        /// <summary>
        ///
        /// </summary>
        public IntPtr Yaw;

        /// <summary>
        ///
        /// </summary>
        public IntPtr Pitch;

        ///// <summary>
        ///// 是否检测成功，0成功，其他为失败
        ///// </summary>
        //public IntPtr Status;

        /// <summary>
        ///
        /// </summary>
        public int Num;
    }
}