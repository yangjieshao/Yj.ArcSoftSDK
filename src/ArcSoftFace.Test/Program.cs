using System;
using System.IO;
using Yj.ArcSoftSDK._4_0;

namespace ArcSoftFace.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".NETVersion:" + Environment.Version);
            var arcTestUtil = new ArcTestUtil();
            arcTestUtil.InitEngines();
            arcTestUtil.Test1();
            //arcTestUtil.Test2();
            Console.WriteLine($"press any key to exit");
            Console.ReadKey();
        }
    }



    public class ArcTestUtil
    {
        IntPtr _pVideoRGBImageEngine = IntPtr.Zero;
        /// <summary>
        /// 初始化引擎
        /// </summary>
        public void InitEngines()
        {
            try
            {
                var deviceInfo = ASFFunctions.GetActiveDeviceInfo();
                Console.WriteLine($"deviceInfo: {deviceInfo}");
                int retCode = ASFFunctions.Activation(appId: "1212"
                    , x86SdkKey: "11"
                    , x64SdkKey: "1212"
                    , sox64Key: "12"
                    , x64ProActiveKey: "1212"
                    , x86ProActiveKey: "13"
                    , sox64ProActiveKey: "14");
                Console.WriteLine($"Activation: {retCode}");

                retCode = ASFFunctions.InitEngine(pEngine: ref _pVideoRGBImageEngine, isImgMode: true, faceMaxNum: 5,
                    isAngleZeroOnly: false, needFaceInfo: true, needRgbLive: true, needIrLive: true, needFaceFeature: true);
                Console.WriteLine($"Init pEngine: {retCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Test1()
        {
            try
            {
                var faceInfos = ASFFunctions.DetectFacesEx(_pVideoRGBImageEngine, File.ReadAllBytes("pics/1.jpg"),
                                                            faceMinWith: 0,
                                                            needCheckImage: true,
                                                            needFaceInfo: true,
                                                            needRgbLive: true,
                                                            needIrLive: true,
                                                            needFeatures: true,
                                                            isRegister: false);
                var faceInfos2 = ASFFunctions.DetectFacesEx(_pVideoRGBImageEngine, File.ReadAllBytes("pics/2.jpg"),
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}