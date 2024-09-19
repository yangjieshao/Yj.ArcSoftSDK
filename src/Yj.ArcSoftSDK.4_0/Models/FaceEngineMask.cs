using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 引擎方法类型结构体，在初始化时将用到的类型用|连接传入，如 ASF_NONE|ASF_FACE_DETECT|ASF_FACERECOGNITION
    /// </summary>
    [Flags]
    internal enum FaceEngineMask
    {
        /// <summary>
        /// 不做方法初始化方法类型
        /// </summary>
#pragma warning disable S2346 // Flags enumerations zero-value members should be named "None"
        ASF_NONE = 0x00000000,
#pragma warning restore S2346 // Flags enumerations zero-value members should be named "None"

        /// <summary>
        /// 人脸检测
        /// </summary>
        ASF_FACE_DETECT = 0x00000001,

        /// <summary>
        /// 人脸特征
        /// <para>人脸识别方法类型常量，包含图片feature提取和feature比对</para>
        /// </summary>
        ASF_FACERECOGNITION = 0x00000004,

        /// <summary>
        /// 年龄
        /// </summary>
        ASF_AGE = 0x00000008,

        /// <summary>
        /// 性别
        /// </summary>
        ASF_GENDER = 0x00000010,

        /// <summary>
        /// 3D角度
        /// </summary>
        ASF_FACE3DANGLE = 0x00000020,

        /// <summary>
        /// 额头区域检测
        /// </summary>
        ASF_FACELANDMARK = 0x00000040,

        /// <summary>
        /// RGB活体
        /// </summary>
        ASF_LIVENESS = 0x00000080,

        /// <summary>
        /// 图像质量检测
        /// </summary>
        ASF_IMAGEQUALITY = 0x00000200,

        /// <summary>
        /// 红外活体
        /// </summary>
        ASF_IR_LIVENESS = 0x00000400,

        /// <summary>
        /// 人脸遮挡
        /// </summary>
        ASF_FACESHELTER = 0x00000800,

        /// <summary>
        /// 口罩检测
        /// </summary>
        ASF_MASKDETECT = 0x00001000,

        /// <summary>
        /// 人脸信息
        /// </summary>
        ASF_UPDATE_FACEDATA = 0x00002000,
    }
}