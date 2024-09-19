namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 单人脸检测结构体
    /// </summary>
    internal struct ASF_SingleFaceInfo
    {
        /// <summary>
        /// 人脸坐标Rect结果
        /// </summary>
        internal MRECT FaceRect;

        /// <summary>
        /// 人脸角度
        /// </summary>
        internal ASF_OrientCode FaceOrient;
    }
}