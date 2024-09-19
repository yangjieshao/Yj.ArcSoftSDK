#if NETFRAMEWORK
using System.Drawing;
using System.IO;
#else
using SkiaSharp;
#endif
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK.Models;
using Yj.ArcSoftSDK.Utils;

namespace Yj.ArcSoftSDK
{
    /// <summary>
    /// </summary>
    public static partial class ASFFunctions
    {
        /// <summary>
        /// 是否为永久授权版本
        /// </summary>
        public static bool IsPro { set; get; } = false;

        /// <summary>
        /// 为了在 AOT 裁剪时不被裁剪
        /// </summary>
        public static void Ready4AOT()
        {
            Marshal.SizeOf(default(ASF_ActiveFileInfo));
            Marshal.SizeOf(default(ASF_AgeInfo));
            Marshal.SizeOf(default(ASF_Face3DAngle));
            Marshal.SizeOf(default(ASF_FaceFeature));
            Marshal.SizeOf(default(ASF_GenderInfo));
            Marshal.SizeOf(default(ASF_ImageQualityInfo));
            Marshal.SizeOf(default(ASF_LivenessInfo));
            Marshal.SizeOf(default(ASF_MultiFaceInfo));
            Marshal.SizeOf(default(ASF_SingleFaceInfo));
            Marshal.SizeOf(default(ASF_VERSION));
            Marshal.SizeOf(default(ASVL_OFFSCREEN));
            Marshal.SizeOf(default(LPASF_ImageData));
            Marshal.SizeOf(default(MRECT));
            Marshal.SizeOf(default(Yj.ArcSoftSDK.Models.PointF));
            Marshal.SizeOf(default(Yj.ArcSoftSDK.Models.Rectangle));
        }

        /// <summary>
        /// 获取人脸 信息
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageBuffer"></param>
        /// <param name="faceMinWith"></param>
        /// <param name="needCheckImage">需要校准图片</param>
        /// <param name="needFaceInfo">需要角度、性别、年龄信息</param>
        /// <param name="needRgbLive">需要RGB活体</param>
        /// <param name="needIrLive">需要Ir活体</param>
        /// <param name="needFeatures">需要特征值</param>
        public static List<FaceInfo> DetectFaces(IntPtr pEngine, byte[] imageBuffer, int faceMinWith = 0,
            bool needCheckImage = true, bool needFaceInfo = false, bool needRgbLive = false,
            bool needIrLive = false, bool needFeatures = false)
        {
            List<FaceInfo> result = null;
#if NETFRAMEWORK
            Image needImage = null;
            using (MemoryStream ms = new MemoryStream(imageBuffer))
            {
                needImage = new Bitmap(ms);
            }
#else
            var needImage = SKBitmap.Decode(imageBuffer);
#endif
            if (needCheckImage)
            {
                needImage = ImageUtil.CheckImage(needImage);
            }
            if (needImage != null)
            {
                var imageInfo = ImageUtil.ReadBMP(needImage);
                if (imageInfo != null)
                {
                    result = DetectFaces(pEngine, imageInfo, faceMinWith, needFaceInfo, needRgbLive, needIrLive, needFeatures);
                    MemoryUtil.Free(imageInfo.ImgData);
                }
                if (needCheckImage)
                {
                    needImage.Dispose();
                }
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
        public static List<FaceInfo> DetectFaces(IntPtr pEngine, ImageInfo imageInfo, int faceMinWith = 0,
            bool needFaceInfo = false, bool needRgbLive = false, bool needIrLive = false, bool needFeatures = false)
        {
            var multiFaceInfo = DetectFace(pEngine, imageInfo);
            var result = new List<FaceInfo>(multiFaceInfo.FaceNum);
            if (multiFaceInfo.FaceNum > 0)
            {
                // 是否有获取人脸信息
                var hadFaceInfo = false;
                var ageInfo = default(ASF_AgeInfo);
                var genderInfo = default(ASF_GenderInfo);
                var face3DAngleInfo = default(ASF_Face3DAngle);
                var rgbLiveInfo = default(ASF_LivenessInfo);
                var irLiveInfo = default(ASF_LivenessInfo);
                // 是否有获取RGB活体
                var hadRgbLive = false;
                // 是否有获取IR活体
                var hadRIrLive = false;

                var pMultiFaceInfo = IntPtr.Zero;
                var engineMask = SetEngineMask(needFaceInfo, needRgbLive, multiFaceInfo);

                if (engineMask != FaceEngineMask.ASF_NONE)
                {
                    pMultiFaceInfo = FaceInfoProcess(pEngine, imageInfo, multiFaceInfo, engineMask);
                }
                var orienArry = ReadyFaceinStruct(pEngine, needFaceInfo, needRgbLive, multiFaceInfo, ref hadFaceInfo,
                    ref ageInfo, ref genderInfo, ref face3DAngleInfo, ref rgbLiveInfo, ref hadRgbLive, pMultiFaceInfo);

                MemoryUtil.Free(ref pMultiFaceInfo);
                if (needIrLive
                    && multiFaceInfo.FaceNum == 1)
                {
                    hadRIrLive = true;
                    irLiveInfo = LivenessInfo_IR(pEngine, imageInfo, multiFaceInfo);
                }

                for (int i = 0; i < multiFaceInfo.FaceNum; i++)
                {
                    var faceInfo = CreateFaceInfo(multiFaceInfo, orienArry, i);

                    if (faceInfo.Rectangle.Top >= 0
                        && faceInfo.Rectangle.Left >= 0
                        && faceInfo.Rectangle.Height >= faceMinWith
                        && faceInfo.Rectangle.Width >= faceMinWith)
                    {
                        result.Add(faceInfo);

                        if (needFeatures)
                        {
                            // 特征值
                            var feature = GetSinglePersonFeature(pEngine, faceInfo.ASF_FaceInfo, imageInfo);
                            faceInfo.Feature = feature;
                        }
                        SetFaceInfo(hadFaceInfo, ageInfo, genderInfo, face3DAngleInfo, rgbLiveInfo, irLiveInfo, hadRgbLive,
                            hadRIrLive, i, faceInfo);
                    }
                }
            }
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
        public static List<FaceInfo> DetectFacesEx(IntPtr pEngine, byte[] imageBuffer, int faceMinWith = 0,
            bool needCheckImage = true, bool needFaceInfo = false, bool needRgbLive = false,
            bool needIrLive = false, bool needFeatures = false, bool needImageQuality = false)
        {
#if NETFRAMEWORK
            Image needImage = null;
            using (MemoryStream ms = new MemoryStream(imageBuffer))
            {
                needImage = new Bitmap(ms);
            }
#else
            var needImage = SKBitmap.Decode(imageBuffer);
#endif
            if (needCheckImage)
            {
                needImage = ImageUtil.CheckImage(needImage);
            }
            var imageInfo = ImageUtil.GetImageData(needImage);
            var result = DetectFacesEx(pEngine, imageInfo, faceMinWith, needFaceInfo, needRgbLive, needIrLive, needFeatures, needImageQuality);
            MemoryUtil.Free(ref imageInfo.ppu8Plane[0]);
            needImage.Dispose();
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
        internal static List<FaceInfo> DetectFacesEx(IntPtr pEngine, ASVL_OFFSCREEN imageInfo, int faceMinWith = 0,
            bool needFaceInfo = false, bool needRgbLive = false, bool needIrLive = false, bool needFeatures = false,
            bool needImageQuality = false)
        {
            var imageInfoPtr = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASVL_OFFSCREEN)));
            MemoryUtil.StructureToPtr(imageInfo, imageInfoPtr);

            var multiFaceInfo = DetectFaceEx(pEngine, imageInfoPtr);
            var result = new List<FaceInfo>(multiFaceInfo.FaceNum);
            if (multiFaceInfo.FaceNum > 0)
            {
                // 是否有获取人脸信息
                var hadFaceInfo = false;
                var ageInfo = default(ASF_AgeInfo);
                var genderInfo = default(ASF_GenderInfo);
                var face3DAngleInfo = default(ASF_Face3DAngle);
                var rgbLiveInfo = default(ASF_LivenessInfo);
                var irLiveInfo = default(ASF_LivenessInfo);
                // 是否有获取RGB活体
                var hadRgbLive = false;
                // 是否有获取IR活体
                var hadRIrLive = false;

                var pMultiFaceInfo = IntPtr.Zero;
                var engineMask = SetEngineMask(needFaceInfo, needRgbLive, multiFaceInfo);

                if (engineMask != FaceEngineMask.ASF_NONE)
                {
                    pMultiFaceInfo = FaceInfoProcessEx(pEngine, imageInfoPtr, multiFaceInfo, engineMask);
                }
                var orienArry = ReadyFaceinStruct(pEngine, needFaceInfo, needRgbLive, multiFaceInfo, ref hadFaceInfo,
                    ref ageInfo, ref genderInfo, ref face3DAngleInfo, ref rgbLiveInfo, ref hadRgbLive, pMultiFaceInfo);

                MemoryUtil.Free(ref pMultiFaceInfo);
                if (needIrLive
                   /* && multiFaceInfo.FaceNum == 1*/)
                {
                    hadRIrLive = true;
                    irLiveInfo = LivenessInfoEx_IR(pEngine, imageInfoPtr, multiFaceInfo);
                }

                // 人脸质量
                float[] qualityArry = null;
                if (IsPro
                    && needImageQuality)
                {
                    var imageQualityInfo = ASFImageQualityDetectEx(pEngine, imageInfoPtr, pMultiFaceInfo);
                    if (imageQualityInfo.Num > 0)
                    {
                        qualityArry = new float[imageQualityInfo.Num];
                        MemoryUtil.Copy(imageQualityInfo.FaceQualityValue, qualityArry, 0, imageQualityInfo.Num);
                    }
                }
                for (int i = 0; i < multiFaceInfo.FaceNum; i++)
                {
                    var faceInfo = CreateFaceInfo(multiFaceInfo, orienArry, i);

                    if (faceInfo.Rectangle.Top >= 0
                        && faceInfo.Rectangle.Left >= 0
                        && faceInfo.Rectangle.Height >= faceMinWith
                        && faceInfo.Rectangle.Width >= faceMinWith)
                    {
                        result.Add(faceInfo);

                        if (qualityArry != null
                            && qualityArry.Length >= i)
                        {
                            faceInfo.ImageQuality = qualityArry[i];
                        }

                        if (needFeatures)
                        {
                            // 特征值
                            var feature = GetSinglePersonFeatureEx(pEngine, faceInfo.ASF_FaceInfo, imageInfoPtr);
                            faceInfo.Feature = feature;
                        }
                        SetFaceInfo(hadFaceInfo, ageInfo, genderInfo, face3DAngleInfo, rgbLiveInfo, irLiveInfo, hadRgbLive,
                            hadRIrLive, i, faceInfo);
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
        public static float FaceFeatureCompare(IntPtr pEngine, byte[] feature1, byte[] feature2, bool isIdcardCompare = false)
        {
            var result = -1f;
            if (feature1 != null
                && feature1.Length > 0
                && feature2 != null
                && feature2.Length > 0)
            {
                var pFaceFeature1 = Feature2IntPtr(feature1);
                var pFaceFeature2 = Feature2IntPtr(feature2);
                result = FaceFeatureCompare(pEngine, pFaceFeature1, pFaceFeature2, isIdcardCompare);
                FreeFeatureIntPtr(pFaceFeature1);
                FreeFeatureIntPtr(pFaceFeature2);
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
            var faceFeature = (ASF_FaceFeature)MemoryUtil.PtrToStructure(featureIntPtr, typeof(ASF_FaceFeature));
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
            var pFaceFeature = IntPtr.Zero;
            if (feature != null
                && feature.Length > 0)
            {
                var faceFeature = new ASF_FaceFeature
                {
                    Feature = MemoryUtil.Malloc(feature.Length)
                };
                MemoryUtil.Copy(feature, 0, faceFeature.Feature, feature.Length);
                faceFeature.FeatureSize = feature.Length;
                pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_FaceFeature)));
                MemoryUtil.StructureToPtr(faceFeature, pFaceFeature);
            }
            return pFaceFeature;
        }
    }
}