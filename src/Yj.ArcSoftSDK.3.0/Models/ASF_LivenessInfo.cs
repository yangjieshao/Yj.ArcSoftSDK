using System;

namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    ///
    /// </summary>
    internal struct ASF_LivenessInfo
    {
        /// <summary>
        /// 是否是真人
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；
        /// </summary>
        public IntPtr IsLive;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int Num;
    }
}