# 硬件要求
最低配置：Intel® CoreTM i5-2300@2.80GHz 或者同级别芯片<br/>
推荐配置：Intel® CoreTM i7-4600U@2.1GHz 或者同级别芯片<br/>

版本： So64  v4.0<br/>
版本： x86 x64 v4.0<br/>



部署到新设备时 不要复制授权文件
商用授权 授权文件名字为 : ArcFacePro64.dat  或  ArcFacePro32.dat
离线激活 可以从官网 开发者中心 -> 帮助中心 -> 下载3.1的激活小助手 -> 生成硬件信息 -> 从开发者中心用硬件信息生成授权文件
-> 把授权文件黏贴到进程运行目录 -> 重命名授权文件未 ArcFacePro64.dat 或 ArcFacePro32.dat

ArcFace64.dat ArcFace32.dat   是免费授权的授权文件名

Linux 部署
创建软连接到要使用的虹软库文件

例：
```
ln /home/app/ArcProLib/Sox64_4.0/libarcsoft_face.so  /usr/lib/libarcsoft_face.so
ln /home/app/ArcProLib/Sox64_4.0/libarcsoft_face_engine.so  /usr/lib/libarcsoft_face_engine.so
ln /home/app/ArcProLib/Sox64_4.0/libarcsoft_face.so  /usr/lib64/libarcsoft_face.so
ln /home/app/ArcProLib/Sox64_4.0/libarcsoft_face_engine.so  /usr/lib64/libarcsoft_face_engine.so
```

## 初始化虹软人脸sdk
````csharp
string proActiveKey32 = Ini.ReadIniData("ArcFace", "ProActiveKey32", string.Empty);
string proActiveKey64 = Ini.ReadIniData("ArcFace", "ProActiveKey64", string.Empty);
string proActiveKeySo64 = Ini.ReadIniData("ArcFace", "ProActiveKeySo64", string.Empty);

string appId = Ini.ReadIniData("ArcFace", "APPID", string.Empty);
string key32 = Ini.ReadIniData("ArcFace", "KEY32", string.Empty);
string key64 = Ini.ReadIniData("ArcFace", "KEY64", string.Empty);
string keySo64 = Ini.ReadIniData("ArcFace", "KEYSo64", string.Empty);

_ = ASFFunctions.Activation(appId, key32, key64, keySo64, proActiveKey32, proActiveKey64, proActiveKeySo64);
````
## 初始化人脸引擎
````csharp

_ = ASFFunctions.InitEngine(pEngine: ref engine, isImgMode: isImgMode, faceMaxNum: maxFaceNum,
    isAngleZeroOnly: false, needFaceInfo: true, needRgbLive: needRgbLive, needIrLive: false,
    needFaceFeature: true,needImageQuality: needImageQuality);
````
## 分析人脸照片
````csharp
List<FaceInfo> result = null;

if (engine != IntPtr.Zero)
{
    try
    {
        Image image = Converter.BuffToImage(imageBuffer);
        if(needCheckImage)
        {
            Converter.CleanImagePropertyItems(ref image);
        }

        result = ASFFunctions.DetectFacesEx(engine, image, faceMinWith: minWidth,
                    needCheckImage: needCheckImage, needFaceInfo: needFaceInfo, needRgbLive: needRgbLive,
                    needIrLive: false, needFeatures: needFeatures, needImageQuality: needImageQuality);
        image.Dispose();
    }
    catch (Exception ex)
    {
        Log.Instance.LogWrite(ex);
    }
}
            
````
## 特征值对比
````csharp
var similarity = ASFFunctions.FaceFeatureCompare(_DetectFacesEngine, feature1, feature2, ASFFunctions.IsPro && isIdcardCompare);
var similarity = ASFFunctions.FaceFeatureCompare(_DetectFacesEngine, featureIntptr1, featureIntptr2, ASFFunctions.IsPro && isIdcardCompare);
````
## 特征值转指针对比
````csharp 
var featureIntptr=ASFFunctions.Feature2IntPtr(feature)
````
