#if !(NETFRAMEWORK)
using System;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public partial class ASFFunctions
    {
        /// <summary>
        /// 年龄检测
        /// </summary>
        private static ASF_AgeInfo AgeEstimation(IntPtr pEngine)
        {
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_AgeInfo)));
            int retCode;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetAge(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetAge(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_AgeInfo);
            if (retCode == 0)
            {
                result = (ASF_AgeInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_AgeInfo));
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }

        /// <summary>
        /// 性别检测
        /// </summary>
        private static ASF_GenderInfo GenderEstimation(IntPtr pEngine)
        {
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_GenderInfo)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetGender(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetGender(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_GenderInfo);
            if (retCode == 0)
            {
                result = (ASF_GenderInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_GenderInfo));
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
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_Face3DAngle)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetFace3DAngle(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetFace3DAngle(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_Face3DAngle);
            if (retCode == 0)
            {
                result = (ASF_Face3DAngle)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_Face3DAngle));
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
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LivenessInfo)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetLivenessScore(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_LivenessInfo);
            if (retCode == 0)
            {
                result = (ASF_LivenessInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_LivenessInfo));
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
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MaskInfo)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetMask(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetMask(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_MaskInfo);
            if (retCode == 0)
            {
                result = (ASF_MaskInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_MaskInfo));
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
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LandMarkInfo)));
            int retCode;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             && (RuntimeInformation.ProcessArchitecture == Architecture.X64
                || RuntimeInformation.ProcessArchitecture == Architecture.X86))
            {
                retCode = ASFFunctions_Pro_Win.ASFGetFaceLandMark(pEngine, pInfo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                retCode = ASFFunctions_Pro_Linux.ASFGetFaceLandMark(pEngine, pInfo);
            }
            else
            {
                MemoryUtil.Free(ref pInfo);
                throw new NotSupportedException("Only supported Windows x86 x64 and Linux x64");
            }
            var result = default(ASF_LandMarkInfo);
            if (retCode == 0)
            {
                result =(ASF_LandMarkInfo) MemoryUtil.PtrToStructure(pInfo, typeof(ASF_LandMarkInfo));
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }
    }
}
#endif