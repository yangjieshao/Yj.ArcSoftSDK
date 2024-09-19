#if NETFRAMEWORK
using System;
using Yj.ArcSoftSDK.Models;
using Yj.ArcSoftSDK.Utils;

namespace Yj.ArcSoftSDK
{
    /// <summary>
    /// </summary>
    public static partial class ASFFunctions
    {
        /// <summary>
        /// 激活SDK
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="x86SdkKey"></param>
        /// <param name="x64SdkKey"></param>
        /// <param name="sox64Key"></param>
        /// <param name="x86ProActiveKey">永久授权版 秘钥 为空则使用免费版</param>
        /// <param name="x64ProActiveKey">永久授权版 秘钥 为空则使用免费版</param>
        /// <param name="sox64ProActiveKey">永久授权版 秘钥 为空则使用免费版</param>

        public static int Activation(string appId, string x86SdkKey, string x64SdkKey, string sox64Key
            , string x86ProActiveKey = null, string x64ProActiveKey = null, string sox64ProActiveKey = null)
        {
            int result = -1;
            if (Environment.Is64BitProcess)
            {
                var pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ActiveFileInfo)));

                if (!string.IsNullOrEmpty(x64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                    IsPro = true;
                }
                else
                {
                    result = ASFFunctions_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
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
                    if (result != 0)
                    {
                        if (System.IO.File.Exists("ArcFacePro64.dat"))
                        {
                            System.IO.File.Delete("ArcFacePro64.dat");
                            result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x64SdkKey, x64ProActiveKey);
                        }
                    }
                    IsPro = true;
                }
                if (result != 0)
                {
                    result = ASFFunctions_Win.ASFOnlineActivation(appId, x64SdkKey);
                    if (result != 0)
                    {
                        if (System.IO.File.Exists("ArcFace64.dat"))
                        {
                            System.IO.File.Delete("ArcFace64.dat");
                            result = ASFFunctions_Win.ASFOnlineActivation(appId, x64SdkKey);
                        }
                    }
                    IsPro = false;
                }
            }
            else
            {
                var pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ActiveFileInfo)));

                if (!string.IsNullOrEmpty(x86ProActiveKey))
                {
                    result = ASFFunctions_Pro_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                    IsPro = true;
                }
                else
                {
                    result = ASFFunctions_Win.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
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
                    if (result != 0)
                    {
                        if (System.IO.File.Exists("ArcFacePro32.dat"))
                        {
                            System.IO.File.Delete("ArcFacePro32.dat");
                            result = ASFFunctions_Pro_Win.ASFOnlineActivation(appId, x86SdkKey, x86ProActiveKey);
                        }
                    }
                    IsPro = true;
                }
                if (result != 0)
                {
                    result = ASFFunctions_Win.ASFOnlineActivation(appId, x86SdkKey);
                    if (result != 0)
                    {
                        if (System.IO.File.Exists("ArcFace32.dat"))
                        {
                            System.IO.File.Delete("ArcFace32.dat");
                            result = ASFFunctions_Win.ASFOnlineActivation(appId, x64SdkKey);
                        }
                    }
                    IsPro = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="isImgMode"></param>
        /// <param name="faceMaxNum"></param>
        /// <param name="isAngleZeroOnly"></param>
        /// <param name="needFaceInfo">需要人脸信息(性别、年龄、角度)</param>
        /// <param name="needRgbLive">需要rgb活体</param>
        /// <param name="needIrLive">需要红外活体</param>
        /// <param name="needFaceFeature"> 需要提取人脸特征值</param>
        /// <param name="needImageQuality"> 是否需要图像质量检测（只对虹软商用授权有效）</param>
        public static int InitEngine(ref IntPtr pEngine, bool isImgMode = false, int faceMaxNum = 5, bool isAngleZeroOnly = true,
            bool needFaceInfo = false, bool needRgbLive = false, bool needIrLive = false, bool needFaceFeature = true,
            bool needImageQuality = false)
        {
            //初始化引擎
            var detectMode = isImgMode ? ASF_DetectMode.ASF_DETECT_MODE_IMAGE : ASF_DetectMode.ASF_DETECT_MODE_VIDEO;
            //检测脸部的角度优先值
            var detectFaceOrientPriority = isAngleZeroOnly ? ASF_OrientPriority.ASF_OP_0_ONLY : ASF_OrientPriority.ASF_OP_ALL_OUT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            var detectFaceScaleVal = 16;
            //引擎初始化时需要初始化的检测功能组合
            var combinedMask = FaceEngineMask.ASF_FACE_DETECT;
            if (needFaceFeature)
            {
                combinedMask |= FaceEngineMask.ASF_FACERECOGNITION;
            }
            if (needFaceInfo)
            {
                // 年龄+性别 3M内存
                combinedMask = combinedMask | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER;
            }
            if (needRgbLive)
            {
                combinedMask |= FaceEngineMask.ASF_LIVENESS;
            }
            if (needIrLive)
            {
                combinedMask |= FaceEngineMask.ASF_IR_LIVENESS;
            }
            if (IsPro
                && needImageQuality)
            {
                combinedMask |= FaceEngineMask.ASF_IMAGEQUALITY;
            }
            int result;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e

            if (IsPro)
            {
                result = ASFFunctions_Pro_Win.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, detectFaceScaleVal, faceMaxNum, (int)combinedMask, ref pEngine);
            }
            else
            {
                result = ASFFunctions_Win.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, detectFaceScaleVal, faceMaxNum, (int)combinedMask, ref pEngine);
            }
            return result;
        }

        /// <summary>
        /// </summary>
        public static int UninitEngine(ref IntPtr pEngine)
        {
            int result;
            if (IsPro)
            {
                result = ASFFunctions_Pro_Win.ASFUninitEngine(pEngine);
            }
            else
            {
                result = ASFFunctions_Win.ASFUninitEngine(pEngine);
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
            var result = -1f;
            var compareModel = isIdcardCompare ? ASF_CompareModel.ASF_ID_PHOTO : ASF_CompareModel.ASF_LIFE_PHOTO;

            int retCode;

            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
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