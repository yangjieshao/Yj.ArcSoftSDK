using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// 
    /// </summary>
    public struct Rectangle
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
                if (Height == 0 && Width == 0 && X == 0)
                {
                    return Y == 0;
                }

                return false;
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
            X = x;
            Y = y;
            Width = width;
            Height = height;
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
        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }

            Rectangle rectangle = (Rectangle)obj;
            if (rectangle.X == X && rectangle.Y == Y && rectangle.Width == Width)
            {
                return rectangle.Height == Height;
            }

            return false;
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
            return X ^ (Y << 13 | (int)((uint)Y >> 19)) ^ (Width << 26 | (int)((uint)Width >> 6)) ^ (Height << 7 | (int)((uint)Height >> 25));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + ",Width=" + Width + ",Height=" + Height + "}";
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public struct PointF : IEquatable<PointF>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly PointF Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Math.Abs(X - 0.0d) < double.Epsilon
                    && Math.Abs(Y - 0.0d) < double.Epsilon;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public float X { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public float Y { set; get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="NullReferenceException"></exception>
        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool Equals(PointF other)
        {
            throw null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PointF))
            {
                return false;
            }

            PointF pointF = (PointF)obj;
            return Math.Abs(pointF.X - X) < double.Epsilon
                && Math.Abs(pointF.Y - Y) < double.Epsilon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 8845;
            hashCode = hashCode * -8799 + X.GetHashCode();
            hashCode = hashCode * -8799 + Y.GetHashCode();
            return hashCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(PointF left, PointF right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(PointF left, PointF right)
        {
            return !left.Equals(right);
        }
    }
}
