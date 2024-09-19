using System;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK.Models;
using Yj.ArcSoftSDK.Utils;

namespace Yj.ArcSoftSDK
{
    public partial class ASFFunctions
    {
#if !NETFRAMEWORK

        /// <summary>
        /// 人脸图像质量检测 仅商用授权码有效
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="imgData">LPASF_ImageData 图像数据</param>
        /// <param name="detectedFaces">人脸检测结果</param>
        /// <returns>调用结果</returns>
        private static ASF_ImageQualityInfo ASFImageQualityDetectEx(IntPtr pEngine, IntPtr imgData, IntPtr detectedFaces)
        {
            if (!IsPro)
            {
                return default;
            }

            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_ImageQualityInfo)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFImageQualityDetectEx(pEngine, imgData, detectedFaces, pInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                  && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFImageQualityDetectEx(pEngine, imgData, detectedFaces, pInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_ImageQualityInfo);
            if (retCode == 0)
            {
                result = (ASF_ImageQualityInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_ImageQualityInfo));
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo">ASVL_OFFSCREEN</param>
        private static ASF_MultiFaceInfo DetectFaceEx(IntPtr pEngine, IntPtr imageInfo)
        {
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));

            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Win.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
                }
                else
                {
                    retCode = ASFFunctions_Win.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                  && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Linux.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
                }
                else
                {
                    retCode = ASFFunctions_Linux.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectMode.ASF_DETECT_MODEL_RGB);
                }
            }
            else
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_MultiFaceInfo);
            if (retCode == 0)
            {
                result = (ASF_MultiFaceInfo)MemoryUtil.PtrToStructure(pMultiFaceInfo, typeof(ASF_MultiFaceInfo));
            }
            MemoryUtil.Free(ref pMultiFaceInfo);
            return result;
        }

        /// <summary>
        /// 获取单人人脸特征
        /// </summary>
        private static byte[] GetSinglePersonFeatureEx(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, IntPtr imageInfo)
        {
            byte[] feature = null;
            var pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_SingleFaceInfo)));
            MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);

            var pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_FaceFeature)));

            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Win.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, pFaceFeature);
                }
                else
                {
                    retCode = ASFFunctions_Win.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, pFaceFeature);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Linux.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, pFaceFeature);
                }
                else
                {
                    retCode = ASFFunctions_Linux.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, pFaceFeature);
                }
            }
            else
            {
                MemoryUtil.Free(ref pSingleFaceInfo);
                MemoryUtil.Free(ref pFaceFeature);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            if (retCode == 0)
            {
                //人脸特征feature过滤
                var faceFeature = (ASF_FaceFeature)MemoryUtil.PtrToStructure(pFaceFeature, typeof(ASF_FaceFeature));
                feature = new byte[faceFeature.FeatureSize];
                MemoryUtil.Copy(faceFeature.Feature, feature, 0, faceFeature.FeatureSize);
            }
            MemoryUtil.Free(ref pSingleFaceInfo);
            MemoryUtil.Free(ref pFaceFeature);
            return feature;
        }


        /// <summary>
        /// RGB活体检测
        /// </summary>
        /// <returns>年龄检测结构体</returns>
        private static ASF_LivenessInfo LivenessInfoEx_IR(IntPtr pEngine, IntPtr imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return default(ASF_LivenessInfo);
            }
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Win.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
                }
                else
                {
                    retCode = ASFFunctions_Win.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Linux.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
                }
                else
                {
                    retCode = ASFFunctions_Linux.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
                }
            }
            else
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            if (retCode != 0)
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                return default(ASF_LivenessInfo);
            }

            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LivenessInfo)));
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
                else
                {
                    retCode = ASFFunctions_Win.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Linux.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
                else
                {
                    retCode = ASFFunctions_Linux.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                MemoryUtil.Free(ref pMultiFaceInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_LivenessInfo);
            if (retCode == 0)
            {
                result = (ASF_LivenessInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_LivenessInfo));
            }
            MemoryUtil.Free(ref pInfo);
            MemoryUtil.Free(ref pMultiFaceInfo);
            return result;
        }


        /// <summary>
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <param name="faceEngineMask"></param>
        /// <returns>pMultiFaceInfo</returns>
        private static IntPtr FaceInfoProcessEx(IntPtr pEngine, IntPtr imageInfo, ASF_MultiFaceInfo multiFaceInfo, FaceEngineMask faceEngineMask)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return IntPtr.Zero;
            }

            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Win.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
                }
                else
                {
                    retCode = ASFFunctions_Win.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                  && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                if (IsPro)
                {
                    retCode = ASFFunctions_Pro_Linux.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
                }
                else
                {
                    retCode = ASFFunctions_Linux.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
                }
            }
            else
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            if (retCode != 0)
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                return IntPtr.Zero;
            }
            else
            {
                return pMultiFaceInfo;
            }
        }
#endif
    }
}