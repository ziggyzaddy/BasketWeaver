using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Inject
{
    namespace UnityEngine
    {
        // Token: 0x0200024D RID: 589
        public struct Vector4 : IEquatable<Vector4>
        {
            // Token: 0x0600153D RID: 5437 RVA: 0x0002295B File Offset: 0x00020B5B
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector4(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            // Token: 0x0600153E RID: 5438 RVA: 0x0002297B File Offset: 0x00020B7B
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector4(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = 0f;
            }

            // Token: 0x0600153F RID: 5439 RVA: 0x0002299E File Offset: 0x00020B9E
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector4(float x, float y)
            {
                this.x = x;
                this.y = y;
                this.z = 0f;
                this.w = 0f;
            }

            // Token: 0x17000431 RID: 1073
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
                            throw new IndexOutOfRangeException("Invalid Vector4 index!");
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
                            throw new IndexOutOfRangeException("Invalid Vector4 index!");
                    }
                }
            }

            // Token: 0x06001542 RID: 5442 RVA: 0x0002295B File Offset: 0x00020B5B
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set(float newX, float newY, float newZ, float newW)
            {
                this.x = newX;
                this.y = newY;
                this.z = newZ;
                this.w = newW;
            }

            // Token: 0x06001543 RID: 5443 RVA: 0x00022A94 File Offset: 0x00020C94
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
            {
                t = Mathf.Clamp01(t);
                Vector4 res;
                res.x = a.x + (b.x - a.x) * t;
                res.y = a.y + (b.y - a.y) * t;
                res.z = a.z + (b.z - a.z) * t;
                res.w = a.w + (b.w - a.w) * t;
                return res;
            }

            // Token: 0x06001544 RID: 5444 RVA: 0x00022B1C File Offset: 0x00020D1C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
            {
                a.x = a.x + (b.x - a.x) * t;
                a.y = a.y + (b.y - a.y) * t;
                a.z = a.z + (b.z - a.z) * t;
                a.w = a.w + (b.w - a.w) * t;
                return a;
            }

            // Token: 0x06001545 RID: 5445 RVA: 0x00022B9C File Offset: 0x00020D9C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
            {
                Vector4 a = target - current;
                float magnitude = a.magnitude;
                Vector4 result;
                if (magnitude <= maxDistanceDelta || magnitude == 0f)
                {
                    result = target;
                }
                else
                {
                    result = current + a / magnitude * maxDistanceDelta;
                }
                return result;
            }

            // Token: 0x06001546 RID: 5446 RVA: 0x00022BF0 File Offset: 0x00020DF0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 Scale(Vector4 a, Vector4 b)
            {
                Vector4 res;
                res.x = a.x * b.x;
                res.y = a.y * b.y;
                res.z = a.z * b.z;
                res.w = a.w * b.w;
                return res;
            }

            // Token: 0x06001547 RID: 5447 RVA: 0x00022C48 File Offset: 0x00020E48
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Scale(Vector4 scale)
            {
                this.x *= scale.x;
                this.y *= scale.y;
                this.z *= scale.z;
                this.w *= scale.w;
            }

            // Token: 0x06001548 RID: 5448 RVA: 0x00022CA8 File Offset: 0x00020EA8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
            }

            // Token: 0x06001549 RID: 5449 RVA: 0x00022D0C File Offset: 0x00020F0C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Vector4 && this.Equals((Vector4)other);
            }

            // Token: 0x0600154A RID: 5450 RVA: 0x00022D40 File Offset: 0x00020F40
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Vector4 other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
            }

            // Token: 0x0600154B RID: 5451 RVA: 0x00022DB0 File Offset: 0x00020FB0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 Normalize(Vector4 a)
            {
                float num = Vector4.Magnitude(a);
                Vector4 result;
                if (num > 1E-05f)
                {
                    float recip = 1.0f / num;
                    result.x = a.x * recip;
                    result.y = a.y * recip;
                    result.z = a.z * recip;
                    result.w = a.w * recip;
                }
                else
                {
                    result.x = 0.0f;
                    result.y = 0.0f;
                    result.z = 0.0f;
                    result.w = 0.0f;
                }
                return result;
            }

            // Token: 0x0600154C RID: 5452 RVA: 0x00022DEC File Offset: 0x00020FEC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Normalize()
            {
                float num = Vector4.Magnitude(this);
                if (num > 1E-05f)
                {
                    float recip = 1.0f / num;
                    this.x *= recip;
                    this.y *= recip;
                    this.z *= recip;
                    this.w *= recip;
                }
                else
                {
                    this.x = 0f;
                    this.y = 0f;
                    this.z = 0f;
                    this.w = 0f;
                }
            }

            // Token: 0x17000432 RID: 1074
            // (get) Token: 0x0600154D RID: 5453 RVA: 0x00022E34 File Offset: 0x00021034
            public Vector4 normalized
            {
                get
                {
                    return Vector4.Normalize(this);
                }
            }

            // Token: 0x0600154E RID: 5454 RVA: 0x00022E54 File Offset: 0x00021054
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Dot(Vector4 a, Vector4 b)
            {
                return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            }

            // Token: 0x0600154F RID: 5455 RVA: 0x00022EA8 File Offset: 0x000210A8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 Project(Vector4 a, Vector4 b)
            {
                return b * Vector4.Dot(a, b) / Vector4.Dot(b, b);
            }

            // Token: 0x06001550 RID: 5456 RVA: 0x00022ED8 File Offset: 0x000210D8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Vector4 a, Vector4 b)
            {
                return Vector4.Magnitude(a - b);
            }

            // Token: 0x06001551 RID: 5457 RVA: 0x00022EFC File Offset: 0x000210FC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Magnitude(Vector4 a)
            {
                return (float)Math.Sqrt(Vector4.Dot(a, a));
            }

            // Token: 0x17000433 RID: 1075
            // (get) Token: 0x06001552 RID: 5458 RVA: 0x00022F20 File Offset: 0x00021120
            public float magnitude
            {
                get
                {
                    return (float)Math.Sqrt(Vector4.Dot(this, this));
                }
            }

            // Token: 0x17000434 RID: 1076
            // (get) Token: 0x06001553 RID: 5459 RVA: 0x00022F4C File Offset: 0x0002114C
            public float sqrMagnitude
            {
                get
                {
                    return Vector4.Dot(this, this);
                }
            }

            // Token: 0x06001554 RID: 5460 RVA: 0x00022F74 File Offset: 0x00021174
            public static Vector4 Min(Vector4 lhs, Vector4 rhs)
            {
                lhs.x = Math.Min(lhs.x, rhs.x);
                lhs.y = Math.Min(lhs.y, rhs.y);
                lhs.z = Math.Min(lhs.z, rhs.z);
                lhs.w = Math.Min(lhs.w, rhs.w);
                return lhs;
            }

            // Token: 0x06001555 RID: 5461 RVA: 0x00022FDC File Offset: 0x000211DC
            public static Vector4 Max(Vector4 lhs, Vector4 rhs)
            {
                lhs.x = Math.Max(lhs.x, rhs.x);
                lhs.y = Math.Max(lhs.y, rhs.y);
                lhs.z = Math.Max(lhs.z, rhs.z);
                lhs.w = Math.Max(lhs.w, rhs.w);
                return lhs;
            }

            // Token: 0x17000435 RID: 1077
            // (get) Token: 0x06001556 RID: 5462 RVA: 0x00023044 File Offset: 0x00021244
            public static Vector4 zero
            {
                get
                {
                    return Vector4.zeroVector;
                }
            }

            // Token: 0x17000436 RID: 1078
            // (get) Token: 0x06001557 RID: 5463 RVA: 0x00023060 File Offset: 0x00021260
            public static Vector4 one
            {
                get
                {
                    return Vector4.oneVector;
                }
            }

            // Token: 0x17000437 RID: 1079
            // (get) Token: 0x06001558 RID: 5464 RVA: 0x0002307C File Offset: 0x0002127C
            public static Vector4 positiveInfinity
            {
                get
                {
                    return Vector4.positiveInfinityVector;
                }
            }

            // Token: 0x17000438 RID: 1080
            // (get) Token: 0x06001559 RID: 5465 RVA: 0x00023098 File Offset: 0x00021298
            public static Vector4 negativeInfinity
            {
                get
                {
                    return Vector4.negativeInfinityVector;
                }
            }

            // Token: 0x0600155A RID: 5466 RVA: 0x000230B4 File Offset: 0x000212B4
            public static Vector4 operator +(Vector4 a, Vector4 b)
            {
                a.x += b.x;
                a.y += b.y;
                a.z += b.z;
                a.w += b.w;
                return a;
            }

            // Token: 0x0600155B RID: 5467 RVA: 0x0002310C File Offset: 0x0002130C
            public static Vector4 operator -(Vector4 a, Vector4 b)
            {
                a.x -= b.x;
                a.y -= b.y;
                a.z -= b.z;
                a.w -= b.w;
                return a;
            }

            // Token: 0x0600155C RID: 5468 RVA: 0x00023164 File Offset: 0x00021364
            public static Vector4 operator -(Vector4 a)
            {
                a.x = -a.x;
                a.y = -a.y;
                a.z = -a.z;
                a.w = -a.w;
                return a;
            }

            // Token: 0x0600155D RID: 5469 RVA: 0x000231A0 File Offset: 0x000213A0
            public static Vector4 operator *(Vector4 a, float d)
            {
                a.x *= d;
                a.y *= d;
                a.z *= d;
                a.w *= d;
                return a;
            }

            // Token: 0x0600155E RID: 5470 RVA: 0x000231E0 File Offset: 0x000213E0
            public static Vector4 operator *(float d, Vector4 a)
            {
                a.x *= d;
                a.y *= d;
                a.z *= d;
                a.w *= d;
                return a;
            }

            // Token: 0x0600155F RID: 5471 RVA: 0x00023220 File Offset: 0x00021420
            public static Vector4 operator /(Vector4 a, float d)
            {
                float recip = 1.0f / d;
                a.x *= recip;
                a.y *= recip;
                a.z *= recip;
                a.w *= recip;
                return a;
            }

            // Token: 0x06001560 RID: 5472 RVA: 0x00023260 File Offset: 0x00021460
            public static bool operator ==(Vector4 lhs, Vector4 rhs)
            {
                return Vector4.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
            }

            // Token: 0x06001561 RID: 5473 RVA: 0x00023288 File Offset: 0x00021488
            public static bool operator !=(Vector4 lhs, Vector4 rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06001562 RID: 5474 RVA: 0x000232A8 File Offset: 0x000214A8
            public static implicit operator Vector4(Vector3 v)
            {
                Vector4 res;
                res.x = v.x;
                res.y = v.y;
                res.z = v.z;
                res.w = 0f;
                return res;
            }

            // Token: 0x06001563 RID: 5475 RVA: 0x000232DC File Offset: 0x000214DC
            public static implicit operator Vector3(Vector4 v)
            {
                Vector3 res;
                res.x = v.x;
                res.y = v.y;
                res.z = v.z;
                return res;
            }

            // Token: 0x06001564 RID: 5476 RVA: 0x0002330C File Offset: 0x0002150C
            public static implicit operator Vector4(Vector2 v)
            {
                Vector4 res;
                res.x = v.x;
                res.y = v.y;
                res.z = 0f;
                res.w = 0f;
                return res;
            }

            // Token: 0x06001565 RID: 5477 RVA: 0x00023340 File Offset: 0x00021540
            public static implicit operator Vector2(Vector4 v)
            {
                Vector2 res;
                res.x = v.x;
                res.y = v.y;
                return res;
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

            // Token: 0x06001568 RID: 5480 RVA: 0x0002342C File Offset: 0x0002162C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float SqrMagnitude(Vector4 a)
            {
                return Vector4.Dot(a, a);
            }

            // Token: 0x06001569 RID: 5481 RVA: 0x00023448 File Offset: 0x00021648
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float SqrMagnitude()
            {
                return Vector4.Dot(this, this);
            }

            // Token: 0x0600156A RID: 5482 RVA: 0x00023470 File Offset: 0x00021670
            // Note: this type is marked as 'beforefieldinit'.
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Vector4()
            {
            }

            // Token: 0x04000803 RID: 2051
            public const float kEpsilon = 1E-05f;

            // Token: 0x04000804 RID: 2052
            public float x;

            // Token: 0x04000805 RID: 2053
            public float y;

            // Token: 0x04000806 RID: 2054
            public float z;

            // Token: 0x04000807 RID: 2055
            public float w;

            // Token: 0x04000808 RID: 2056
            private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

            // Token: 0x04000809 RID: 2057
            private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

            // Token: 0x0400080A RID: 2058
            private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

            // Token: 0x0400080B RID: 2059
            private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        }
    }

}