using System;

namespace Yj.ArcSoftSDK._4_0.Models
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

        /// <summary>
        /// 戴眼镜置信度[0-1],推荐阈值0.5
        /// <see cref="float"/>
        /// </summary>
        public IntPtr WearGlasses;

        /// <summary>
        /// 左眼状态 0 未闭眼；1 闭眼
        /// <see cref="int"/>
        /// </summary>
        public IntPtr LeftEyeClosed;

        /// <summary>
        /// 右眼状态 0 未闭眼；1 闭眼
        /// <see cref="int"/>
        /// </summary>
        public IntPtr RightEyeClosed;

        /// <summary>
        /// "1" 表示 遮挡, "0" 表示 未遮挡, "-1" 表示不确定
        /// <see cref="int"/>
        /// </summary>
        public IntPtr FaceShelter;
        /// <summary>
        /// 多张人脸信息
        /// <see cref="ASF_FaceDataInfo"/>
        /// do not free by self !!!
        /// </summary>
        public IntPtr FaceDataInfoList;
    }
}