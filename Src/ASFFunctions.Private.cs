using System;
#if !(NETFRAMEWORK)
using System.Runtime.InteropServices;
#endif
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public static partial class ASFFunctions
    {
        /// <summary>
        /// 年龄检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_AgeInfo AgeEstimation(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_AgeInfo>());
            int retCode = -1;

            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetAge(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetAge(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetAge(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_AgeInfo result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_AgeInfo>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// 性别检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_GenderInfo GenderEstimation(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_GenderInfo>());
            int retCode = -1;
            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetGender(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetGender(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetGender(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_GenderInfo result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_GenderInfo>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// 人脸3D角度检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_Face3DAngle Face3DAngleDetection(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_Face3DAngle>());
            int retCode = -1;
            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetFace3DAngle(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetFace3DAngle(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetFace3DAngle(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_Face3DAngle result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_Face3DAngle>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// RGB活体检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_LivenessInfo LivenessInfo_RGB(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LivenessInfo>());
            int retCode = -1;
            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetLivenessScore(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetLivenessScore(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetLivenessScore(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_LivenessInfo result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_LivenessInfo>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// 口罩检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_MaskInfo MaskEstimation(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MaskInfo>());
            int retCode = -1;
            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetMask(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetMask(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetMask(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_MaskInfo result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_MaskInfo>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// 额头检测
        /// </summary>
        /// <param name="pEngine">引擎Handle</param>
        /// <returns>年龄检测结构体</returns>
        private static ASF_LandMarkInfo FaceLandEstimation(IntPtr pEngine)
        {
            IntPtr pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_LandMarkInfo>());
            int retCode = -1;

            if (
#if !(NETFRAMEWORK)
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64
#else
                Environment.Is64BitProcess
#endif
                )
            {
                retCode = ASFFunctions_Pro_x64.ASFGetFaceLandMark(pEngine, pInfo);
            }
            else
#if !(NETFRAMEWORK)
            if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                && RuntimeInformation.ProcessArchitecture == Architecture.X86)
#endif
            {
                retCode = ASFFunctions_Pro_x86.ASFGetFaceLandMark(pEngine, pInfo);
            }
#if !(NETFRAMEWORK)
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Sox64.ASFGetFaceLandMark(pEngine, pInfo);
            }
            else
            {
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
#endif
            ASF_LandMarkInfo result = default;
            if (retCode == 0)
            {
                result = MemoryUtil.PtrToStructure<ASF_LandMarkInfo>(pInfo);
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }
    }
}