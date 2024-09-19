using System;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public partial class ASFFunctions
    {
        private static void SetFaceInfo(bool hadFaceInfo, ASF_AgeInfo ageInfo, ASF_GenderInfo genderInfo, ASF_Face3DAngle face3DAngleInfo, ASF_LivenessInfo rgbLiveInfo
            , ASF_LivenessInfo irLiveInfo, ASF_MaskInfo maskInfo, ASF_LandMarkInfo lanMaskInfo, bool hadRgbLive, bool hadRIrLive, int i, FaceInfo faceInfo)
        {
            if (hadFaceInfo)
            {
                if (ageInfo.Num > i)
                {
                    faceInfo.Age = MemoryUtil.PtrToInt(ageInfo.AgeArray + 4 * i);
                }
                else
                {
                    faceInfo.Age = -1;
                }
                if (genderInfo.Num > i)
                {
                    faceInfo.Gender = MemoryUtil.PtrToInt(genderInfo.GenderArray + 4 * i);
                }
                else
                {
                    faceInfo.Gender = -1;
                }

                if (maskInfo.Num > i)
                {
                    faceInfo.Mask = MemoryUtil.PtrToInt(maskInfo.MaskArray + 4 * i);
                }
                else
                {
                    faceInfo.Mask = -1;
                }
                if (lanMaskInfo.Num > i)
                {
                    var faceLandmark = (ASF_FaceLandmark)MemoryUtil.PtrToStructure(lanMaskInfo.Point + MemoryUtil.SizeOf(typeof(ASF_FaceLandmark)) * i, typeof(ASF_FaceLandmark));
                    faceInfo.FaceLandPoint = new PointF(faceLandmark.X, faceLandmark.Y);
                }
                //else
                //{
                //    faceInfo.FaceLandPoint = new PointF(-1f, -1f);
                //}
                if (face3DAngleInfo.Num > i)
                {
                    faceInfo.Face3DAngle.Status = 0;
                    //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                    faceInfo.Face3DAngle.Roll = MemoryUtil.PtrToFloat(face3DAngleInfo.Roll + 4 * i);
                    faceInfo.Face3DAngle.Pitch = MemoryUtil.PtrToFloat(face3DAngleInfo.Pitch + 4 * i);
                    faceInfo.Face3DAngle.Yaw = MemoryUtil.PtrToFloat(face3DAngleInfo.Yaw + 4 * i);
                }
                else
                {
                    faceInfo.Face3DAngle.Status = -1;
                }
            }
            else
            {
                faceInfo.Age = -1;
                faceInfo.Gender = -1;
                faceInfo.Face3DAngle.Status = -1;
                faceInfo.Mask = -1;
                //faceInfo.FaceLandPoint = new PointF(-1f, -1f);
            }
            if (hadRgbLive
                //&& rgbLiveInfo.Num == 1
                && i == 0
                && rgbLiveInfo.IsLive != IntPtr.Zero)
            {
                faceInfo.RgbLive = MemoryUtil.PtrToInt(rgbLiveInfo.IsLive + 4 * i);
            }
            else
            {
                faceInfo.RgbLive = -2;
            }
            if (hadRIrLive
                && irLiveInfo.Num == 1
                && i == 0
                && irLiveInfo.IsLive != IntPtr.Zero)
            {
                faceInfo.IrLive = MemoryUtil.PtrToInt(irLiveInfo.IsLive + 4 * i);
            }
            else
            {
                faceInfo.IrLive = -2;
            }
        }

        private static FaceInfo CreateFaceInfo(ASF_MultiFaceInfo multiFaceInfo, int[] orienArry, int i)
        {
            var faceInfo = new FaceInfo
            {
                ASF_FaceInfo = new ASF_SingleFaceInfo
                {
                    FaceRect = (MRECT)MemoryUtil.PtrToStructure(multiFaceInfo.FaceRects + MemoryUtil.SizeOf(typeof(MRECT)) * i, typeof(MRECT)),
                    FaceOrient = (ASF_OrientCode)orienArry[i],
                    FaceDataInfo = (ASF_FaceDataInfo)MemoryUtil.PtrToStructure(multiFaceInfo.FaceDataInfoList + MemoryUtil.SizeOf(typeof(ASF_FaceDataInfo)) * i, typeof(ASF_FaceDataInfo)),
                },
                WearGlasses = MemoryUtil.PtrToFloat(multiFaceInfo.WearGlasses + 4 * i),
                IsLeftEyeClosed = MemoryUtil.PtrToInt(multiFaceInfo.LeftEyeClosed + 4 * i) == 1,
                IsRightEyeClosed = MemoryUtil.PtrToInt(multiFaceInfo.RightEyeClosed + 4 * i) == 1,
                FaceShelter = MemoryUtil.PtrToInt(multiFaceInfo.FaceShelter + 4 * i),
            };

            if (multiFaceInfo.FaceID != IntPtr.Zero)
            {
                faceInfo.FaceID = MemoryUtil.PtrToInt(multiFaceInfo.FaceID + 4 * i);
            }

            faceInfo.Rectangle = new Rectangle(faceInfo.ASF_FaceInfo.FaceRect.Left,
                                            faceInfo.ASF_FaceInfo.FaceRect.Top,
                                            faceInfo.ASF_FaceInfo.FaceRect.Right - faceInfo.ASF_FaceInfo.FaceRect.Left,
                                            faceInfo.ASF_FaceInfo.FaceRect.Bottom - faceInfo.ASF_FaceInfo.FaceRect.Top);
            faceInfo.FaceOrient = faceInfo.ASF_FaceInfo.FaceOrient switch
            {
                ASF_OrientCode.ASF_OC_0 => 0,
                ASF_OrientCode.ASF_OC_90 => 90,
                ASF_OrientCode.ASF_OC_270 => 270,
                ASF_OrientCode.ASF_OC_180 => 180,
                ASF_OrientCode.ASF_OC_30 => 30,
                ASF_OrientCode.ASF_OC_60 => 60,
                ASF_OrientCode.ASF_OC_120 => 120,
                ASF_OrientCode.ASF_OC_150 => 150,
                ASF_OrientCode.ASF_OC_210 => 210,
                ASF_OrientCode.ASF_OC_240 => 240,
                ASF_OrientCode.ASF_OC_300 => 300,
                ASF_OrientCode.ASF_OC_330 => 330,
                _ => 0,
            };
            return faceInfo;
        }

        private static int[] ReadyFaceinStruct(IntPtr pEngine, bool needFaceInfo, bool needRgbLive, ASF_MultiFaceInfo multiFaceInfo,
            ref bool hadFaceInfo, ref ASF_AgeInfo ageInfo, ref ASF_GenderInfo genderInfo, ref ASF_Face3DAngle face3DAngleInfo,
            ref ASF_LivenessInfo rgbLiveInfo, ref bool hadRgbLive, IntPtr pMultiFaceInfo, ref ASF_MaskInfo aSF_MaskInfo, ref ASF_LandMarkInfo lanMaskInfo)
        {
            if (pMultiFaceInfo != IntPtr.Zero)
            {
                if (needFaceInfo
                    && multiFaceInfo.FaceNum > 0)
                {
                    hadFaceInfo = true;
                    ageInfo = AgeEstimation(pEngine);
                    genderInfo = GenderEstimation(pEngine);
                    face3DAngleInfo = Face3DAngleDetection(pEngine);
                    aSF_MaskInfo = MaskEstimation(pEngine);
                    lanMaskInfo = FaceLandEstimation(pEngine);
                }
                if (needRgbLive
                   /* && multiFaceInfo.FaceNum == 1*/)
                {
                    hadRgbLive = true;
                    rgbLiveInfo = LivenessInfo_RGB(pEngine);
                }
            }
            int[] orienArry;
            if (multiFaceInfo.FaceNum > 0)
            {
                orienArry = new int[multiFaceInfo.FaceNum];
                MemoryUtil.Copy(multiFaceInfo.FaceOrients, orienArry, 0, multiFaceInfo.FaceNum);
            }
            else
            {
                orienArry = Array.Empty<int>();
            }
            return orienArry;
        }

        /// <summary>
        /// </summary>
        private static FaceEngineMask SetEngineMask(bool needFaceInfo, bool needRgbLive, ASF_MultiFaceInfo multiFaceInfo)
        {
            var engineMask = FaceEngineMask.ASF_NONE;
            if (needFaceInfo && multiFaceInfo.FaceNum >= 1)
            {
                engineMask |= FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE
                    | FaceEngineMask.ASF_FACELANDMARK | FaceEngineMask.ASF_MASKDETECT;// | FaceEngineMask.ASF_FACESHELTER | FaceEngineMask.ASF_UPDATE_FACEDATA;
            }
            if (needRgbLive/* && multiFaceInfo.FaceNum == 1*/)
            {
                engineMask |= FaceEngineMask.ASF_LIVENESS;
            }

            return engineMask;
        }
    }
}