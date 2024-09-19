#if (NETFRAMEWORK)
using System;
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
            var retCode = ASFFunctions_Pro_Win.ASFGetAge(pEngine, pInfo);
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
            var retCode = ASFFunctions_Pro_Win.ASFGetGender(pEngine, pInfo);
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
            var retCode = ASFFunctions_Pro_Win.ASFGetFace3DAngle(pEngine, pInfo);
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
            var retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore(pEngine, pInfo);
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
            var retCode = ASFFunctions_Pro_Win.ASFGetMask(pEngine, pInfo);
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
            var retCode = ASFFunctions_Pro_Win.ASFGetFaceLandMark(pEngine, pInfo);
            var result = default(ASF_LandMarkInfo);
            if (retCode == 0)
            {
                result = (ASF_LandMarkInfo)MemoryUtil.PtrToStructure(pInfo, typeof(ASF_LandMarkInfo));
            }
            MemoryUtil.Free(ref pInfo);
            return result;
        }
    }
}
#endif