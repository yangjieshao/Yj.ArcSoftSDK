using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// </summary>
    internal struct ASF_FaceDataInfo
    {
        /// <summary>
        /// 人脸信息
        /// <para> FACE_DATA_SIZE 5000</para>
        /// do not free by self !!!
        /// </summary>
        public IntPtr Data;
        /// <summary>
        /// 人脸信息长度
        /// </summary>
        public int DataSize;
    }
}
