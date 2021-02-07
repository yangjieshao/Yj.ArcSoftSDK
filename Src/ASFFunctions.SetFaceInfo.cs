using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK._4_0.Models;
using Yj.ArcSoftSDK._4_0.Utils;

namespace Yj.ArcSoftSDK._4_0
{
    public static partial class ASFFunctions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="hadFaceInfo"></param>
        /// <param name="ageInfo"></param>
        /// <param name="genderInfo"></param>
        /// <param name="face3DAngleInfo"></param>
        /// <param name="rgbLiveInfo"></param>
        /// <param name="irLiveInfo"></param>
        /// <param name="maskInfo"></param>
        /// <param name="lanMaskInfo"></param>
        /// <param name="hadRgbLive"></param>
        /// <param name="hadRIrLive"></param>
        /// <param name="i"></param>
        /// <param name="faceInfo"></param>
        private static void SetFaceInfo(bool hadFaceInfo, ASF_AgeInfo ageInfo, ASF_GenderInfo genderInfo, ASF_Face3DAngle face3DAngleInfo, ASF_LivenessInfo rgbLiveInfo
            , ASF_LivenessInfo irLiveInfo, ASF_MaskInfo maskInfo, ASF_LandMarkInfo lanMaskInfo, bool hadRgbLive, bool hadRIrLive, int i, FaceInfo faceInfo)
        {
            if (hadFaceInfo)
            {
                if (ageInfo.Num > i)
                {
                    faceInfo.Age = MemoryUtil.PtrToStructure<int>(ageInfo.AgeArray + MemoryUtil.SizeOf<int>() * i);
                }
                else
                {
                    faceInfo.Age = -1;
                }
                if (genderInfo.Num > i)
                {
                    faceInfo.Gender = MemoryUtil.PtrToStructure<int>(genderInfo.GenderArray + MemoryUtil.SizeOf<int>() * i);
                }
                else
                {
                    faceInfo.Gender = -1;
                }

                if (maskInfo.Num > i)
                {
                    faceInfo.Mask = MemoryUtil.PtrToStructure<int>(maskInfo.MaskArray + MemoryUtil.SizeOf<int>() * i);
                }
                else
                {
                    faceInfo.Mask = -1;
                }
                if (lanMaskInfo.Num > i)
                {
                    var faceLandmark = MemoryUtil.PtrToStructure<ASF_FaceLandmark>(lanMaskInfo.Point + MemoryUtil.SizeOf<ASF_FaceLandmark>() * i);
                    faceInfo.FaceLandPoint = new PointF(faceLandmark.X, faceLandmark.Y);
                }
                else
                {
                    faceInfo.FaceLandPoint = new PointF(-1f, -1f);
                }
                if (face3DAngleInfo.Num > i)
                {
                    faceInfo.Face3DAngle.Status = 0;
                    //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                    faceInfo.Face3DAngle.Roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.Roll + MemoryUtil.SizeOf<float>() * i);
                    faceInfo.Face3DAngle.Pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.Pitch + MemoryUtil.SizeOf<float>() * i);
                    faceInfo.Face3DAngle.Yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.Yaw + MemoryUtil.SizeOf<float>() * i);
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
                faceInfo.FaceLandPoint = new PointF(-1f, -1f);
            }
            if (hadRgbLive
                && rgbLiveInfo.Num == 1)
            {
                faceInfo.RgbLive = MemoryUtil.PtrToStructure<int>(rgbLiveInfo.IsLive);
            }
            else
            {
                faceInfo.RgbLive = -2;
            }
            if (hadRIrLive
                && irLiveInfo.Num == 1)
            {
                faceInfo.IrLive = MemoryUtil.PtrToStructure<int>(irLiveInfo.IsLive);
            }
            else
            {
                faceInfo.IrLive = -2;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="multiFaceInfo"></param>
        /// <param name="orienArry"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static FaceInfo CreateFaceInfo(ASF_MultiFaceInfo multiFaceInfo, int[] orienArry, int i)
        {
            FaceInfo faceInfo = new FaceInfo
            {
                ASF_FaceInfo = new ASF_SingleFaceInfo
                {
                    FaceRect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.FaceRects + MemoryUtil.SizeOf< MRECT>() * i),
                    FaceOrient = (ASF_OrientCode)orienArry[i],
                    FaceDataInfo = MemoryUtil.PtrToStructure<ASF_FaceDataInfo>(multiFaceInfo.FaceDataInfoList + MemoryUtil.SizeOf<ASF_FaceDataInfo>() * i),
                },
                IsLeftEyeClosed = MemoryUtil.PtrToStructure<int>(multiFaceInfo.LeftEyeClosed + MemoryUtil.SizeOf<int>() * i) == 1,
                IsRightEyeClosed = MemoryUtil.PtrToStructure<int>(multiFaceInfo.RightEyeClosed + MemoryUtil.SizeOf<int>() * i) == 1,
                FaceShelter = MemoryUtil.PtrToStructure<int>(multiFaceInfo.FaceShelter + MemoryUtil.SizeOf<int>() * i),
            };
            if (multiFaceInfo.FaceID != IntPtr.Zero)
            {
                faceInfo.FaceID = MemoryUtil.PtrToStructure<int>(multiFaceInfo.FaceID + MemoryUtil.SizeOf<int>() * i);
            }

            faceInfo.Rectangle = new Rectangle(faceInfo.ASF_FaceInfo.FaceRect.Left,
                                            faceInfo.ASF_FaceInfo.FaceRect.Top,
                                            faceInfo.ASF_FaceInfo.FaceRect.Right - faceInfo.ASF_FaceInfo.FaceRect.Left,
                                            faceInfo.ASF_FaceInfo.FaceRect.Bottom - faceInfo.ASF_FaceInfo.FaceRect.Top);
            switch (faceInfo.ASF_FaceInfo.FaceOrient)
            {
                case ASF_OrientCode.ASF_OC_0:
                    faceInfo.FaceOrient = 0;
                    break;

                case ASF_OrientCode.ASF_OC_90:
                    faceInfo.FaceOrient = 90;
                    break;

                case ASF_OrientCode.ASF_OC_270:
                    faceInfo.FaceOrient = 270;
                    break;

                case ASF_OrientCode.ASF_OC_180:
                    faceInfo.FaceOrient = 180;
                    break;

                case ASF_OrientCode.ASF_OC_30:
                    faceInfo.FaceOrient = 30;
                    break;

                case ASF_OrientCode.ASF_OC_60:
                    faceInfo.FaceOrient = 60;
                    break;

                case ASF_OrientCode.ASF_OC_120:
                    faceInfo.FaceOrient = 120;
                    break;

                case ASF_OrientCode.ASF_OC_150:
                    faceInfo.FaceOrient = 150;
                    break;

                case ASF_OrientCode.ASF_OC_210:
                    faceInfo.FaceOrient = 210;
                    break;

                case ASF_OrientCode.ASF_OC_240:
                    faceInfo.FaceOrient = 240;
                    break;

                case ASF_OrientCode.ASF_OC_300:
                    faceInfo.FaceOrient = 300;
                    break;

                case ASF_OrientCode.ASF_OC_330:
                    faceInfo.FaceOrient = 330;
                    break;

                default:
                    faceInfo.FaceOrient = 0;
                    break;
            }

            return faceInfo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="needFaceInfo"></param>
        /// <param name="needRgbLive"></param>
        /// <param name="multiFaceInfo"></param>
        /// <param name="hadFaceInfo"></param>
        /// <param name="ageInfo"></param>
        /// <param name="genderInfo"></param>
        /// <param name="face3DAngleInfo"></param>
        /// <param name="rgbLiveInfo"></param>
        /// <param name="hadRgbLive"></param>
        /// <param name="pMultiFaceInfo"></param>
        /// <param name="aSF_MaskInfo"></param>
        /// <param name="lanMaskInfo"></param>
        /// <returns></returns>
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
                    && multiFaceInfo.FaceNum == 1)
                {
                    hadRgbLive = true;
                    rgbLiveInfo = LivenessInfo_RGB(pEngine);
                }
                MemoryUtil.Free(ref pMultiFaceInfo);
            }
            int[] orienArry;
            if (multiFaceInfo.FaceNum>0)
            {
                orienArry = new int[multiFaceInfo.FaceNum];
                Marshal.Copy(multiFaceInfo.FaceOrients, orienArry, 0, multiFaceInfo.FaceNum);
            }
            else
            {
                orienArry = new int[]{ };
            }
            return orienArry;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="needFaceInfo"></param>
        /// <param name="needRgbLive"></param>
        /// <param name="needImageQuality"></param>
        /// <param name="multiFaceInfo"></param>
        /// <returns></returns>
        private static FaceEngineMask SetEngineMask(bool needFaceInfo, bool needRgbLive,bool needImageQuality, ASF_MultiFaceInfo multiFaceInfo)
        {
            FaceEngineMask engineMask = FaceEngineMask.ASF_NONE;
            if (needFaceInfo && multiFaceInfo.FaceNum >= 1)
            {
                engineMask |= FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE
                    | FaceEngineMask.ASF_FACELANDMARK | FaceEngineMask.ASF_MASKDETECT;// | FaceEngineMask.ASF_FACESHELTER | FaceEngineMask.ASF_UPDATE_FACEDATA;
            }
            if (needRgbLive && multiFaceInfo.FaceNum == 1)
            {
                engineMask |= FaceEngineMask.ASF_LIVENESS;
            }
            //if (needImageQuality && multiFaceInfo.FaceNum >= 1)
            //{
            //    engineMask |= FaceEngineMask.ASF_IMAGEQUALITY;
            //}

            return engineMask;
        }
    }
}