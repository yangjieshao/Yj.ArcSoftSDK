#if !NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    /// <summary>
    /// </summary>
    public static partial class ASFFunctions
    {
        /// <summary>
        /// 采集当前设备信息
        /// </summary>
        public static string GetActiveDeviceInfo()
        {
            var deviceInfo = IntPtr.Zero;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                _ = ASFFunctions_Pro_Win.ASFGetActiveDeviceInfo(ref deviceInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                _ = ASFFunctions_Pro_Linux.ASFGetActiveDeviceInfo(ref deviceInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            return MemoryUtil.PtrToString(deviceInfo);
        }

        /// <summary>
        /// 离线激活
        /// </summary>
        /// <param name="activationFilePath">许可文件路径</param>
        public static int OfflineActivation(string activationFilePath)
        {
            if (string.IsNullOrWhiteSpace(activationFilePath)
                || !System.IO.File.Exists(activationFilePath))
            {
                return -1;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                var result = ASFFunctions_Pro_Win.ASFOfflineActivation(activationFilePath);
                if (result == 0
                    || result == 0x16002 /* SDK已激活 */
                    || result == 0x19007 /* 离线授权文件不可用，本地原有激活文件可继续使用 */)
                {
                    return 0;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                var result = ASFFunctions_Pro_Linux.ASFOfflineActivation(activationFilePath);
                if (result == 0
                    || result == 0x16002 /* SDK已激活 */
                    || result == 0x19007 /* 离线授权文件不可用，本地原有激活文件可继续使用 */)
                {
                    return 0;
                }
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            return -1;
        }

        /// <summary>
        /// 激活SDK
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="x86SdkKey"></param>
        /// <param name="x64SdkKey"></param>
        /// <param name="sox64Key"></param>
        /// <param name="x86ProActiveKey">永久授权版 秘钥 </param>
        /// <param name="x64ProActiveKey">永久授权版 秘钥 </param>
        /// <param name="sox64ProActiveKey">永久授权版 秘钥 </param>
        /// <param name="activationFilePath">许可文件路径 空表示离线激活</param>
        /// <returns></returns>
        public static int Activation(string appId, string x86SdkKey, string x64SdkKey, string sox64Key
            , string x86ProActiveKey = null, string x64ProActiveKey = null, string sox64ProActiveKey = null
            , string activationFilePath = null)
        {
            var result = OfflineActivation(activationFilePath);
            if (result == 0)
            {
                return result;
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64 )
            {
                var pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ActiveFileInfo)));

                if (!string.IsNullOrEmpty(x64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                var aSF_ActiveFileInfo = default(ASF_ActiveFileInfo);
                if (result == 0)
                {
                    aSF_ActiveFileInfo = (ASF_ActiveFileInfo)MemoryUtil.PtrToStructure(pASF_ActiveFileInfo, typeof(ASF_ActiveFileInfo));
                }
                MemoryUtil.Free(ref pASF_ActiveFileInfo);
                if (result == 0
                    && long.TryParse(aSF_ActiveFileInfo.EndTime, out long endTime)
                    && long.TryParse(aSF_ActiveFileInfo.StartTime, out long startTime)
                    && (DateTime.Now.ToTimestamp() / 1000) < endTime
                    && (DateTime.Now.ToTimestamp() / 1000) >= startTime
                    && (aSF_ActiveFileInfo.Platform == "windows_x64" || aSF_ActiveFileInfo.SdkType == "windows_x64"))
                {
                    return result;
                }

                if (!string.IsNullOrEmpty(x64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x64SdkKey, x64ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro64.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro64.dat");
                        result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x64SdkKey, x64ProActiveKey);
                    }
                }
            }
            else
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
            {
                var pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ActiveFileInfo)));

                if (!string.IsNullOrEmpty(x86ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                var aSF_ActiveFileInfo = default(ASF_ActiveFileInfo);
                if (result == 0)
                {
                    aSF_ActiveFileInfo = (ASF_ActiveFileInfo)MemoryUtil.PtrToStructure(pASF_ActiveFileInfo, typeof(ASF_ActiveFileInfo));
                }
                MemoryUtil.Free(ref pASF_ActiveFileInfo);
                if (result == 0
                    && long.TryParse(aSF_ActiveFileInfo.EndTime, out long endTime)
                    && long.TryParse(aSF_ActiveFileInfo.StartTime, out long startTime)
                    && (DateTime.Now.ToTimestamp() / 1000) < endTime
                    && (DateTime.Now.ToTimestamp() / 1000) >= startTime
                    && (aSF_ActiveFileInfo.Platform == "windows_x86" || aSF_ActiveFileInfo.SdkType == "windows_x86"))
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(x86ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x86SdkKey, x86ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro32.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro32.dat");
                        result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x86SdkKey, x86ProActiveKey);
                    }
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                var pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ActiveFileInfo)));

                if (!string.IsNullOrEmpty(sox64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Linux.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                var aSF_ActiveFileInfo = default(ASF_ActiveFileInfo);
                if (result == 0)
                {
                    aSF_ActiveFileInfo = (ASF_ActiveFileInfo)MemoryUtil.PtrToStructure(pASF_ActiveFileInfo, typeof(ASF_ActiveFileInfo));
                }
                MemoryUtil.Free(ref pASF_ActiveFileInfo);
                if (result == 0
                    && long.TryParse(aSF_ActiveFileInfo.EndTime, out long endTime)
                    && long.TryParse(aSF_ActiveFileInfo.StartTime, out long startTime)
                    && (DateTime.Now.ToTimestamp() / 1000) < endTime
                    && (DateTime.Now.ToTimestamp() / 1000) >= startTime
                    && aSF_ActiveFileInfo.Platform.Contains("linux" ))
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(sox64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Linux.ASFOnlineActivation(appId, sox64Key, sox64ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro64.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro64.dat");
                        result = ASFFunctions_Pro_Linux.ASFOnlineActivation(appId, sox64Key, sox64ProActiveKey);
                    }
                }
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            return result;
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="isImgMode"></param>
        /// <param name="faceMaxNum">[1,10]</param>
        /// <param name="isAngleZeroOnly"></param>
        /// <param name="needFaceInfo">需要人脸信息(性别、年龄、角度)</param>
        /// <param name="needRgbLive">需要rgb活体</param>
        /// <param name="needIrLive">需要红外活体</param>
        /// <param name="needFaceFeature"> 需要提取人脸特征值</param>
        /// <param name="needImageQuality"> 是否需要图像质量检测（只对虹软商用授权有效）</param>
        /// <param name="shelterThreshhold"> 设置遮挡算法检测的阈值（只对虹软4.0商用授权有效）</param>
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_ActiveFileInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_AgeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_Face3DAngle))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_FaceDataInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_FaceFeature))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_LandMarkInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_FaceLandmark))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_GenderInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_ImageQualityInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_LivenessInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_LivenessThreshold))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_MaskInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_MultiFaceInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_SingleFaceInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASF_VERSION))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ASVL_OFFSCREEN))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(LPASF_ImageData))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(MRECT))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(PointF))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Rectangle))]
        public static int InitEngine(ref IntPtr pEngine, bool isImgMode = false, int faceMaxNum = 5, bool isAngleZeroOnly = true,
            bool needFaceInfo = false, bool needRgbLive = false, bool needIrLive = false, bool needFaceFeature = true,
            bool needImageQuality = false, float shelterThreshhold = 0.8f)
        {
            if (faceMaxNum < 1)
            {
                faceMaxNum = 1;
            }
            if (faceMaxNum > 10)
            {
                faceMaxNum = 10;
            }
            //初始化引擎
            var detectMode = isImgMode ? ASF_DetectMode.ASF_DETECT_MODE_IMAGE : ASF_DetectMode.ASF_DETECT_MODE_VIDEO;
            //检测脸部的角度优先值
            var detectFaceOrientPriority = isAngleZeroOnly ? ASF_OrientPriority.ASF_OP_0_ONLY : ASF_OrientPriority.ASF_OP_ALL_OUT;
            //引擎初始化时需要初始化的检测功能组合
            var combinedMask = FaceEngineMask.ASF_FACE_DETECT;
            if (needFaceFeature)
            {
                combinedMask |= FaceEngineMask.ASF_FACERECOGNITION;
            }
            if (needFaceInfo)
            {
                // 年龄+性别 3M内存
                combinedMask = combinedMask | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER
                    | FaceEngineMask.ASF_FACELANDMARK | FaceEngineMask.ASF_FACESHELTER | FaceEngineMask.ASF_MASKDETECT | FaceEngineMask.ASF_UPDATE_FACEDATA;
            }
            if (needRgbLive)
            {
                combinedMask |= FaceEngineMask.ASF_LIVENESS;
            }
            if (needIrLive)
            {
                combinedMask |= FaceEngineMask.ASF_IR_LIVENESS;
            }
            if (needImageQuality)
            {
                combinedMask |= FaceEngineMask.ASF_IMAGEQUALITY;
            }
            int result;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                result = ASFFunctions_Pro_Win.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, faceMaxNum, (int)combinedMask, ref pEngine);
                if (result == 0 && needFaceInfo)
                {
                    result = ASFFunctions_Pro_Win.ASFSetFaceShelterParam(pEngine, shelterThreshhold);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                result = ASFFunctions_Pro_Linux.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, faceMaxNum, (int)combinedMask, ref pEngine);
                if (result == 0 && needFaceInfo)
                {
                    result = ASFFunctions_Pro_Linux.ASFSetFaceShelterParam(pEngine, shelterThreshhold);
                }
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            return result;
        }

        /// <summary>
        /// </summary>
        public static int UninitEngine(ref IntPtr pEngine)
        {
            int result;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                result = ASFFunctions_Pro_Win.ASFUninitEngine(pEngine);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                result = ASFFunctions_Pro_Linux.ASFUninitEngine(pEngine);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            pEngine = IntPtr.Zero;
            return result;
        }

        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="pFaceFeature1"></param>
        /// <param name="pFaceFeature2"></param>
        /// <param name="isIdcardCompare">是否证件照对比</param>
        public static float FaceFeatureCompare(IntPtr pEngine, IntPtr pFaceFeature1, IntPtr pFaceFeature2, bool isIdcardCompare)
        {
            float result = -1;
            ASF_CompareModel compareModel = isIdcardCompare ? ASF_CompareModel.ASF_ID_PHOTO : ASF_CompareModel.ASF_LIFE_PHOTO;

            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            if (retCode != 0
                || result > 1)
            {
                // 相似度不可能大于1
                result = -1;
            }
            return result;
        }
    }
}
#endif