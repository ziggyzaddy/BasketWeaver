using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Inject
{
    namespace UnityEngine
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct Color32
        {
            [FieldOffset(0)]
            private int rgba;
            [FieldOffset(0)]
            public byte r;
            [FieldOffset(1)]
            public byte g;
            [FieldOffset(2)]
            public byte b;
            [FieldOffset(3)]
            public byte a;
            public Color32(byte r, byte g, byte b, byte a)
            {
                this.rgba = 0;
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            [MethodImpl((MethodImplOptions)256)]
            public static implicit operator Color32(Color c)
            {
                Color32 color;
                color.rgba = 0;
                color.r = (byte)((double)Mathf.Clamp01(c.r) * (double)byte.MaxValue);
                color.g = (byte)((double)Mathf.Clamp01(c.g) * (double)byte.MaxValue);
                color.b = (byte)((double)Mathf.Clamp01(c.b) * (double)byte.MaxValue);
                color.a = (byte)((double)Mathf.Clamp01(c.a) * (double)byte.MaxValue);
                return color;
            }

            [MethodImpl((MethodImplOptions)256)]
            public static implicit operator Color(Color32 c)
            {
                Color color;

                float recip = 1.0f / (float)byte.MaxValue;
                color.r = (float)c.r * recip;
                color.g = (float)c.g * recip;
                color.b = (float)c.b * recip;
                color.a = (float)c.a * recip;
                return color;
            }

            [MethodImpl((MethodImplOptions)256)]
            public static Color32 Lerp(Color32 a, Color32 b, float t)
            {
                t = Mathf.Clamp01(t);

                a.r = (byte)((double)a.r + (double)((int)b.r - (int)a.r) * (double)t);
                a.g = (byte)((double)a.g + (double)((int)b.g - (int)a.g) * (double)t);
                a.b = (byte)((double)a.b + (double)((int)b.b - (int)a.b) * (double)t);
                a.a = (byte)((double)a.a + (double)((int)b.a - (int)a.a) * (double)t);
                return a;
            }

            [MethodImpl((MethodImplOptions)256)]
            public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
            {

                a.r = (byte)((double)a.r + (double)((int)b.r - (int)a.r) * (double)t);
                a.g = (byte)((double)a.g + (double)((int)b.g - (int)a.g) * (double)t);
                a.b = (byte)((double)a.b + (double)((int)b.b - (int)a.b) * (double)t);
                a.a = (byte)((double)a.a + (double)((int)b.a - (int)a.a) * (double)t);
                return a;
            }

            [MethodImpl((MethodImplOptions)256)]
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("RGBA(");
                stringBuilder.Append((object)this.r.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.g.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.b.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.a.ToString());
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            [MethodImpl((MethodImplOptions)256)]
            public string ToString(string format)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("RGBA(");
                stringBuilder.Append((object)this.r.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.g.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.b.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append((object)this.a.ToString(format));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

        }

    }
}
