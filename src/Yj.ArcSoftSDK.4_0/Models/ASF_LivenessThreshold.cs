namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 活体置信度
    /// </summary>
    internal struct ASF_LivenessThreshold
    {
        /// <summary>
        /// BGR活体检测阈值设置，默认值0.5
        /// </summary>
        public float Thresholdmodel_BGR;
        /// <summary>
        /// IR活体检测阈值设置，默认值0.7
        /// </summary>
        public float Thresholdmodel_IR;
    }
}