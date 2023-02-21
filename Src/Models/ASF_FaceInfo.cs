namespace Yj.ArcSoftSDK._4_0.Models
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
        /// 0：非真人；1：真人；-1：不确定；-2:传入人脸数>1；-3: 人脸过小 -4: 角度过大 -5: 人脸超出边界  -6: 深度图错误 -7: 红外图太亮了
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

        /// <summary>
        /// 口罩 "0" 代表没有带口罩，"1"代表带口罩 ,"-1"表不确定
        /// </summary>
        public int Mask { set; get; }
        /// <summary>
        /// 戴眼镜置信度[0-1],推荐阈值0.5
        /// </summary>
        public float WearGlasses { set; get; }
        /// <summary>
        /// 左眼状态
        /// </summary>
        public bool IsLeftEyeClosed { set; get; }
        /// <summary>
        /// 右眼状态
        /// </summary>
        public bool IsRightEyeClosed { set; get; }
        /// <summary>
        /// "1" 表示 遮挡, "0" 表示 未遮挡, "-1" 表示不确定
        /// </summary>
        public int FaceShelter { set; get; }
        /// <summary>
        /// 额头坐标 Empty 表示无效
        /// </summary>
        public PointF FaceLandPoint { set; get; } = PointF.Empty;
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