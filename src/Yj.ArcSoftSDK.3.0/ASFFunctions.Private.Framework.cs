#if NETFRAMEWORK
using System;
using Yj.ArcSoftSDK.Models;
using Yj.ArcSoftSDK.Utils;

namespace Yj.ArcSoftSDK
{
    public partial class ASFFunctions
    {
        /// <summary>
        /// </summary>
        private static ASF_MultiFaceInfo DetectFace(IntPtr pEngine, ImageInfo imageInfo)
        {
            if (imageInfo == null)
            {
                return default(ASF_MultiFaceInfo);
            }
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));

            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFDetectFaces(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, 0x1);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFDetectFaces(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, 0x1);
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
        private static byte[] GetSinglePersonFeature(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, ImageInfo imageInfo)
        {
            byte[] feature = null;
            var pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_SingleFaceInfo)));
            MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);

            var pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_FaceFeature)));

            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFFaceFeatureExtract(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pSingleFaceInfo, pFaceFeature);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFFaceFeatureExtract(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pSingleFaceInfo, pFaceFeature);
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
        /// 年龄检测
        /// </summary>
        private static ASF_AgeInfo AgeEstimation(IntPtr pEngine)
        {
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_AgeInfo)));
            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFGetAge(pEngine, pInfo);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFGetAge(pEngine, pInfo);
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
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFGetGender(pEngine, pInfo);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFGetGender(pEngine, pInfo);
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
        private static ASF_Face3DAngle Face3DAngleDetection(IntPtr pEngine)
        {
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_Face3DAngle)));
            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFGetFace3DAngle(pEngine, pInfo);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFGetFace3DAngle(pEngine, pInfo);
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
        private static ASF_LivenessInfo LivenessInfo_RGB(IntPtr pEngine)
        {
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LivenessInfo)));
            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore(pEngine, pInfo);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFGetLivenessScore(pEngine, pInfo);
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
        /// RGB活体检测
        /// </summary>
        private static ASF_LivenessInfo LivenessInfo_IR(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return default(ASF_LivenessInfo);
            }
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFProcess_IR(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFProcess_IR(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            }
            if (retCode != 0)
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                return default(ASF_LivenessInfo);
            }

            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LivenessInfo)));

            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore_IR(pEngine, pInfo);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFGetLivenessScore_IR(pEngine, pInfo);
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
        private static IntPtr FaceInfoProcess(IntPtr pEngine, ImageInfo imageInfo, ASF_MultiFaceInfo multiFaceInfo, FaceEngineMask faceEngineMask)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return IntPtr.Zero;
            }

            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);
            int retCode;
            if (IsPro)
            {
                retCode = ASFFunctions_Pro_Win.ASFProcess(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, (int)faceEngineMask);
            }
            else
            {
                retCode = ASFFunctions_Win.ASFProcess(pEngine, imageInfo.Width, imageInfo.Height, (int)imageInfo.Format, imageInfo.ImgData, pMultiFaceInfo, (int)faceEngineMask);
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
    }
}
#endif