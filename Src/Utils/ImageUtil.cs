using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Yj.ArcSoftSDK._4_0.Models;

namespace Yj.ArcSoftSDK._4_0.Utils
{
    /// <summary>
    ///
    /// </summary>
    public static class ImageUtil
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="inImage"></param>
        /// <param name="needCheckImage"></param>
        /// <returns></returns>
        public static ASVL_OFFSCREEN GetImageData(Image inImage, bool needCheckImage = false)
        {
            ASVL_OFFSCREEN result = new ASVL_OFFSCREEN
            {
                u32PixelArrayFormat = (uint)ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8,
                ppu8Plane = new IntPtr[4],
                pi32Pitch = new int[4]
            };

            int pitch;

            //将Image转换为 Format24bppRgb 格式的BMP
            Bitmap image;
            bool needDisposeImage = false;
            if(inImage.PixelFormat!= PixelFormat.Format24bppRgb
                || inImage.Flags!=2
                || needCheckImage)
            {
                needDisposeImage = true;
                image = new Bitmap(inImage);
            }
            else
            {
                image = inImage as Bitmap;
            }
            //将Bitmap锁定到系统内存中,获得BitmapData
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行
            IntPtr ptr = data.Scan0;
            //定义数组长度
            int soureBitArrayLength = data.Height * Math.Abs(data.Stride);
            byte[] sourceBitArray = new byte[soureBitArrayLength];
            //将bitmap中的内容拷贝到ptr_bgr数组中
            Marshal.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);
            result.i32Width = data.Width;
            result.i32Height = data.Height;
            pitch = Math.Abs(data.Stride);

            result.pi32Pitch[0] = data.Stride;// data.Width * 3;
            int bgr_len = result.pi32Pitch[0] * data.Height;
            byte[] destBitArray = new byte[bgr_len];
            for (int i = 0; i < data.Height; ++i)
            {
                Array.Copy(sourceBitArray, i * pitch, destBitArray, i * result.pi32Pitch[0], result.pi32Pitch[0]);
            }

            result.ppu8Plane[0] = MemoryUtil.Malloc(destBitArray.Length);
            MemoryUtil.Copy(destBitArray, 0, result.ppu8Plane[0], destBitArray.Length);

            image.UnlockBits(data);

            if (needDisposeImage)
            {
                image.Dispose();
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inImage"></param>
        /// <param name="needCheckImage"></param>
        /// <returns></returns>
        public static ASVL_OFFSCREEN GetImageData_IR(Bitmap inImage, bool needCheckImage = false)
        {
            inImage.ConverGray();
            return GetImageData(inImage, needCheckImage);
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>成功或失败</returns>
        public static ImageInfo ReadBMP(Image image)
        {
            ImageInfo result = new ImageInfo();

            //将Image转换为Format24bppRgb格式的BMP
            Bitmap bm;
            if (image.PixelFormat != PixelFormat.Format24bppRgb)
            {
                bm = new Bitmap(image);
            }
            else
            {
                bm = image as Bitmap;
            }
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行
                IntPtr ptr = data.Scan0;

                //定义数组长度
                int soureBitArrayLength = data.Height * Math.Abs(data.Stride);
                byte[] sourceBitArray = new byte[soureBitArrayLength];

                //将bitmap中的内容拷贝到ptr_bgr数组中
                MemoryUtil.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);

                //填充引用对象字段值
                result.Width = data.Width;
                result.Height = data.Height;
                result.Format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;

                //获取去除对齐位后度图像数据
                int line = result.Width * 3;
                int pitch = Math.Abs(data.Stride);
                int bgr_len = line * result.Height;
                byte[] destBitArray = new byte[bgr_len];

                /*
                 * 图片像素数据在内存中是按行存储，一般图像库都会有一个内存对齐，在每行像素的末尾位置
                 * 每行的对齐位会使每行多出一个像素空间（三通道如RGB会多出3个字节，四通道RGBA会多出4个字节）
                 * 以下循环目的是去除每行末尾的对齐位，将有效的像素拷贝到新的数组
                 */
                for (int i = 0; i < result.Height; ++i)
                {
                    Array.Copy(sourceBitArray, i * pitch, destBitArray, i * line, line);
                }

                result.ImgData = MemoryUtil.Malloc(destBitArray.Length);
                MemoryUtil.Copy(destBitArray, 0, result.ImgData, destBitArray.Length);
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                bm.UnlockBits(data);
            }

            if (image.PixelFormat != PixelFormat.Format24bppRgb)
            {
                bm.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 获取图片IR信息
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <returns>成功或失败</returns>
        public static ImageInfo ReadBMP_IR(Bitmap bitmap)
        {
            bitmap.ConverGray();
            return ReadBMP(bitmap);
        }

        /// <summary>
        /// 图像灰度化
        /// </summary>
        /// <param name="curBitmpap"></param>
        public static void ConverGray(this Bitmap curBitmpap)
        {
            //位图矩形
            Rectangle rect = new Rectangle(0, 0, curBitmpap.Width, curBitmpap.Height);
            //以可读写的方式锁定全部位图像素
            BitmapData bmpData = curBitmpap.LockBits(rect, ImageLockMode.ReadWrite, curBitmpap.PixelFormat);
            //得到首地址
            IntPtr ptr = bmpData.Scan0;

            //定义被锁定的数组大小，由位图数据与未用空间组成的
            int bytes = bmpData.Stride * bmpData.Height;
            //定义位图数组
            byte[] rgbValues = new byte[bytes];
            //复制被锁定的位图像素值到该数组内
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            //灰度化
            double colorTemp;
            for (int i = 0; i < bmpData.Height; i++)
            {
                //只处理每行中是图像像素的数据，舍弃未用空间
                for (int j = 0; j < bmpData.Width * 3; j += 3)
                {
                    //利用公式计算灰度值
                    colorTemp = rgbValues[i * bmpData.Stride + j + 2] * 0.299 + rgbValues[i * bmpData.Stride + j + 1] * 0.587 + rgbValues[i * bmpData.Stride + j] * 0.114;
                    //R=G=B
                    rgbValues[i * bmpData.Stride + j] = rgbValues[i * bmpData.Stride + j + 1] = rgbValues[i * bmpData.Stride + j + 2] = (byte)colorTemp;
                }
            }

            //把数组复制回位图
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            //解锁位图像素
            curBitmpap.UnlockBits(bmpData);
        }

        /// <summary>
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        internal static Bitmap ScaleImage(Image image, int dstWidth, int dstHeight)
        {
            try
            {
                //按比例缩放
                float scaleRate = GetWidthAndHeight(image.Width, image.Height, dstWidth, dstHeight);
                int width = (int)(image.Width * scaleRate);
                int height = (int)(image.Height * scaleRate);

                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width -= width % 4;
                }

                Bitmap destBitmap = new Bitmap(width, height);
                using (var g = Graphics.FromImage(destBitmap))
                {
                    g.Clear(Color.Transparent);

                    //设置画布的描绘质量
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }

                return destBitmap;
            }
            catch (Exception)
            {
                // no use
            }

            return null;
        }

        /// <summary>
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeigt">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        internal static float GetWidthAndHeight(int oldWidth, int oldHeigt, int newWidth, int newHeight)
        {
            //按比例缩放
            float scaleRate;
            if (oldWidth >= newWidth && oldHeigt >= newHeight)
            {
                int widthDis = oldWidth - newWidth;
                int heightDis = oldHeigt - newHeight;
                if (widthDis > heightDis)
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
                else
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
            }
            else if (oldWidth >= newWidth /*&& oldHeigt < newHeight*/)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (/*oldWidth < newWidth &&*/ oldHeigt >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeigt;
            }
            else
            {
                int widthDis = newWidth - oldWidth;
                int heightDis = newHeight - oldHeigt;
                if (widthDis > heightDis)
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
                else
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
            }
            return scaleRate;
        }

        /// <summary>
        /// 重设图片DPI
        /// </summary>
        /// <param name="image"></param>
        /// <param name="xDpi"></param>
        /// <param name="yDpi"></param>
        /// <param name="useOldSize">使用旧照片尺寸，
        /// <para>true则按比例缩放图片大小，false则按原大小填充图像</para></param>
        /// <returns></returns>
        internal static Bitmap ResetDpi(this Image image, float xDpi = 96, float yDpi = 96, bool useOldSize = true)
        {
            Bitmap result;
            if (image.HorizontalResolution != xDpi
                || image.VerticalResolution != yDpi)
            {
                Bitmap bitmap;
                if (useOldSize)
                {
                    bitmap = new Bitmap(image.Width, image.Height, image.PixelFormat);
                }
                else
                {
                    bitmap = new Bitmap((int)(image.Width * xDpi / image.HorizontalResolution)
                            , (int)(image.Height * yDpi / image.VerticalResolution), image.PixelFormat);
                }
                bitmap.SetResolution(xDpi, yDpi);
                using (var graphic = Graphics.FromImage(bitmap))
                {
                    //设置高质量查值法
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //设置高质量，低速度呈现平滑程度
                    graphic.SmoothingMode = SmoothingMode.HighQuality;
                    //清空画布并以透明背景色填充
                    graphic.Clear(Color.Transparent);
                    graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                }
                result = bitmap;
            }
            else
            {
                result = new Bitmap(image);
            }
            return result;
        }

        /// <summary>
        /// 宽度4的倍数校验
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        internal static Image CheckImage(Image image)
        {
            Image result = null;
            if (image != null)
            {
                result = (Image)image.Clone();
                
                if (result.Width % 4 != 0
                    || result.Height % 4 != 0)
                {
                    Image newImage = ScaleImage(result, result.Width - (result.Width % 4), result.Height - (result.Height % 4));
                    result.Dispose();
                    result = newImage;
                }
            }

            return result;
        }
    }
}