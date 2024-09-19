using System;

namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 多人脸检测结构体
    /// </summary>
    internal struct ASF_MultiFaceInfo
    {
        /// <summary>
        /// 人脸Rect结果集
        /// <see cref="MRECT"/>
        /// </summary>
        public IntPtr FaceRects;

        /// <summary>
        /// 人脸角度结果集，与faceRects一一对应  对应ASF_OrientCode
        /// <see cref="ASF_OrientCode"/>
        /// </summary>
        public IntPtr FaceOrients;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int FaceNum;

        /// <summary>
        /// face ID，IMAGE模式下不返回FaceID
        /// <para>int32</para>
        /// </summary>
        public IntPtr FaceID;
    }
}