#if NETFRAMEWORK
using System.Drawing;
using System.IO;
#else
using SkiaSharp;
using System.Runtime.InteropServices;
#endif
using System;
using System.Collections.Generic;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    /// <summary>
    ///
    /// </summary>
    public static partial class ASFFunctions
    {
        /// <returns></returns>
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
        /// <returns></returns>
        public static int Activation(string appId, string x86SdkKey, string x64SdkKey, string sox64Key
            , string x86ProActiveKey = null, string x64ProActiveKey = null, string sox64ProActiveKey = null)
        {
            int result = -1;

            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                ASF_ActiveFileInfo aSF_ActiveFileInfo = default;
                IntPtr pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_ActiveFileInfo>());

                if (!string.IsNullOrEmpty(x64ProActiveKey))
                {
                    result = ASFFunctions_Pro_x64.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                if (result == 0)
                {
                    aSF_ActiveFileInfo = MemoryUtil.PtrToStructure<ASF_ActiveFileInfo>(pASF_ActiveFileInfo);
                }
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
                    result = ASFFunctions_Pro_x64.ASFOnlineActivation(appId, x64SdkKey, x64ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro64.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro64.dat");
                        result = ASFFunctions_Pro_x64.ASFOnlineActivation(appId, x64SdkKey, x64ProActiveKey);
                    }
                }
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                ASF_ActiveFileInfo aSF_ActiveFileInfo = default;
                IntPtr pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_ActiveFileInfo>());

                if (!string.IsNullOrEmpty(x86ProActiveKey))
                {
                    result = ASFFunctions_Pro_x86.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                if (result == 0)
                {
                    aSF_ActiveFileInfo = MemoryUtil.PtrToStructure<ASF_ActiveFileInfo>(pASF_ActiveFileInfo);
                }
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
                    result = ASFFunctions_Pro_x86.ASFOnlineActivation(appId, x86SdkKey, x86ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro32.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro32.dat");
                        result = ASFFunctions_Pro_x86.ASFOnlineActivation(appId, x86SdkKey, x86ProActiveKey);
                    }
                }
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                ASF_ActiveFileInfo aSF_ActiveFileInfo = default;
                IntPtr pASF_ActiveFileInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_ActiveFileInfo>());

                if (!string.IsNullOrEmpty(sox64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Sox64.ASFGetActiveFileInfo(pASF_ActiveFileInfo);
                }

                if (result == 0)
                {
                    aSF_ActiveFileInfo = MemoryUtil.PtrToStructure<ASF_ActiveFileInfo>(pASF_ActiveFileInfo);
                }
                if (result == 0
                    && long.TryParse(aSF_ActiveFileInfo.EndTime, out long endTime)
                    && long.TryParse(aSF_ActiveFileInfo.StartTime, out long startTime)
                    && (DateTime.Now.ToTimestamp() / 1000) < endTime
                    && (DateTime.Now.ToTimestamp() / 1000) >= startTime
                    && (aSF_ActiveFileInfo.Platform == "linux_x64" || aSF_ActiveFileInfo.SdkType == "linux_x64"))
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(sox64ProActiveKey))
                {
                    result = ASFFunctions_Pro_Sox64.ASFOnlineActivation(appId, sox64Key, sox64ProActiveKey);
                    if (result != 0
                        && System.IO.File.Exists("ArcFacePro64.dat"))
                    {
                        System.IO.File.Delete("ArcFacePro64.dat");
                        result = ASFFunctions_Pro_Sox64.ASFOnlineActivation(appId, sox64Key, sox64ProActiveKey);
                    }
                }
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
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
        /// <returns></returns>
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
            int result = -1;
            //初始化引擎
            ASF_DetectMode detectMode = isImgMode ? ASF_DetectMode.ASF_DETECT_MODE_IMAGE : ASF_DetectMode.ASF_DETECT_MODE_VIDEO;
            //检测脸部的角度优先值
            ASF_OrientPriority detectFaceOrientPriority = isAngleZeroOnly ? ASF_OrientPriority.ASF_OP_0_ONLY : ASF_OrientPriority.ASF_OP_ALL_OUT;
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
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e

            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                result = ASFFunctions_Pro_x64.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, faceMaxNum, (int)combinedMask, ref pEngine);
                if (result == 0 && needFaceInfo)
                {
                    result = ASFFunctions_Pro_x64.ASFSetFaceShelterParam(pEngine, shelterThreshhold);
                }
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                result = ASFFunctions_Pro_x86.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, faceMaxNum, (int)combinedMask, ref pEngine);
                if (result == 0 && needFaceInfo)
                {
                    result = ASFFunctions_Pro_x86.ASFSetFaceShelterParam(pEngine, shelterThreshhold);
                }
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                result = ASFFunctions_Pro_Sox64.ASFInitEngine((uint)detectMode, (int)detectFaceOrientPriority, faceMaxNum, (int)combinedMask, ref pEngine);
                if (result == 0 && needFaceInfo)
                {
                    result = ASFFunctions_Pro_Sox64.ASFSetFaceShelterParam(pEngine, shelterThreshhold);
                }
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pEngine"></param>
        /// <returns></returns>
        public static int UninitEngine(ref IntPtr pEngine)
        {
            int result = -1;
            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                result = ASFFunctions_Pro_x64.ASFUninitEngine(pEngine);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                result = ASFFunctions_Pro_x86.ASFUninitEngine(pEngine);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                result = ASFFunctions_Pro_Sox64.ASFUninitEngine(pEngine);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            pEngine = IntPtr.Zero;
            return result;
        }

        /// <summary>
        /// 获取人脸 信息
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageBuffer"></param>
        /// <param name="faceMinWith"></param>
        /// <param name="needCheckImage"></param>
        /// <param name="needFaceInfo">需要角度、性别、年龄信息</param>
        /// <param name="needRgbLive">需要RGB活体</param>
        /// <param name="needIrLive">需要Ir活体</param>
        /// <param name="needFeatures">需要特征值</param>
        /// <param name="needImageQuality"> 是否需要图像质量检测（只对虹软商用授权有效）</param>
        /// <param name="isRegister">算法登记照(只对4.0算法有效)</param>
        /// <returns></returns>
        public static List<FaceInfo> DetectFacesEx(IntPtr pEngine, byte[] imageBuffer, int faceMinWith = 0,
            bool needCheckImage = true, bool needFaceInfo = false, bool needRgbLive = false,
            bool needIrLive = false, bool needFeatures = false, bool needImageQuality = false, bool isRegister = true)
        {
#if NETFRAMEWORK
            Image needImage = null;
            using (MemoryStream ms = new MemoryStream(imageBuffer))
            {
                needImage = new Bitmap(ms);
            }
#else
            SKBitmap needImage = SKBitmap.Decode(imageBuffer);
#endif
            if (needCheckImage)
            {
                needImage = ImageUtil.CheckImage(needImage);
            }
            var imageInfo = ImageUtil.GetImageData(needImage);
            List<FaceInfo> result = DetectFacesEx(pEngine, imageInfo, faceMinWith, needFaceInfo, needRgbLive, needIrLive
                , needFeatures, needImageQuality, isRegister);
            if(imageInfo.ppu8Plane[0]!=IntPtr.Zero)
            {
                MemoryUtil.Free(imageInfo.ppu8Plane[0]);
                imageInfo.ppu8Plane[0] = IntPtr.Zero;
            }
            if (needCheckImage)
            {
                needImage.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 获取人脸 信息
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="faceMinWith"></param>
        /// <param name="needFaceInfo">需要角度、性别、年龄信息</param>
        /// <param name="needRgbLive">需要RGB活体</param>
        /// <param name="needIrLive">需要Ir活体</param>
        /// <param name="needFeatures">需要特征值</param>
        /// <param name="needImageQuality"> 是否需要图像质量检测（只对虹软商用授权有效）</param>
        /// <param name="isRegister">算法登记照(只对4.0算法有效)</param>
        /// <returns></returns>
        public static List<FaceInfo> DetectFacesEx(IntPtr pEngine, ASVL_OFFSCREEN imageInfo, int faceMinWith = 0,
            bool needFaceInfo = false, bool needRgbLive = false, bool needIrLive = false, bool needFeatures = false,
            bool needImageQuality = false, bool isRegister = true)
        {
            IntPtr imageInfoPtr = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASVL_OFFSCREEN>());
            MemoryUtil.StructureToPtr(imageInfo, imageInfoPtr);

            ASF_MultiFaceInfo multiFaceInfo = DetectFaceEx(pEngine, imageInfoPtr);
            var result = new List<FaceInfo>(multiFaceInfo.FaceNum);
            if (multiFaceInfo.FaceNum > 0)
            {
                // 是否有获取人脸信息
                bool hadFaceInfo = false;
                ASF_AgeInfo ageInfo = default;
                ASF_GenderInfo genderInfo = default;
                ASF_Face3DAngle face3DAngleInfo = default;
                ASF_LivenessInfo rgbLiveInfo = default;
                ASF_LivenessInfo irLiveInfo = default;

                ASF_MaskInfo maskInfo = default;
                ASF_LandMarkInfo lanMaskInfo = default;
                // 是否有获取RGB活体
                bool hadRgbLive = false;
                // 是否有获取IR活体
                bool hadIrLive = false;

                IntPtr pMultiFaceInfo = IntPtr.Zero;
                FaceEngineMask engineMask = SetEngineMask(needFaceInfo, needRgbLive, multiFaceInfo);

                if (engineMask != FaceEngineMask.ASF_NONE)
                {
                    pMultiFaceInfo = FaceInfoProcessEx(pEngine, imageInfoPtr, multiFaceInfo, engineMask);
                }
                int[] orienArry = ReadyFaceinStruct(pEngine, needFaceInfo, needRgbLive, multiFaceInfo, ref hadFaceInfo,
                    ref ageInfo, ref genderInfo, ref face3DAngleInfo, ref rgbLiveInfo, ref hadRgbLive, pMultiFaceInfo,
                    ref maskInfo, ref lanMaskInfo);

                if (needIrLive
                    /*&& multiFaceInfo.FaceNum == 1*/)
                {
                    hadIrLive = true;
                    irLiveInfo = LivenessInfoEx_IR(pEngine, imageInfoPtr, multiFaceInfo);
                }

                for (int i = 0; i < multiFaceInfo.FaceNum; i++)
                {
                    FaceInfo faceInfo = CreateFaceInfo(multiFaceInfo, orienArry, i);

                    if (faceInfo.Rectangle.Top >= 0
                        && faceInfo.Rectangle.Left >= 0
                        && faceInfo.Rectangle.Height >= faceMinWith
                        && faceInfo.Rectangle.Width >= faceMinWith)
                    {
                        result.Add(faceInfo);

                        SetFaceInfo(hadFaceInfo, ageInfo, genderInfo, face3DAngleInfo, rgbLiveInfo, irLiveInfo, maskInfo, lanMaskInfo, hadRgbLive,
                            hadIrLive, i, faceInfo);
                        if (needImageQuality)
                        {
                            faceInfo.ImageQuality = ASFImageQualityDetectEx(pEngine, faceInfo.ASF_FaceInfo, imageInfoPtr, faceInfo.Mask == 1);
                        }

                        if (needFeatures)
                        {
                            // 特征值
                            byte[] feature = GetSinglePersonFeatureEx(pEngine, faceInfo.ASF_FaceInfo, imageInfoPtr, isRegister, faceInfo.Mask == 1);
                            faceInfo.Feature = feature;
                        }
                    }
                }
            }

            MemoryUtil.Free(ref imageInfoPtr);
            return result;
        }

        /// <summary>
        /// 获取人脸个数
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageBuffer"></param>
        /// <param name="faceMinWith">人脸最小宽度</param>
        /// <param name="needCheckImage"></param>
        /// <returns></returns>
        public static int GetFaceNum(IntPtr pEngine, byte[] imageBuffer, int faceMinWith = 0, bool needCheckImage = true)
        {
            return DetectFacesEx(pEngine, imageBuffer, faceMinWith, needCheckImage).Count;
        }

        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="feature1"></param>
        /// <param name="feature2"></param>
        /// <param name="isIdcardCompare">是否证件照对比</param>
        /// <returns></returns>
        public static float FaceFeatureCompare(IntPtr pEngine, byte[] feature1, byte[] feature2, bool isIdcardCompare = false)
        {
            float result = -1;
            if (feature1 != null
                && feature1.Length > 0
                && feature2 != null
                && feature2.Length > 0)
            {
                IntPtr pFaceFeature1 = Feature2IntPtr(feature1);
                IntPtr pFaceFeature2 = Feature2IntPtr(feature2);
                result = FaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, isIdcardCompare);
                FreeFeatureIntPtr(pFaceFeature1);
                FreeFeatureIntPtr(pFaceFeature2);
            }
            return result;
        }

        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="pFaceFeature1"></param>
        /// <param name="pFaceFeature2"></param>
        /// <param name="isIdcardCompare">是否证件照对比</param>
        /// <returns></returns>
        public static float FaceFeatureCompare(IntPtr pEngine, IntPtr pFaceFeature1, IntPtr pFaceFeature2, bool isIdcardCompare)
        {
            float result = -1;
            int retCode = -1;
            ASF_CompareModel compareModel = isIdcardCompare ? ASF_CompareModel.ASF_ID_PHOTO : ASF_CompareModel.ASF_LIFE_PHOTO;

            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFFaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, ref result, (int)compareModel);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            if (retCode != 0
                || result > 1)
            {
                // 相似度不可能大于1
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// 释放特征值指针
        /// create by <see cref="Feature2IntPtr(byte[])"/>
        /// </summary>
        /// <param name="featureIntPtr"></param>
        public static void FreeFeatureIntPtr(IntPtr featureIntPtr)
        {
            var faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(featureIntPtr);
            MemoryUtil.Free(ref faceFeature.Feature);
            MemoryUtil.Free(ref featureIntPtr);
        }

        /// <summary>
        /// 获取特征值指针
        /// free by <see cref="FreeFeatureIntPtr(IntPtr)"/>
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static IntPtr Feature2IntPtr(byte[] feature)
        {
            IntPtr pFaceFeature = IntPtr.Zero;
            if (feature != null
                && feature.Length > 0)
            {
                ASF_FaceFeature faceFeature = new ASF_FaceFeature
                {
                    Feature = MemoryUtil.Malloc(feature.Length)
                };
                MemoryUtil.Copy(feature, 0, faceFeature.Feature, feature.Length);
                faceFeature.FeatureSize = feature.Length;
                pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
                MemoryUtil.StructureToPtr(faceFeature, pFaceFeature);
            }
            return pFaceFeature;
        }
    }
}