#region netDxf, Copyright(C) 2009 Daniel Carvajal, Licensed under LGPL.

//                        netDxf library
// Copyright (C) 2009 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 

#endregion

using System;

namespace netDxf
{
    /// <summary>
    /// Represent a two component vector of double precision.
    /// </summary>
    public struct Vector2
    {
        #region private fields

        private double x;
        private double y;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of Vector2.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Initializes a new instance of Vector2.
        /// </summary>
        /// <param name="array">Array of two elements that represents the vector.</param>
        public Vector2(double[] array)
        {
            if (array.Length != 2)
                throw new ArgumentOutOfRangeException("array", array.Length, "The dimension of the array must be two");
            this.x = array[0];
            this.y = array[1];
        }

        #endregion

        #region constants

        /// <summary>
        /// Zero vector.
        /// </summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        /// <summary>
        /// Unit X vector.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return new Vector2(1, 0); }
        }

        /// <summary>
        /// Unit Y vector.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return new Vector2(0, 1); }
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the X component.
        /// </summary>
        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Gets or sets the Y component.
        /// </summary>
        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        /// <summary>
        /// Gets or sets a vector element defined by its index.
        /// </summary>
        /// <param name="index">Index of the element.</param>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:

                        return this.y;
                    default:

                        throw (new ArgumentOutOfRangeException("index"));
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:

                        this.y = value;
                        break;
                    default:

                        throw (new ArgumentOutOfRangeException("index"));
                }
            }
        }

        #endregion

        #region static methods

        /// <summary>
        /// Obtains the dot product of two vectors.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>The dot product.</returns>
        public static double DotProduct(Vector2 u, Vector2 v)
        {
            return (u.X*v.X) + (u.Y*v.Y);
        }

        /// <summary>
        /// Obtains the cross product of two vectors.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Vector2.</returns>
        public static double CrossProduct(Vector2 u, Vector2 v)
        {
            return (u.X*v.Y) - (u.Y*v.X);
        }

        /// <summary>
        /// Obtains the counter clockwise perpendicular vector .
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Perpendicular(Vector2 u)
        {
            return new Vector2(-u.Y, u.X);
        }

        /// <summary>
        /// Obtains the distance between two points.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Distancie.</returns>
        public static double Distance(Vector2 u, Vector2 v)
        {
            return (Math.Sqrt((u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y)));
        }

        /// <summary>
        /// Obtains the square distance between two points.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Square distance.</returns>
        public static double SquareDistance(Vector2 u, Vector2 v)
        {
            return (u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y);
        }

        /// <summary>
        /// Obtains the angle between two vectors.
        /// </summary>
        /// <param name="u">A normalized Vector2.</param>
        /// <param name="v">A normalized Vector2.</param>
        /// <returns>Angle in radians.</returns>
        public static double AngleBetween(Vector2 u, Vector2 v)
        {
            double dx = v.X - u.X;
            double dy = v.Y - u.Y;
            double angle = Math.Atan2(dy, dx);
            if (angle<0)
                return MathHelper.TwoPI + angle;
            return angle;
        }

        /// <summary>
        /// Obtains the midpoint.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 MidPoint(Vector2 u, Vector2 v)
        {
            return new Vector2((v.X + u.X)*0.5F, (v.Y + u.Y)*0.5F);
        }

        /// <summary>
        /// Checks if two vectors are perpendicular.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <param name="threshold">Tolerance used.</param>
        /// <returns>True if are penpendicular or false in anyother case.</returns>
        public static bool ArePerpendicular(Vector2 u, Vector2 v, double threshold)
        {
            return MathHelper.IsZero(DotProduct(u, v), threshold);
        }

        /// <summary>
        /// Checks if two vectors are parallel.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <param name="threshold">Tolerance used.</param>
        /// <returns>True if are parallel or false in anyother case.</returns>
        public static bool AreParallel(Vector2 u, Vector2 v, double threshold)
        {
            double a = u.X*v.Y - u.Y*v.X;
            return MathHelper.IsZero(a, threshold);
        }

        /// <summary>
        /// Rounds the components of a vector.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="numDigits">Number of significative defcimal digits.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Round(Vector2 u, int numDigits)
        {
            return new Vector2(Math.Round(u.X, numDigits), Math.Round(u.Y, numDigits));
        }

        #endregion

        #region overloaded operators

        /// <summary>
        /// Check if the components of two vectors are equal.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>True if the two components are equal or false in anyother case.</returns>
        public static bool operator ==(Vector2 u, Vector2 v)
        {
            return ((MathHelper.IsEqual(v.X, u.X)) && (MathHelper.IsEqual(v.Y, u.Y)));
        }

        /// <summary>
        /// Check if the components of two vectors are different.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>True if the two components are different or false in anyother case.</returns>
        public static bool operator !=(Vector2 u, Vector2 v)
        {
            return ((!MathHelper.IsEqual(v.X, u.X)) || (!MathHelper.IsEqual(v.Y, u.Y)));
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>The addition of u plus v.</returns>
        public static Vector2 operator +(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X + v.X, u.Y + v.Y);
        }

        /// <summary>
        /// Substracts two vectors.
        /// </summary>
        /// <param name="u">Vector3.</param>
        /// <param name="v">Vector3.</param>
        /// <returns>The substraction of u minus v.</returns>
        public static Vector2 operator -(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X - v.X, u.Y - v.Y);
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <returns>The negative vector of u.</returns>
        public static Vector2 operator -(Vector2 u)
        {
            return new Vector2(-u.X, -u.Y);
        }

        /// <summary>
        /// Multuplies a vector with an scalar (same as a*u, conmutative property).
        /// </summary>
        /// <param name="u">Vector2.</param>
        /// <param name="a">Scalar.</param>
        /// <returns>The multiplication of u times a.</returns>
        public static Vector2 operator *(Vector2 u, double a)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        /// <summary>
        /// Multuplies an scalar with a vector (same as u*a, conmutative property).
        /// </summary>
        /// <param name="a">Scalar.</param>
        /// <param name="u">Vector3.</param>
        /// <returns>The multiplication of a times u.</returns>
        public static Vector2 operator *(double a, Vector2 u)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        /// <summary>
        /// Divides a vector with an scalar (not same as a/v).
        /// </summary>
        /// <param name="a">Vector3.</param>
        /// <param name="u">Scalar.</param>
        /// <returns>The multiplication of a times u.</returns>
        public static Vector2 operator /(Vector2 u, double a)
        {
            double invEscalar = 1/a;
            return new Vector2(u.X*invEscalar, u.Y*invEscalar);
        }

        /// <summary>
        /// Divides an scalar with a vector (not same as v/a).
        /// </summary>
        /// <param name="a">Vector3.</param>
        /// <param name="u">Scalar.</param>
        /// <returns>The multiplication of a times u.</returns>
        public static Vector2 operator /(double a, Vector2 u)
        {
            double invEscalar = 1/a;
            return new Vector2(u.X*invEscalar, u.Y*invEscalar);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        public void Normalize()
        {
            double mod = this.Modulus();
            if (MathHelper.IsZero(mod))
                throw new ArithmeticException("Cannot normalize a zero vector");
            double modInv = 1/mod;
            this.x *= modInv;
            this.y *= modInv;
        }

        /// <summary>
        /// Obtains the modulus of the vector.
        /// </summary>
        /// <returns>Vector modulus.</returns>
        public double Modulus()
        {
            return (Math.Sqrt(DotProduct(this, this)));
        }

        /// <summary>
        /// Returns an array that represents the vector.
        /// </summary>
        /// <returns>Array.</returns>
        public double[] ToArray()
        {
            double[] u = new[] {this.x, this.y};
            return u;
        }

        #endregion

        #region comparision methods

        /// <summary>
        /// Check if the components of two vectors are approximate equals.
        /// </summary>
        /// <param name="obj">Vector2.</param>
        /// <param name="threshold">Maximun tolerance.</param>
        /// <returns>True if the three components are almost equal or false in anyother case.</returns>
        public bool Equals(Vector2 obj, double threshold)
        {
            return ((MathHelper.IsEqual(obj.X, this.x, threshold)) && (MathHelper.IsEqual(obj.Y, this.y, threshold)));
        }

        /// <summary>
        /// Check if the components of two vectors are equals (uses double.Epsilon the tolerance).
        /// </summary>
        /// <param name="obj">Vector2.</param>
        /// <returns>True if the three components are almost equal or false in anyother case.</returns>
        public bool Equals(Vector2 obj)
        {
            return ((MathHelper.IsEqual(obj.X, this.x)) && (MathHelper.IsEqual(obj.Y, this.y)));
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
                return this.Equals((Vector2) obj);
            return false;
        }

        public override int GetHashCode()
        {
            return unchecked(this.X.GetHashCode() ^ this.Y.GetHashCode());
        }

        #endregion

        #region overrides

        /// <summary>
        /// Obtains a string that represents the vector.
        /// </summary>
        /// <returns>A string text.</returns>
        public override string ToString()
        {
            return string.Format("{0};{1}", this.x, this.y);
        }

        /// <summary>
        /// Obtains a string that represents the vector.
        /// </summary>
        /// <param name="provider">An IFormatProvider interface implementation that supplies culture-specific formatting information. </param>
        /// <returns>A string text.</returns>
        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0};{1}", this.x.ToString(provider), this.y.ToString(provider));
        }

        #endregion
    }
}