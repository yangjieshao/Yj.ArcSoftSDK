using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 口罩信息
    /// </summary>
    internal struct ASF_MaskInfo
    {
        /// <summary>
        /// "0" 代表没有带口罩，"1"代表带口罩 ,"-1"表不确定
        /// </summary>
        public IntPtr MaskArray;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int Num;
    }
}
