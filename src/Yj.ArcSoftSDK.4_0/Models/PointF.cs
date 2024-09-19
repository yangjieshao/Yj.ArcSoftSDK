using System;

namespace Yj.ArcSoftSDK._4_0.Models
{
    /// <summary>
    /// </summary>
    public struct PointF : IEquatable<PointF>
    {
        /// <summary>
        /// </summary>
        public static readonly PointF Empty;

        /// <summary>
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
        /// </summary>
        public float X { set; get; }

        /// <summary>
        /// </summary>
        public float Y { set; get; }

        /// <summary>
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
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PointF other)
        {
            return Math.Abs(other.X - X) < double.Epsilon
                && Math.Abs(other.Y - Y) < double.Epsilon;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PointF))
            {
                return false;
            }
            return Equals((PointF)obj);
        }

        /// <summary>
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
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(PointF left, PointF right)
        {
            return left.Equals(right);
        }

        /// <summary>
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