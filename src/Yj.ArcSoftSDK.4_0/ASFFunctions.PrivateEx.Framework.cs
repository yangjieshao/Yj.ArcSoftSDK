#if NETFRAMEWORK
using System;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public partial class ASFFunctions
    {
        /// <summary>
        /// </summary>
        private static ASF_MultiFaceInfo DetectFaceEx(IntPtr pEngine, IntPtr imageInfo)
        {
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));

            var retCode = ASFFunctions_Pro_Win.ASFDetectFacesEx(pEngine, imageInfo, pMultiFaceInfo, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
            var result = default(ASF_MultiFaceInfo);
            if (retCode == 0)
            {
                result = (ASF_MultiFaceInfo) MemoryUtil.PtrToStructure(pMultiFaceInfo, typeof(ASF_MultiFaceInfo));
            }
            MemoryUtil.Free(ref pMultiFaceInfo);
            return result;
        }

        /// <summary>
        /// 获取单人人脸特征
        /// </summary>
        private static byte[] GetSinglePersonFeatureEx(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, IntPtr imageInfo, bool isRegister = true, bool hadMask = true)
        {
            byte[] feature = null;

            var pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(Models.ASF_SingleFaceInfo)));
            MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);

            var pFaceFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_FaceFeature)));

            var retCode = ASFFunctions_Pro_Win.ASFFaceFeatureExtractEx(pEngine, imageInfo, pSingleFaceInfo, (int)(isRegister ? ASF_RegisterOrNot.ASF_REGISTER : ASF_RegisterOrNot.ASF_RECOGNITION), hadMask ? 1 : 0, pFaceFeature);
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
        /// 人脸图像质量检测
        /// </summary>
        /// <param name="pEngine">引擎handle</param>
        /// <param name="imageInfo">LPASF_ImageData 图像数据</param>
        /// <param name="faceInfo">人脸信息</param>
        /// <param name="isMask">是否带口罩 默认不带</param>
        /// <returns>调用结果</returns>
        private static float ASFImageQualityDetectEx(IntPtr pEngine, ASF_SingleFaceInfo faceInfo, IntPtr imageInfo, bool isMask)
        {
            var result = -1f;
            try
            {
                var pSingleFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(Models.ASF_SingleFaceInfo)));
                MemoryUtil.StructureToPtr(faceInfo, pSingleFaceInfo);
                var retCode = ASFFunctions_Pro_Win.ASFImageQualityDetectEx(pEngine, imageInfo, pSingleFaceInfo, isMask ? 1 : 0, ref result, (int)ASF_DetectModel.ASF_DETECT_MODEL_RGB);
                MemoryUtil.Free(ref pSingleFaceInfo);
                if (retCode != 0)
                {
                    result = -1;
                }
            }
            catch
            {
                // unuse
            }
            return result;
        }

        /// <summary>
        /// RGB活体检测
        /// </summary>
        private static ASF_LivenessInfo LivenessInfoEx_IR(IntPtr pEngine, IntPtr imageInfo, ASF_MultiFaceInfo multiFaceInfo)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return default(ASF_LivenessInfo);
            }
            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);

            var retCode = ASFFunctions_Pro_Win.ASFProcessEx_IR(pEngine, imageInfo, pMultiFaceInfo, (int)FaceEngineMask.ASF_IR_LIVENESS);
            if (retCode != 0)
            {
                MemoryUtil.Free(ref pMultiFaceInfo);
                return default(ASF_LivenessInfo);
            }
            var pInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_LivenessInfo)));

            retCode = ASFFunctions_Pro_Win.ASFGetLivenessScore_IR(pEngine, pInfo);
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
        private static IntPtr FaceInfoProcessEx(IntPtr pEngine, IntPtr imageInfo, ASF_MultiFaceInfo multiFaceInfo, FaceEngineMask faceEngineMask)
        {
            if (multiFaceInfo.FaceNum == 0)
            {
                return IntPtr.Zero;
            }

            var pMultiFaceInfo = MemoryUtil.Malloc(MemoryUtil.SizeOf(typeof(ASF_MultiFaceInfo)));
            MemoryUtil.StructureToPtr(multiFaceInfo, pMultiFaceInfo);
            var retCode = ASFFunctions_Pro_Win.ASFProcessEx(pEngine, imageInfo, pMultiFaceInfo, (int)faceEngineMask);
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