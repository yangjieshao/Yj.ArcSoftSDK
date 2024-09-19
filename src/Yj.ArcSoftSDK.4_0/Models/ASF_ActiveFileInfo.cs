namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 激活文件信息 (4.1算法)
    /// </summary>
    internal struct ASF_ActiveFileInfo
    {
        /// <summary>
        /// 开始时间 精度到毫秒的时间戳
        /// </summary>
        public string StartTime;

        /// <summary>
        /// 截止时间 精度到毫秒的时间戳
        /// </summary>
        public string EndTime;

        /// <summary>
        /// 激活码
        /// </summary>
        public string ActiveKey;

        /// <summary>
        /// 平台
        /// </summary>
        public string Platform;

        /// <summary>
        /// sdk类型
        /// </summary>
        public string SdkType;

        /// <summary>
        /// APPID
        /// </summary>
        public string AppId;

        /// <summary>
        /// SDKKEY
        /// </summary>
        public string SdkKey;

        /// <summary>
        /// SDK版本号
        /// </summary>
        public string SdkVersion;

        /// <summary>
        /// 激活文件版本号
        /// </summary>
        public string FileVersion;
    }
}