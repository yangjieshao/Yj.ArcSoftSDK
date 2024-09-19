using System;
using Yj.ArcSoftSDK.Models;
using Yj.ArcSoftSDK.Utils;

namespace Yj.ArcSoftSDK
{
    public partial class ASFFunctions
    {
        /// <summary>
        /// </summary>
        private static void SetFaceInfo(bool hadFaceInfo, ASF_AgeInfo ageInfo, ASF_GenderInfo genderInfo, ASF_Face3DAngle face3DAngleInfo, ASF_LivenessInfo rgbLiveInfo, ASF_LivenessInfo irLiveInfo, bool hadRgbLive, bool hadRIrLive, int i, FaceInfo faceInfo)
        {
            if (hadFaceInfo)
            {
                if (ageInfo.Num > i)
                {
                    faceInfo.Age = MemoryUtil.PtrToInt(ageInfo.AgeArray + 4 * i);
                }
                if (genderInfo.Num > i)
                {
                    faceInfo.Gender = MemoryUtil.PtrToInt(genderInfo.GenderArray + 4 * i);
                }
                if (face3DAngleInfo.Num > i)
                {
                    faceInfo.Face3DAngle.Status = MemoryUtil.PtrToInt(face3DAngleInfo.Status + 4 * i);
                    if (faceInfo.Face3DAngle.Status == 0)
                    {
                        //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                        faceInfo.Face3DAngle.Roll = MemoryUtil.PtrToFloat(face3DAngleInfo.Roll + 4 * i);
                        faceInfo.Face3DAngle.Pitch = MemoryUtil.PtrToFloat(face3DAngleInfo.Pitch + 4 * i);
                        faceInfo.Face3DAngle.Yaw = MemoryUtil.PtrToFloat(face3DAngleInfo.Yaw + 4 * i);
                    }
                }
            }
            if (hadRgbLive
              /*  && rgbLiveInfo.Num == 1*/
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
              && IsPro
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

        /// <summary>
        /// </summary>
        private static FaceInfo CreateFaceInfo(ASF_MultiFaceInfo multiFaceInfo, int[] orienArry, int i)
        {
            var faceInfo = new FaceInfo
            {
                ASF_FaceInfo = new Models.ASF_SingleFaceInfo
                {
                    FaceRect = (MRECT)MemoryUtil.PtrToStructure(multiFaceInfo.FaceRects + MemoryUtil.SizeOf(typeof(MRECT)) * i, typeof(MRECT)),
                    FaceOrient = (ASF_OrientCode)orienArry[i],
                },
            };

            if (multiFaceInfo.FaceID != IntPtr.Zero)
            {
                faceInfo.FaceID = MemoryUtil.PtrToInt(multiFaceInfo.FaceID + 4 * i);
            }

            faceInfo.Rectangle = new Models.Rectangle(faceInfo.ASF_FaceInfo.FaceRect.Left,
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

        /// <summary>
        /// </summary>
        private static int[] ReadyFaceinStruct(IntPtr pEngine, bool needFaceInfo, bool needRgbLive, ASF_MultiFaceInfo multiFaceInfo,
            ref bool hadFaceInfo, ref ASF_AgeInfo ageInfo, ref ASF_GenderInfo genderInfo, ref ASF_Face3DAngle face3DAngleInfo,
            ref ASF_LivenessInfo rgbLiveInfo, ref bool hadRgbLive, IntPtr pMultiFaceInfo)
        {
            if (pMultiFaceInfo != IntPtr.Zero)
            {
                if (needFaceInfo
                    && multiFaceInfo.FaceNum <= 4)
                {
                    hadFaceInfo = true;
                    ageInfo = AgeEstimation(pEngine);
                    genderInfo = GenderEstimation(pEngine);
                    face3DAngleInfo = Face3DAngleDetection(pEngine);
                }
                if (needRgbLive
                    /*&& multiFaceInfo.FaceNum == 1*/)
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
            FaceEngineMask engineMask = FaceEngineMask.ASF_NONE;
            if (needFaceInfo && multiFaceInfo.FaceNum >= 1 && multiFaceInfo.FaceNum <= 4)
            {
                engineMask |= FaceEngineMask.ASF_AGE;
                engineMask |= FaceEngineMask.ASF_GENDER;
                engineMask |= FaceEngineMask.ASF_FACE3DANGLE;
            }
            if (needRgbLive /*&& multiFaceInfo.FaceNum == 1*/)
            {
                engineMask |= FaceEngineMask.ASF_LIVENESS;
            }

            return engineMask;
        }
    }
}