#if !(NETFRAMEWORK)
using SkiaSharp;
using System;
using Yj.ArcSoftSDK.Models;

namespace Yj.ArcSoftSDK.Utils
{
    /// <summary>
    /// </summary>
    internal static partial class ImageUtil
    {
        /// <summary>
        /// </summary>
        public static ASVL_OFFSCREEN GetImageData(SKBitmap inImage)
        {
            var result = new ASVL_OFFSCREEN
            {
                u32PixelArrayFormat = (uint)ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8,
                ppu8Plane = new IntPtr[4],
                pi32Pitch = new int[4],
                i32Width = inImage.Width,
                i32Height = inImage.Height
            };
            result.pi32Pitch[0] = inImage.Width * 3;// data.Width * 3;

            var sourceBitArray = inImage.Bytes;
            var destBitArray = new byte[inImage.Width * inImage.Height * 3];

            for (int sourceIndex = 0, destIndex = 0; sourceIndex < sourceBitArray.Length; sourceIndex += 4, destIndex += 3)
            {
                destBitArray[destIndex] = sourceBitArray[sourceIndex];
                destBitArray[destIndex + 1] = sourceBitArray[sourceIndex + 1];
                destBitArray[destIndex + 2] = sourceBitArray[sourceIndex + 2];
            }

            result.ppu8Plane[0] = MemoryUtil.Malloc(destBitArray.Length);
            MemoryUtil.Copy(destBitArray, 0, result.ppu8Plane[0], destBitArray.Length);
            return result;
        }

        /// <summary>
        /// </summary>
        public static ASVL_OFFSCREEN GetImageData_IR(SKBitmap inImage)
        {
            var result = new ASVL_OFFSCREEN
            {
                u32PixelArrayFormat = (uint)ASF_ImagePixelFormat.ASVL_PAF_GRAY,
                ppu8Plane = new IntPtr[4],
                pi32Pitch = new int[4],
                i32Width = inImage.Width,
                i32Height = inImage.Height
            };
            result.pi32Pitch[0] = inImage.Width ;

            var sourceBitArray = inImage.Bytes;
            var destBitArray = new byte[inImage.Width * inImage.Height];

            for (int sourceIndex = 0, destIndex = 0; sourceIndex < sourceBitArray.Length; sourceIndex += 4, destIndex ++)
            {
                destBitArray[destIndex] = (byte)(sourceBitArray[sourceIndex + 2] * 0.299 + sourceBitArray[sourceIndex + 1] * 0.587 + sourceBitArray[sourceIndex] * 0.114);
            }

            result.ppu8Plane[0] = MemoryUtil.Malloc(destBitArray.Length);
            MemoryUtil.Copy(destBitArray, 0, result.ppu8Plane[0], destBitArray.Length);
            return result;
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        public static ImageInfo ReadBMP(SKBitmap image)
        {
            var result = new ImageInfo();
            try
            {
                //填充引用对象字段值
                result.Width = image.Width;
                result.Height = image.Height;
                result.Format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;
                var sourceBitArray = image.Bytes;
                var destBitArray = new byte[image.Width * image.Height * 3];

                for (int sourceIndex = 0, destIndex = 0; sourceIndex < sourceBitArray.Length; sourceIndex += 4, destIndex += 3)
                {
                    destBitArray[destIndex] = sourceBitArray[sourceIndex];
                    destBitArray[destIndex + 1] = sourceBitArray[sourceIndex + 1];
                    destBitArray[destIndex + 2] = sourceBitArray[sourceIndex + 2];
                }
                result.ImgData = MemoryUtil.Malloc(destBitArray.Length);
                MemoryUtil.Copy(destBitArray, 0, result.ImgData, destBitArray.Length);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 获取图片IR信息
        /// </summary>
        public static ImageInfo ReadBMP_IR(SKBitmap image)
        {
            var result = new ImageInfo();
            try
            {
                //填充引用对象字段值
                result.Width = image.Width;
                result.Height = image.Height;
                result.Format = ASF_ImagePixelFormat.ASVL_PAF_GRAY;
                var sourceBitArray = image.Bytes;
                var destBitArray = new byte[image.Width * image.Height];

                for (int sourceIndex = 0, destIndex = 0; sourceIndex < sourceBitArray.Length; sourceIndex += 4, destIndex ++)
                {
                    destBitArray[destIndex] = (byte)(sourceBitArray[sourceIndex + 2] * 0.299 + sourceBitArray[sourceIndex + 1] * 0.587 + sourceBitArray[sourceIndex] * 0.114);
                }
                result.ImgData = MemoryUtil.Malloc(destBitArray.Length);
                MemoryUtil.Copy(destBitArray, 0, result.ImgData, destBitArray.Length);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 宽度4的倍数校验
        /// </summary>
        internal static SKBitmap CheckImage(SKBitmap image)
        {
            SKBitmap result = null;
            if (image != null)
            {
                result = image.Copy();

                if (result.Width % 4 != 0
                    || result.Height % 4 != 0)
                {
                    var newImage = result.Resize(new SKImageInfo(result.Width - (result.Width % 4), result.Height - (result.Height % 4)), SKFilterQuality.High);
                    result.Dispose();
                    result = newImage;
                }
            }

            return result;
        }
    }
}
#endif