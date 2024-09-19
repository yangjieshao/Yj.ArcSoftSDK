using System;

namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 图像质量信息
    /// </summary>
    internal struct ASF_ImageQualityInfo
    {
        /// <summary>
        /// 人脸质量 float
        /// </summary>
        public IntPtr FaceQualityValue;

        /// <summary>
        /// 检测到的人脸数
        /// </summary>
        public int Num;
    }
}