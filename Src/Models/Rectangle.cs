using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 
    /// </summary>
    public struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Rectangle Empty;

        /// <summary>
        /// 
        /// </summary>
        public int X { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Y { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Width { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Left => X;

        /// <summary>
        /// 
        /// </summary>
        public int Top => Y;

        /// <summary>
        /// 
        /// </summary>
        public int Right => X + Width;

        /// <summary>
        /// 
        /// </summary>
        public int Bottom => Y + Height;

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Height == 0
                    && Width == 0
                    && X == 0
                    && Y == 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Rectangle FromLTRB(int left, int top, int right, int bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool operator ==(Rectangle left, Rectangle right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Width == right.Width)
            {
                return left.Height == right.Height;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X ^ ((Y << 13) | (int)((uint)Y >> 19)) ^ ((Width << 26) | (int)((uint)Width >> 6)) ^ ((Height << 7) | (int)((uint)Height >> 25));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + ",Width=" + Width + ",Height=" + Height + "}";
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }

            return Equals((Rectangle)obj);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Rectangle other)
        {
            return other.X == X
                && other.Y == Y
                && other.Width == Width
                && other.Height == Height;
        }
    }
}
