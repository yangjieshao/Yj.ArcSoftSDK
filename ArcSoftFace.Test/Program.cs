using System;
using System.IO;
using Yj.ArcSoftSDK._4_0;

namespace ArcSoftFace.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ArcTestUtil arcTestUtil = new ArcTestUtil();
            arcTestUtil.InitEngines();
        }
    }


    public class ArcTestUtil
    {
        /// <summary>
        /// 初始化引擎
        /// </summary>
        public void InitEngines()
        {
            int retCode=-1;
            try
            {
                IntPtr _pVideoRGBImageEngine = IntPtr.Zero;

                //retCode = ASFFunctions.Activation(appId: "11111"
                //    , x86SdkKey: ""
                //    , x64SdkKey: "22222"
                //    , sox64Key: ""
                //    , x64ProActiveKey: "33333"
                //    , x86ProActiveKey: "");
                //Console.WriteLine($"Activation: {retCode}");

                retCode = ASFFunctions.InitEngine(pEngine: ref _pVideoRGBImageEngine, isImgMode: true, faceMaxNum: 5,
                    isAngleZeroOnly: false, needFaceInfo: true, needRgbLive: true, needIrLive: true, needFaceFeature: true);
                Console.WriteLine($"Init pEngine: {retCode}");

                var faceInfos = ASFFunctions.DetectFacesEx(_pVideoRGBImageEngine, File.ReadAllBytes("3.jpg"),
                                                            faceMinWith: 0,
                                                            needCheckImage: true,
                                                            needFaceInfo: true,
                                                            needRgbLive: true,
                                                            needIrLive: true,
                                                            needFeatures: true,
                                                            isRegister:false);
                var faceInfos2 = ASFFunctions.DetectFacesEx(_pVideoRGBImageEngine, File.ReadAllBytes("003.jpg"),
                                                            faceMinWith: 0,
                                                            needCheckImage: true,
                                                            needFaceInfo: true,
                                                            needRgbLive: true,
                                                            needIrLive: true,
                                                            needFeatures: true,
                                                            isRegister: false);

                byte[] feature1 = null;// System.IO.File.ReadAllBytes("feature1.dat");
                foreach (var faceInfo in faceInfos)
                {
                    if (faceInfo.Feature != null)
                    {
                        feature1 = new byte[faceInfo.Feature.Length];
                        faceInfo.Feature.CopyTo(feature1, 0);
                    }

                    Console.WriteLine($"pic1 FaceID:{faceInfo.FaceID} 角度: {faceInfo.FaceOrient}" +
                        $" 年龄: {faceInfo.Age} 性别: {faceInfo.Gender} 活体: {faceInfo.RgbLive}" +
                        $" Rect<Left:{faceInfo.Rectangle.Left:0000},Right:{faceInfo.Rectangle.Right:0000},Top:{faceInfo.Rectangle.Top:0000},Bottom:{faceInfo.Rectangle.Bottom:0000}>" +
                        $" 3DAngle<Roll:{faceInfo.Face3DAngle.Roll:000.000},Yaw:{faceInfo.Face3DAngle.Yaw:000.000},Pitch:{faceInfo.Face3DAngle.Pitch:000.000}>");
                }

                byte[] feature2 = null;// System.IO.File.ReadAllBytes("feature2.dat");
                foreach (var faceInfo in faceInfos2)
                {
                    if (faceInfo.Feature != null)
                    {
                        feature2 = new byte[faceInfo.Feature.Length];
                        faceInfo.Feature.CopyTo(feature2, 0);
                    }

                    Console.WriteLine($"pic2 FaceID:{faceInfo.FaceID} 角度: {faceInfo.FaceOrient}" +
                        $" 年龄: {faceInfo.Age} 性别: {faceInfo.Gender} 活体: {faceInfo.RgbLive}" +
                        $" Rect<Left:{faceInfo.Rectangle.Left:0000},Right:{faceInfo.Rectangle.Right:0000},Top:{faceInfo.Rectangle.Top:0000},Bottom:{faceInfo.Rectangle.Bottom:0000}>" +
                        $" 3DAngle<Roll:{faceInfo.Face3DAngle.Roll:000.000},Yaw:{faceInfo.Face3DAngle.Yaw:000.000},Pitch:{faceInfo.Face3DAngle.Pitch:000.000}>");
                }

                if (feature1 != null
                    && feature1.Length > 0
                    && feature2 != null
                    && feature2.Length > 0)
                {
                    float similarity = ASFFunctions.FaceFeatureCompare(_pVideoRGBImageEngine, feature1, feature2);
                    Console.WriteLine($"bitmap1 similarity bitmap2: {similarity}");
                }
            }
            catch
            {

            }

        }
    }
}