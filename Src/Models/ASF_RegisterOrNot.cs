namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 根据应用场景（注册照还是识别照）选择对应的模型进行人脸特征提取
    /// </summary>
    internal enum ASF_RegisterOrNot
    {
        /// <summary>
        /// 用于识别照人脸特征提取
        /// </summary>
        ASF_RECOGNITION = 0x0,

        /// <summary>
        /// 用于注册照人脸特征提取
        /// </summary>
        ASF_REGISTER = 0x1
    }
}
