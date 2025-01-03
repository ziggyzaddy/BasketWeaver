using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Internal;

namespace BasketWeaver
{
    namespace UnityEngine
    {
        // Token: 0x02000142 RID: 322
        public struct Quaternion : IEquatable<Quaternion>
        {
            // Token: 0x06000CE5 RID: 3301 RVA: 0x000129F3 File Offset: 0x00010BF3
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Quaternion(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            // Token: 0x06000CE6 RID: 3302 RVA: 0x00012A14 File Offset: 0x00010C14
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
            {
                Quaternion result;
                Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out result);
                return result;
            }

            // Token: 0x06000CE7 RID: 3303 RVA: 0x00012A30 File Offset: 0x00010C30
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Inverse(Quaternion rotation)
            {
                Quaternion result;
                Quaternion.Inverse_Injected(ref rotation, out result);
                return result;
            }

            // Token: 0x06000CE8 RID: 3304 RVA: 0x00012A48 File Offset: 0x00010C48
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
            {
                Quaternion result;
                Quaternion.Slerp_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CE9 RID: 3305 RVA: 0x00012A64 File Offset: 0x00010C64
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
            {
                Quaternion result;
                Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CEA RID: 3306 RVA: 0x00012A80 File Offset: 0x00010C80
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
            {
                Quaternion result;
                Quaternion.Lerp_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CEB RID: 3307 RVA: 0x00012A9C File Offset: 0x00010C9C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
            {
                Quaternion result;
                Quaternion.LerpUnclamped_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CEC RID: 3308 RVA: 0x00012AB8 File Offset: 0x00010CB8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Quaternion Internal_FromEulerRad(Vector3 euler)
            {
                Quaternion result;
                Quaternion.Internal_FromEulerRad_Injected(ref euler, out result);
                return result;
            }

            // Token: 0x06000CED RID: 3309 RVA: 0x00012AD0 File Offset: 0x00010CD0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Vector3 Internal_ToEulerRad(Quaternion rotation)
            {
                Vector3 result;
                Quaternion.Internal_ToEulerRad_Injected(ref rotation, out result);
                return result;
            }

            // Token: 0x06000CEE RID: 3310 RVA: 0x00012AE7 File Offset: 0x00010CE7
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
            {
                Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
            }

            // Token: 0x06000CEF RID: 3311 RVA: 0x00012AF4 File Offset: 0x00010CF4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion AngleAxis(float angle, Vector3 axis)
            {
                Quaternion result;
                Quaternion.AngleAxis_Injected(angle, ref axis, out result);
                return result;
            }

            // Token: 0x06000CF0 RID: 3312 RVA: 0x00012B0C File Offset: 0x00010D0C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
            {
                Quaternion result;
                Quaternion.LookRotation_Injected(ref forward, ref upwards, out result);
                return result;
            }

            // Token: 0x06000CF1 RID: 3313 RVA: 0x00012B28 File Offset: 0x00010D28
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion LookRotation(Vector3 forward)
            {
                return Quaternion.LookRotation(forward, Vector3.up);
            }

            // Token: 0x170002B0 RID: 688
            public float this[int index]
            {
                get
                {
                    float result;
                    switch (index)
                    {
                        case 0:
                            result = this.x;
                            break;
                        case 1:
                            result = this.y;
                            break;
                        case 2:
                            result = this.z;
                            break;
                        case 3:
                            result = this.w;
                            break;
                        default:
                            throw new IndexOutOfRangeException("Invalid Quaternion index!");
                    }
                    return result;
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
                        case 2:
                            this.z = value;
                            break;
                        case 3:
                            this.w = value;
                            break;
                        default:
                            throw new IndexOutOfRangeException("Invalid Quaternion index!");
                    }
                }
            }

            // Token: 0x06000CF4 RID: 3316 RVA: 0x000129F3 File Offset: 0x00010BF3
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set(float newX, float newY, float newZ, float newW)
            {
                this.x = newX;
                this.y = newY;
                this.z = newZ;
                this.w = newW;
            }

            // Token: 0x170002B1 RID: 689
            // (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00012C14 File Offset: 0x00010E14
            public static Quaternion identity
            {
                get
                {
                    return Quaternion.identityQuaternion;
                }
            }

            // Token: 0x06000CF6 RID: 3318 RVA: 0x00012C30 File Offset: 0x00010E30
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
            {
                Quaternion res;
                res.x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
                res.y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
                res.z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;
                res.w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
                return res;
            }

            // Token: 0x06000CF7 RID: 3319 RVA: 0x00012D48 File Offset: 0x00010F48
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator *(Quaternion rotation, Vector3 point)
            {
                float num = rotation.x * 2f;
                float num2 = rotation.y * 2f;
                float num3 = rotation.z * 2f;
                float num4 = rotation.x * num;
                float num5 = rotation.y * num2;
                float num6 = rotation.z * num3;
                float num7 = rotation.x * num2;
                float num8 = rotation.x * num3;
                float num9 = rotation.y * num3;
                float num10 = rotation.w * num;
                float num11 = rotation.w * num2;
                float num12 = rotation.w * num3;
                Vector3 result;
                result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
                result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
                result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
                return result;
            }

            // Token: 0x06000CF8 RID: 3320 RVA: 0x00012E90 File Offset: 0x00011090
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static bool IsEqualUsingDot(float dot)
            {
                return dot > 0.999999f;
            }

            // Token: 0x06000CF9 RID: 3321 RVA: 0x00012EB0 File Offset: 0x000110B0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Quaternion lhs, Quaternion rhs)
            {
                return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
            }

            // Token: 0x06000CFA RID: 3322 RVA: 0x00012ED4 File Offset: 0x000110D4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Quaternion lhs, Quaternion rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06000CFB RID: 3323 RVA: 0x00012EF4 File Offset: 0x000110F4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Dot(Quaternion a, Quaternion b)
            {
                return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            }

            // Token: 0x06000CFC RID: 3324 RVA: 0x00012F48 File Offset: 0x00011148
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetLookRotation(Vector3 view)
            {
                Vector3 up = Vector3.up;
                this.SetLookRotation(view, up);
            }

            // Token: 0x06000CFD RID: 3325 RVA: 0x00012F64 File Offset: 0x00011164
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
            {
                this = Quaternion.LookRotation(view, up);
            }

            // Token: 0x06000CFE RID: 3326 RVA: 0x00012F74 File Offset: 0x00011174
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Angle(Quaternion a, Quaternion b)
            {
                float num = Quaternion.Dot(a, b);
                return (!Quaternion.IsEqualUsingDot(num)) ? ((float)Math.Acos(Math.Min(Math.Abs(num), 1f)) * 2f * 57.29578f) : 0f;
            }

            // Token: 0x06000CFF RID: 3327 RVA: 0x00012FC8 File Offset: 0x000111C8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Vector3 Internal_MakePositive(Vector3 euler)
            {
                float num = -0.005729578f;
                float num2 = 360f + num;
                if (euler.x < num)
                {
                    euler.x += 360f;
                }
                else if (euler.x > num2)
                {
                    euler.x -= 360f;
                }
                if (euler.y < num)
                {
                    euler.y += 360f;
                }
                else if (euler.y > num2)
                {
                    euler.y -= 360f;
                }
                if (euler.z < num)
                {
                    euler.z += 360f;
                }
                else if (euler.z > num2)
                {
                    euler.z -= 360f;
                }
                return euler;
            }

            // Token: 0x170002B2 RID: 690
            // (get) Token: 0x06000D00 RID: 3328 RVA: 0x000130BC File Offset: 0x000112BC
            // (set) Token: 0x06000D01 RID: 3329 RVA: 0x000130EB File Offset: 0x000112EB
            public Vector3 eulerAngles
            {
                get
                {
                    return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
                }
                set
                {
                    this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
                }
            }

            // Token: 0x06000D02 RID: 3330 RVA: 0x00013104 File Offset: 0x00011304
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Euler(float x, float y, float z)
            {
                Vector3 vec;
                vec.x = x;
                vec.y = y;
                vec.z = z;
                return Quaternion.Internal_FromEulerRad(vec * 0.017453292f);
            }

            // Token: 0x06000D03 RID: 3331 RVA: 0x00013130 File Offset: 0x00011330
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Euler(Vector3 euler)
            {
                return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
            }

            // Token: 0x06000D04 RID: 3332 RVA: 0x00013155 File Offset: 0x00011355
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void ToAngleAxis(out float angle, out Vector3 axis)
            {
                Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
                angle *= 57.29578f;
            }

            // Token: 0x06000D05 RID: 3333 RVA: 0x0001316F File Offset: 0x0001136F
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
            {
                this = Quaternion.FromToRotation(fromDirection, toDirection);
            }

            // Token: 0x06000D06 RID: 3334 RVA: 0x00013180 File Offset: 0x00011380
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
            {
                float num = Quaternion.Angle(from, to);
                Quaternion result;
                if (num == 0f)
                {
                    result = to;
                }
                else
                {
                    result = Quaternion.SlerpUnclamped(from, to, Math.Min(1f, maxDegreesDelta / num));
                }
                return result;
            }

            // Token: 0x06000D07 RID: 3335 RVA: 0x000131C4 File Offset: 0x000113C4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion Normalize(Quaternion q)
            {
                float num = (float)Math.Sqrt(Quaternion.Dot(q, q));
                Quaternion result;
                if (num < Mathf.Epsilon)
                {
                    result = Quaternion.identity;
                }
                else
                {
                    float recip = 1.0f / num;
                    Quaternion res;
                    res.x = q.x * num;
                    res.y = q.y * num;
                    res.z = q.z * num;
                    res.w = q.w * num;
                    return res;
                }
                return result;
            }

            // Token: 0x06000D08 RID: 3336 RVA: 0x00013225 File Offset: 0x00011425
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Normalize()
            {
                this = Quaternion.Normalize(this);
            }

            // Token: 0x170002B3 RID: 691
            // (get) Token: 0x06000D09 RID: 3337 RVA: 0x0001323C File Offset: 0x0001143C
            public Quaternion normalized
            {
                get
                {
                    return Quaternion.Normalize(this);
                }
            }

            // Token: 0x06000D0A RID: 3338 RVA: 0x0001325C File Offset: 0x0001145C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
            }

            // Token: 0x06000D0B RID: 3339 RVA: 0x000132C0 File Offset: 0x000114C0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Quaternion && this.Equals((Quaternion)other);
            }

            // Token: 0x06000D0C RID: 3340 RVA: 0x000132F4 File Offset: 0x000114F4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Quaternion other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(this.x.ToString("F1"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.y.ToString("F1"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.z.ToString("F1"));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.w.ToString("F1"));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x06000CDD RID: 3293
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ToString(string format)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(this.x.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.y.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.z.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(this.w.ToString(format));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }


            // Token: 0x06000D0F RID: 3343 RVA: 0x00013428 File Offset: 0x00011628
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion EulerRotation(float x, float y, float z)
            {
                return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
            }

            // Token: 0x06000D10 RID: 3344 RVA: 0x0001344C File Offset: 0x0001164C
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion EulerRotation(Vector3 euler)
            {
                return Quaternion.Internal_FromEulerRad(euler);
            }

            // Token: 0x06000D11 RID: 3345 RVA: 0x00013467 File Offset: 0x00011667
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetEulerRotation(float x, float y, float z)
            {
                this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
            }

            // Token: 0x06000D12 RID: 3346 RVA: 0x0001347D File Offset: 0x0001167D
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetEulerRotation(Vector3 euler)
            {
                this = Quaternion.Internal_FromEulerRad(euler);
            }

            // Token: 0x06000D13 RID: 3347 RVA: 0x0001348C File Offset: 0x0001168C
            [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3 ToEuler()
            {
                return Quaternion.Internal_ToEulerRad(this);
            }

            // Token: 0x06000D14 RID: 3348 RVA: 0x000134AC File Offset: 0x000116AC
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion EulerAngles(float x, float y, float z)
            {
                return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
            }

            // Token: 0x06000D15 RID: 3349 RVA: 0x000134D0 File Offset: 0x000116D0
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion EulerAngles(Vector3 euler)
            {
                return Quaternion.Internal_FromEulerRad(euler);
            }

            // Token: 0x06000D16 RID: 3350 RVA: 0x000134EB File Offset: 0x000116EB
            [Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void ToAxisAngle(out Vector3 axis, out float angle)
            {
                Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
            }

            // Token: 0x06000D17 RID: 3351 RVA: 0x000134FB File Offset: 0x000116FB
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetEulerAngles(float x, float y, float z)
            {
                this.SetEulerRotation(new Vector3(x, y, z));
            }

            // Token: 0x06000D18 RID: 3352 RVA: 0x0001350C File Offset: 0x0001170C
            [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetEulerAngles(Vector3 euler)
            {
                this = Quaternion.EulerRotation(euler);
            }

            // Token: 0x06000D19 RID: 3353 RVA: 0x0001351C File Offset: 0x0001171C
            [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 ToEulerAngles(Quaternion rotation)
            {
                return Quaternion.Internal_ToEulerRad(rotation);
            }

            // Token: 0x06000D1A RID: 3354 RVA: 0x00013538 File Offset: 0x00011738
            [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3 ToEulerAngles()
            {
                return Quaternion.Internal_ToEulerRad(this);
            }

            // Token: 0x06000D1B RID: 3355 RVA: 0x00013558 File Offset: 0x00011758
            [Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetAxisAngle(Vector3 axis, float angle)
            {
                this = Quaternion.AxisAngle(axis, angle);
            }

            // Token: 0x06000D1C RID: 3356 RVA: 0x00013568 File Offset: 0x00011768
            [Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Quaternion AxisAngle(Vector3 axis, float angle)
            {
                return Quaternion.AngleAxis(57.29578f * angle, axis);
            }

            // Token: 0x06000D1E RID: 3358
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void FromToRotation_Injected(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion ret);

            // Token: 0x06000D1F RID: 3359
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Inverse_Injected(ref Quaternion rotation, out Quaternion ret);

            // Token: 0x06000D20 RID: 3360
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Slerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

            // Token: 0x06000D21 RID: 3361
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void SlerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

            // Token: 0x06000D22 RID: 3362
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Lerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

            // Token: 0x06000D23 RID: 3363
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void LerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

            // Token: 0x06000D24 RID: 3364
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret);

            // Token: 0x06000D25 RID: 3365
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Internal_ToEulerRad_Injected(ref Quaternion rotation, out Vector3 ret);

            // Token: 0x06000D26 RID: 3366
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Internal_ToAxisAngleRad_Injected(ref Quaternion q, out Vector3 axis, out float angle);

            // Token: 0x06000D27 RID: 3367
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void AngleAxis_Injected(float angle, ref Vector3 axis, out Quaternion ret);

            // Token: 0x06000D28 RID: 3368
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void LookRotation_Injected(ref Vector3 forward, [DefaultValue("Vector3.up")] ref Vector3 upwards, out Quaternion ret);

            // Token: 0x040006B6 RID: 1718
            public float x;

            // Token: 0x040006B7 RID: 1719
            public float y;

            // Token: 0x040006B8 RID: 1720
            public float z;

            // Token: 0x040006B9 RID: 1721
            public float w;

            // Token: 0x040006BA RID: 1722
            private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

            // Token: 0x040006BB RID: 1723
            public const float kEpsilon = 1E-06f;
        }
    }
}