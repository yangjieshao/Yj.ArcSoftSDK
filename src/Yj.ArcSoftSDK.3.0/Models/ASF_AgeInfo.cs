using System;

namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 年龄结果结构体
    /// </summary>
    internal struct ASF_AgeInfo
    {
        /// <summary>
        /// 年龄检测结果集合
        /// </summary>
        public IntPtr AgeArray;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int Num;
    }
}