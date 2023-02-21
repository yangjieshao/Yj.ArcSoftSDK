using System;
#if NET5_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public static partial class ASFFunctions
    {
        /// <summary>
        /// 人脸图像质量检测
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="imageInfo">LPASF_ImageData 图像数据</param>
        /// <param name="faceInfo">人脸信息</param>
        /// <param name="isMask">是否带口罩 默认不带</param>
        /// <returns>调用结果</returns>
        private static float ASFImageQualityDetectEx(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, IntPtr imageInfo, bool isMask)
        {
            float result = -1;
            try
            {
                IntPtr pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
                MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);
                int retCode = -1;

                if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
                {
                    retCode = ASFFunctions_Pro_x64.ASFImageQualityDetectEx(pEngine, imageInfo, pSingleFaceInfo, isMask ? 1 : 0, ref result, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
                }
                else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
                {
                    retCode = ASFFunctions_Pro_x86.ASFImageQualityDetectEx(pEngine, imageInfo, pSingleFaceInfo, isMask ? 1 : 0, ref result, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
                }
#if NET5_0_OR_GREATER
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && RuntimeInformation.ProcessArchitecture == Architecture.X64)
                {
                    retCode = ASFFunctions_Pro_Sox64.ASFImageQualityDetectEx(pEngine, imageInfo, pSingleFaceInfo, isMask ? 1 : 0, ref result, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
                }
                else
                {
                    throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
                }
#endif
                MemoryUtil.Free(ref pSingleFaceInfo);
                if(retCode!=0)
                {
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"提取质量 出错 retCode:{ex.Message}");
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="imageInfo">ASVL_OFFSCREEN</param>
        /// <returns></returns>
        private static ASF_MultiFaceInfo DetectFaceEx(IntPtr pEngine, IntPtr imageInfo)
        {
            ASF_MultiFaceInfo result = new ASF_MultiFaceInfo();
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());

            int retCode = -1;

            if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
            }
            else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
            }
#if NET5_0_OR_GREATER
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_MultiFaceInfo>(pMultiFaceInfo);
            }
            MemoryUtil.Free(ref pMultiFaceInfo);
            return result;
        }

        /// <summary>
        /// 获取单人人脸特征
        /// </summary>
        /// <returns></returns>
        private static byte[] GetSinglePersonFeatureEx(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, IntPtr imageInfo, bool isRegister = true, bool hadMask = true)
        {
            byte[] feature = null;
            int retCode = -1;

            IntPtr pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_SingleFaceInfo>());
            MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);

            IntPtr pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());

            if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, (int)(isRegister ? ASF_RegisterOrNot.ASF_REGISTER : ASF_RegisterOrNot.ASF_RECOGNITION), hadMask ? 1 : 0, pFaceFeature);
            }
            else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, (int)(isRegister ? ASF_RegisterOrNot.ASF_REGISTER : ASF_RegisterOrNot.ASF_RECOGNITION), hadMask ? 1 : 0, pFaceFeature);
            }
#if NET5_0_OR_GREATER
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, (int)(isRegister ? ASF_RegisterOrNot.ASF_REGISTER : ASF_RegisterOrNot.ASF_RECOGNITION), hadMask ? 1 : 0, pFaceFeature);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            if (retCode == 0)
            {
                //人脸特征feature过滤
                ASF_FaceFeature faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(pFaceFeature);
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
        /// <param name="pEngine">引擎Handle</param>
        /// <param name="imageInfo"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_LivenessInfo LivenessInfoEx_IR(IntPtr pEngine, IntPtr imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            ASF_LivenessInfo result = default;
            if (multiFaceInfo.FaceNum == 0)
            {
                return result;
            }
            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            int retCode = -1;

            if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            }
            else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            }
#if NET5_0_OR_GREATER
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            if (retCode == 0)
            {
                IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessInfo>());

                if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
                {
                    retCode = ASFFunctions_Pro_x64.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
                else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
                {
                    retCode = ASFFunctions_Pro_x86.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
#if NET5_0_OR_GREATER
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && RuntimeInformation.ProcessArchitecture == Architecture.X64)
                {
                    retCode = ASFFunctions_Pro_Sox64.ASFGetLivenessScore_IR(pEngine, pInfo);
                }
                else
                {
                    throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
                }
#endif
                if (retCode == 0)
                {
                    result = MemoryUtil.PtrToStructure<ASF_LivenessInfo>(pInfo);
                }
                MemoryUtil.Free(ref pInfo);
            }
            MemoryUtil.Free(ref pMultiFaceInfo);
            return result;
        }

        /// <summary>
        ///
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

            IntPtr pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);
            int retCode = -1;

            if (
#if NET5_0_OR_GREATER
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
            }
            else
#if NET5_0_OR_GREATER
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
            }
#if NET5_0_OR_GREATER
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
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
    }
}