using System;

namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 性别结构体
    /// </summary>
    internal struct ASF_GenderInfo
    {
        /// <summary>
        /// 性别检测结果集合
        /// </summary>
        public IntPtr GenderArray;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int Num;
    }
}