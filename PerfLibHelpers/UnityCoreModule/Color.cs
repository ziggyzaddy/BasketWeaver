using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

// Helper Namespace, referenced and removed by BasketWeaver during Injetion
namespace Inject
{
    namespace UnityEngine
    {
        public struct Color : IEquatable<Color>
        {
            public float r;
            public float g;
            public float b;
            public float a;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator +(Color a, Color b)
            {
                a.r = a.r + b.r;
                a.g = a.g + b.g;
                a.b = a.b + b.b;
                a.a = a.a + b.a;
                return a;
            }

            // Token: 0x060003DC RID: 988 RVA: 0x00006D44 File Offset: 0x00004F44
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator -(Color a, Color b)
            {
                a.r = a.r - b.r;
                a.g = a.g - b.g;
                a.b = a.b - b.b;
                a.a = a.a - b.a;
                return a;
            }

            // Token: 0x060003DD RID: 989 RVA: 0x00006D9C File Offset: 0x00004F9C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(Color a, Color b)
            {
                a.r = a.r * b.r;
                a.g = a.g * b.g;
                a.b = a.b * b.b;
                a.a = a.a * b.a;
                return a;
            }

            // Token: 0x060003DE RID: 990 RVA: 0x00006DF4 File Offset: 0x00004FF4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(Color a, float b)
            {
                a.r = a.r * b;
                a.g = a.g * b;
                a.b = a.b * b;
                a.a = a.a * b;
                return a;
            }

            // Token: 0x060003DF RID: 991 RVA: 0x00006E34 File Offset: 0x00005034
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator *(float b, Color a)
            {
                a.r = a.r * b;
                a.g = a.g * b;
                a.b = a.b * b;
                a.a = a.a * b;
                return a;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator /(Color a, float b)
            {
                float recip = 1.0f / b;
                a.r = a.r * b;
                a.g = a.g * b;
                a.b = a.b * b;
                a.a = a.a * b;
                return a;
            }


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                if (!(other is Color))
                {
                    return false;
                }
                return Equals((Color)other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Color other)
            {
                return r.Equals(other.r) && g.Equals(other.g) && b.Equals(other.b) && a.Equals(other.a);
            }

            public override string ToString()
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("RGBA(");
                stringBuilder.Append(this.r.ToString("F3"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.g.ToString("F3"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.b.ToString("F3"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.a.ToString("F3"));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x060003D7 RID: 983 RVA: 0x00006BB4 File Offset: 0x00004DB4
            public string ToString(string format)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("RGBA(");
                stringBuilder.Append(this.r.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.g.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.b.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.a.ToString(format));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }


        }
    }
}
