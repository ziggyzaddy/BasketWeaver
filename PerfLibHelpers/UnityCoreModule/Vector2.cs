using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Internal;


namespace Inject
{
    namespace UnityEngine
    {
        // Token: 0x0200024A RID: 586
        public struct Vector2 : IEquatable<Vector2>
        {
            // Token: 0x060014B9 RID: 5305 RVA: 0x00020DD0 File Offset: 0x0001EFD0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            // Token: 0x1700040E RID: 1038
            public float this[int index]
            {
                get
                {
                    float result;
                    if (index != 0)
                    {
                        if (index != 1)
                        {
                            throw new IndexOutOfRangeException("Invalid Vector2 index!");
                        }
                        result = this.y;
                    }
                    else
                    {
                        result = this.x;
                    }
                    return result;
                }
                set
                {
                    if (index != 0)
                    {
                        if (index != 1)
                        {
                            throw new IndexOutOfRangeException("Invalid Vector2 index!");
                        }
                        this.y = value;
                    }
                    else
                    {
                        this.x = value;
                    }
                }
            }

            // Token: 0x060014BC RID: 5308 RVA: 0x00020DD0 File Offset: 0x0001EFD0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set(float newX, float newY)
            {
                this.x = newX;
                this.y = newY;
            }

            // Token: 0x060014BD RID: 5309 RVA: 0x00020E60 File Offset: 0x0001F060
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
            {
                t = Mathf.Clamp01(t);
                Vector2 res;
                res.x = a.x + (b.x - a.x) * t;
                res.y = a.y + (b.y - a.y) * t;
                return res;
            }

            // Token: 0x060014BE RID: 5310 RVA: 0x00020EB4 File Offset: 0x0001F0B4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
            {
                Vector2 res;
                res.x = a.x + (b.x - a.x) * t;
                res.y = a.y + (b.y - a.y) * t;
                return res;
            }

            // Token: 0x060014BF RID: 5311 RVA: 0x00020F00 File Offset: 0x0001F100
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
            {
                Vector2 a = target - current;
                float magnitude = a.magnitude;
                Vector2 result;
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

            // Token: 0x060014C0 RID: 5312 RVA: 0x00020F54 File Offset: 0x0001F154
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 Scale(Vector2 a, Vector2 b)
            {
                a.x = a.x * b.x;
                a.y = a.y * b.y;
                return a;
            }

            // Token: 0x060014C1 RID: 5313 RVA: 0x00020F8C File Offset: 0x0001F18C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Scale(Vector2 scale)
            {
                this.x *= scale.x;
                this.y *= scale.y;
            }

            // Token: 0x060014C2 RID: 5314 RVA: 0x00020FB8 File Offset: 0x0001F1B8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Normalize()
            {
                float magnitude = this.magnitude;
                if (magnitude > 1E-05f)
                {
                    float recip = 1.0f / magnitude;
                    this.x *= recip;
                    this.y *= recip;
                }
                else
                {
                    this.x = 0f;
                    this.y = 0f;
                }
            }

            // Token: 0x1700040F RID: 1039
            // (get) Token: 0x060014C3 RID: 5315 RVA: 0x00020FFC File Offset: 0x0001F1FC
            public Vector2 normalized
            {
                get
                {
                    Vector2 result;

                    float magnitude = this.magnitude;
                    if (magnitude > 1E-05f)
                    {
                        float recip = 1.0f / magnitude;
                        result.x = this.x * recip;
                        result.y = this.y * recip;
                    }
                    else
                    {
                        result.x = 0f;
                        result.y = 0f;
                    }
                    return result;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(x.ToString("F1"));
                stringBuilder.Append(", ");
                stringBuilder.Append(y.ToString("F1"));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x06000CDD RID: 3293
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ToString(string format)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(x.ToString(format));
                stringBuilder.Append(", ");
                stringBuilder.Append(y.ToString(format));
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }


            // Token: 0x060014C6 RID: 5318 RVA: 0x000210B4 File Offset: 0x0001F2B4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
            }

            // Token: 0x060014C7 RID: 5319 RVA: 0x000210F0 File Offset: 0x0001F2F0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Vector2 && this.Equals((Vector2)other);
            }

            // Token: 0x060014C8 RID: 5320 RVA: 0x00021124 File Offset: 0x0001F324
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Vector2 other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y);
            }

            // Token: 0x060014C9 RID: 5321 RVA: 0x00021168 File Offset: 0x0001F368
            public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
            {
                return -2f * Vector2.Dot(inNormal, inDirection) * inNormal + inDirection;
            }

            // Token: 0x060014CA RID: 5322 RVA: 0x00021198 File Offset: 0x0001F398
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 Perpendicular(Vector2 inDirection)
            {
                Vector2 res;
                res.x = -inDirection.y;
                res.y = inDirection.x;
                return res;
            }

            // Token: 0x060014CB RID: 5323 RVA: 0x000211C4 File Offset: 0x0001F3C4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Dot(Vector2 lhs, Vector2 rhs)
            {
                return lhs.x * rhs.x + lhs.y * rhs.y;
            }

            // Token: 0x17000410 RID: 1040
            // (get) Token: 0x060014CC RID: 5324 RVA: 0x000211F8 File Offset: 0x0001F3F8
            public float magnitude
            {
                get
                {
                    return (float)Math.Sqrt(this.x * this.x + this.y * this.y);
                }
            }

            // Token: 0x17000411 RID: 1041
            // (get) Token: 0x060014CD RID: 5325 RVA: 0x00021230 File Offset: 0x0001F430
            public float sqrMagnitude
            {
                get
                {
                    return this.x * this.x + this.y * this.y;
                }
            }

            // Token: 0x060014CE RID: 5326 RVA: 0x00021260 File Offset: 0x0001F460
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Angle(Vector2 from, Vector2 to)
            {
                float num = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
                float result;
                if (num < 1E-15f)
                {
                    result = 0f;
                }
                else
                {
                    float f = Mathf.Clamp(Vector2.Dot(from, to) / num, -1f, 1f);
                    result = (float)System.Math.Acos(f) * 57.29578f;
                }
                return result;
            }

            // Token: 0x060014CF RID: 5327 RVA: 0x000212C8 File Offset: 0x0001F4C8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float SignedAngle(Vector2 from, Vector2 to)
            {
                float num = Vector2.Angle(from, to);
                float num2 = (float)Math.Sqrt(from.x * to.y - from.y * to.x);
                return num * num2;
            }

            // Token: 0x060014D0 RID: 5328 RVA: 0x00021310 File Offset: 0x0001F510
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Vector2 a, Vector2 b)
            {
                a.x -= b.x;
                a.y -= b.y;
                return (float)Math.Sqrt(a.x * a.x + a.y * a.y);
            }

            // Token: 0x060014D1 RID: 5329 RVA: 0x00021334 File Offset: 0x0001F534
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
            {
                Vector2 result;
                if (vector.sqrMagnitude > maxLength * maxLength)
                {
                    result = vector.normalized * maxLength;
                }
                else
                {
                    result = vector;
                }
                return result;
            }

            // Token: 0x060014D2 RID: 5330 RVA: 0x0002136C File Offset: 0x0001F56C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float SqrMagnitude(Vector2 a)
            {
                return a.x * a.x + a.y * a.y;
            }

            // Token: 0x060014D3 RID: 5331 RVA: 0x000213A0 File Offset: 0x0001F5A0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public float SqrMagnitude()
            {
                return this.x * this.x + this.y * this.y;
            }

            // Token: 0x060014D4 RID: 5332 RVA: 0x000213D0 File Offset: 0x0001F5D0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 Min(Vector2 lhs, Vector2 rhs)
            {
                lhs.x = Math.Min(lhs.x, rhs.x);
                lhs.y = Math.Min(lhs.y, rhs.y);
                return lhs;
            }

            // Token: 0x060014D5 RID: 5333 RVA: 0x00021410 File Offset: 0x0001F610
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 Max(Vector2 lhs, Vector2 rhs)
            {
                lhs.x = Math.Max(lhs.x, rhs.x);
                lhs.y = Math.Max(lhs.y, rhs.y);
                return lhs;
            }

            // Token: 0x060014D6 RID: 5334 RVA: 0x00021450 File Offset: 0x0001F650
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
            {
                return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, Time.deltaTime);
            }

            // Token: 0x060014D7 RID: 5335 RVA: 0x00021478 File Offset: 0x0001F678
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
            {
                return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, float.PositiveInfinity, Time.deltaTime);
            }

            // Token: 0x060014D8 RID: 5336 RVA: 0x000214A4 File Offset: 0x0001F6A4
            public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
            {
                smoothTime = Math.Max(0.0001f, smoothTime);
                float num = 2f / smoothTime;
                float num2 = num * deltaTime;
                float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
                Vector2 vector = current - target;
                Vector2 vector2 = target;
                float maxLength = maxSpeed * smoothTime;
                vector = Vector2.ClampMagnitude(vector, maxLength);
                target = current - vector;
                Vector2 vector3 = (currentVelocity + num * vector) * deltaTime;
                currentVelocity = (currentVelocity - num * vector3) * d;
                Vector2 vector4 = target + (vector + vector3) * d;
                if (Vector2.Dot(vector2 - current, vector4 - vector2) > 0f)
                {
                    vector4 = vector2;
                    currentVelocity = (vector4 - vector2) / deltaTime;
                }
                return vector4;
            }

            // Token: 0x060014D9 RID: 5337 RVA: 0x000215AC File Offset: 0x0001F7AC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator +(Vector2 a, Vector2 b)
            {
                a.x = a.x + b.x;
                a.y = a.y + b.y;
                return a;
            }

            // Token: 0x060014DA RID: 5338 RVA: 0x000215E4 File Offset: 0x0001F7E4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator -(Vector2 a, Vector2 b)
            {
                a.x = a.x - b.x;
                a.y = a.y - b.y;
                return a;
            }

            // Token: 0x060014DB RID: 5339 RVA: 0x0002161C File Offset: 0x0001F81C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator *(Vector2 a, Vector2 b)
            {
                a.x = a.x * b.x;
                a.y = a.y * b.y;
                return a;
            }

            // Token: 0x060014DC RID: 5340 RVA: 0x00021654 File Offset: 0x0001F854
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator /(Vector2 a, Vector2 b)
            {

                a.x /= b.x;
                a.y /= b.y;
                return a;
            }

            // Token: 0x060014DD RID: 5341 RVA: 0x0002168C File Offset: 0x0001F88C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator -(Vector2 a)
            {
                a.x = -a.x;
                a.y = -a.y;
                return a;
            }

            // Token: 0x060014DE RID: 5342 RVA: 0x000216B8 File Offset: 0x0001F8B8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator *(Vector2 a, float d)
            {

                Vector2 res;
                res.x = a.x * d;
                res.y = a.y * d;
                return res;
            }

            // Token: 0x060014DF RID: 5343 RVA: 0x000216E4 File Offset: 0x0001F8E4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator *(float d, Vector2 a)
            {

                a.x = a.x * d;
                a.y = a.y * d;
                return a;
            }

            // Token: 0x060014E0 RID: 5344 RVA: 0x00021710 File Offset: 0x0001F910
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2 operator /(Vector2 a, float d)
            {
                float recip = 1.0f / d;
                a.x *= recip;
                a.y *= recip;
                return a;
            }

            // Token: 0x060014E1 RID: 5345 RVA: 0x0002173C File Offset: 0x0001F93C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Vector2 lhs, Vector2 rhs)
            {
                return (lhs - rhs).sqrMagnitude < 9.9999994E-11f;
            }

            // Token: 0x060014E2 RID: 5346 RVA: 0x00021768 File Offset: 0x0001F968
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Vector2 lhs, Vector2 rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x060014E3 RID: 5347 RVA: 0x00021788 File Offset: 0x0001F988
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Vector2(Vector3 v)
            {
                Vector2 res;
                res.x = v.x;
                res.y = v.y;
                return res;
            }

            // Token: 0x060014E4 RID: 5348 RVA: 0x000217B0 File Offset: 0x0001F9B0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Vector3(Vector2 v)
            {
                Vector3 res;
                res.x = v.x;
                res.y = v.y;
                res.z = 0f;
                return res;
            }

            // Token: 0x17000412 RID: 1042
            // (get) Token: 0x060014E5 RID: 5349 RVA: 0x000217E0 File Offset: 0x0001F9E0
            public static Vector2 zero
            {
                get
                {
                    return Vector2.zeroVector;
                }
            }

            // Token: 0x17000413 RID: 1043
            // (get) Token: 0x060014E6 RID: 5350 RVA: 0x000217FC File Offset: 0x0001F9FC
            public static Vector2 one
            {
                get
                {
                    return Vector2.oneVector;
                }
            }

            // Token: 0x17000414 RID: 1044
            // (get) Token: 0x060014E7 RID: 5351 RVA: 0x00021818 File Offset: 0x0001FA18
            public static Vector2 up
            {
                get
                {
                    return Vector2.upVector;
                }
            }

            // Token: 0x17000415 RID: 1045
            // (get) Token: 0x060014E8 RID: 5352 RVA: 0x00021834 File Offset: 0x0001FA34
            public static Vector2 down
            {
                get
                {
                    return Vector2.downVector;
                }
            }

            // Token: 0x17000416 RID: 1046
            // (get) Token: 0x060014E9 RID: 5353 RVA: 0x00021850 File Offset: 0x0001FA50
            public static Vector2 left
            {
                get
                {
                    return Vector2.leftVector;
                }
            }

            // Token: 0x17000417 RID: 1047
            // (get) Token: 0x060014EA RID: 5354 RVA: 0x0002186C File Offset: 0x0001FA6C
            public static Vector2 right
            {
                get
                {
                    return Vector2.rightVector;
                }
            }

            // Token: 0x17000418 RID: 1048
            // (get) Token: 0x060014EB RID: 5355 RVA: 0x00021888 File Offset: 0x0001FA88
            public static Vector2 positiveInfinity
            {
                get
                {
                    return Vector2.positiveInfinityVector;
                }
            }

            // Token: 0x17000419 RID: 1049
            // (get) Token: 0x060014EC RID: 5356 RVA: 0x000218A4 File Offset: 0x0001FAA4
            public static Vector2 negativeInfinity
            {
                get
                {
                    return Vector2.negativeInfinityVector;
                }
            }

            // Token: 0x060014ED RID: 5357 RVA: 0x000218C0 File Offset: 0x0001FAC0
            // Note: this type is marked as 'beforefieldinit'.
            static Vector2()
            {
            }

            // Token: 0x040007E6 RID: 2022
            public float x;

            // Token: 0x040007E7 RID: 2023
            public float y;

            // Token: 0x040007E8 RID: 2024
            private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

            // Token: 0x040007E9 RID: 2025
            private static readonly Vector2 oneVector = new Vector2(1f, 1f);

            // Token: 0x040007EA RID: 2026
            private static readonly Vector2 upVector = new Vector2(0f, 1f);

            // Token: 0x040007EB RID: 2027
            private static readonly Vector2 downVector = new Vector2(0f, -1f);

            // Token: 0x040007EC RID: 2028
            private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

            // Token: 0x040007ED RID: 2029
            private static readonly Vector2 rightVector = new Vector2(1f, 0f);

            // Token: 0x040007EE RID: 2030
            private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

            // Token: 0x040007EF RID: 2031
            private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

            // Token: 0x040007F0 RID: 2032
            public const float kEpsilon = 1E-05f;

            // Token: 0x040007F1 RID: 2033
            public const float kEpsilonNormalSqrt = 1E-15f;

        }
    }
}