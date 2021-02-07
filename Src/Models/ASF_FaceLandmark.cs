using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal struct ASF_LandMarkInfo
    {
        /// <summary>
        /// <see cref="ASF_FaceLandmark"/>
        /// </summary>
        public IntPtr Point;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int Num;
    }
    /// <summary>
    /// 
    /// </summary>
    internal struct ASF_FaceLandmark
    {
        /// <summary>
        /// 
        /// </summary>
        public float X;

        /// <summary>
        /// 
        /// </summary>
        public float Y;
    }
}
