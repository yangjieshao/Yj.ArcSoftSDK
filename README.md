# 硬件要求
最低配置：Intel® CoreTM i5-2300@2.80GHz 或者同级别芯片 实测 J1900 也勉强能用<br/>
推荐配置：Intel® CoreTM i7-4600U@2.1GHz 或者同级别芯片<br/>

版本： So64  v3.0 v3.1 v4.0<br/>
版本： x86 x64 v3.0 v3.1 v4.0<br/>

3.0 的授权文件和账号是绑定的 自行从虹软的开发者中心下载自己的库文件并替换



部署到新设备时 不要复制授权文件
商用授权 授权文件名字为 : ArcFacePro64.dat  或  ArcFacePro32.dat <br/>
离线激活 可以从官网 [开发者中心](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/login)  -> [帮助中心](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/help) -> 下载3.1的[激活小助手 win](https://ai.arcsoft.com.cn/ucenter/uploadFiles/winv2.zip),[激活小助手 linux](https://ai.arcsoft.com.cn/ucenter/uploadFiles/linuxv2.zip) -> 生成硬件信息 -> 从开发者中心用硬件信息生成授权文件
-> 把授权文件黏贴到进程运行目录 -> 重命名授权文件为 ArcFacePro64.dat 或 ArcFacePro32.dat

ArcFace64.dat ArcFace32.dat   是免费授权的授权文件名

Linux 部署
创建软连接到要使用的虹软库文件

例：
```
cp -Rf /usr/Projects/ArcLib/4.0/* /usr/lib64
cp -Rf /usr/Projects/ArcLib/4.0/* /usr/lib
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

## 针对部分有多个网卡的Linux系统 （特别是UOS） 每次启动会读取不同网卡mac的情况
仅针对 v4.0 </br>
首先 禁用全部网卡后 一个个网卡的启用并获取设备信息 注册获取授权文件 </br>
获取到所有网卡的授权文件后
````csharp
int activationRet = -1;
foreach (var activeFile in ArcSoftConfig.ActiveFiles)
{
    activationRet = ASFFunctions.OfflineActivation(activeFile);
    if (activationRet == 0)
    {
        var initEngine = IntPtr.Zero;
        var initEngineRet = ASFFunctions.InitEngine(pEngine: ref initEngine, isImgMode: true, faceMaxNum: 0,
            isAngleZeroOnly: false, needFaceInfo: false, needRgbLive: false, needIrLive: false,
            needFaceFeature: false, needImageQuality: false);
        if(initEngineRet == 0 )
        {
            Logger.LogInformation("初始化虹软 使用授权文件 激活SDK成功 永久版:true  授权文件地址:{activeFile}", activeFile);
            ASFFunctions.UninitEngine(ref initEngine);
            break;
        }
        else if(initEngineRet == 90118)
        {
            Logger.LogWarning("初始化虹软 使用授权文件 激活SDK失败 设备不匹配 授权文件地址:{activeFile}", activeFile);
        }
        else
        {
            Logger.LogWarning("初始化虹软 使用授权文件 激活SDK失败 授权文件地址:{activeFile}  {initEngineRet}", activeFile, initEngineRet);
        }
    }
    else
    {
        activationRet = -1;
    }
}
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
## 特征值转指针用于对比
````csharp 
var featureIntptr=ASFFunctions.Feature2IntPtr(feature)
````
## 释放特征值转指针
````csharp 
var featureIntptr=ASFFunctions.FreeFeatureIntPtr(featureIntptr)
````
