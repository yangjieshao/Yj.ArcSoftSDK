namespace Yj.ArcSoftSDK.Models
{
    /// <summary>
    /// 人脸信息
    /// </summary>
    public class FaceInfo
    {
        /// <summary>
        ///
        /// </summary>
        internal ASF_SingleFaceInfo ASF_FaceInfo { set; get; }

        /// <summary>
        /// 人脸坐标Rect结果
        /// </summary>
        public Rectangle Rectangle { internal set; get; }

        /// <summary>
        /// 头像角度
        /// </summary>
        public int FaceOrient { internal set; get; }

        /// <summary>
        /// 0：男；1：女；
        /// </summary>
        public int Gender { internal set; get; }

        /// <summary>
        ///
        /// </summary>
        public int Age { internal set; get; }

        /// <summary>
        ///
        /// </summary>
        public Face3DAngle Face3DAngle { internal set; get; } = new Face3DAngle();

        /// <summary>
        /// RGB 活体
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；
        /// </summary>
        public int RgbLive { internal set; get; }

        /// <summary>
        /// 红外 活体
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；
        /// </summary>
        public int IrLive { internal set; get; }

        /// <summary>
        /// 特征
        /// </summary>
        public byte[] Feature { internal set; get; }

        /// <summary>
        ///  face ID，IMAGE模式下不返回FaceID
        /// </summary>
        public int FaceID { internal set; get; }

        /// <summary>
        /// 图像质量 -1无效
        /// </summary>
        public float ImageQuality { internal set; get; } = -1;
    }

    /// <summary>
    /// 3D人脸角度检测
    /// </summary>
    public class Face3DAngle
    {
        /// <summary>
        /// 是否检测成功，0成功，其他为失败
        /// </summary>
        public int Status { internal set; get; }

        /// <summary>
        ///
        /// </summary>
        public float Roll { internal set; get; }

        /// <summary>
        ///
        /// </summary>
        public float Yaw { internal set; get; }

        /// <summary>
        ///
        /// </summary>
        public float Pitch { internal set; get; }
    }
}