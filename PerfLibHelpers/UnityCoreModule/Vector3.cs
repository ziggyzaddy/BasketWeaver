using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Internal;

namespace Inject
{
    namespace UnityEngine
    {
        // New Function
        public struct UserVector
        {
            public int x; 
            public int y;
            public int z;
        }

        // Token: 0x02000141 RID: 321
        public struct Vector3 : IEquatable<Vector3>
        {

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void AssignTest(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            // Token: 0x06000CA1 RID: 3233
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            // Token: 0x06000CA2 RID: 3234
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3(float x, float y)
            {
                this.x = x;
                this.y = y;
                this.z = 0f;
            }

            // Token: 0x06000CA3 RID: 3235
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
            {
                Vector3 result;
                Vector3.Slerp_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CA4 RID: 3236
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
            {
                Vector3 result;
                Vector3.SlerpUnclamped_Injected(ref a, ref b, t, out result);
                return result;
            }

            // Token: 0x06000CA5 RID: 3237
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

            // Token: 0x06000CA6 RID: 3238
            public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
            {
                Vector3.OrthoNormalize2(ref normal, ref tangent);
            }

            // Token: 0x06000CA7 RID: 3239
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

            // Token: 0x06000CA8 RID: 3240
            public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
            {
                Vector3.OrthoNormalize3(ref normal, ref tangent, ref binormal);
            }

            // Token: 0x06000CA9 RID: 3241
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
            {
                Vector3 result;
                Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out result);
                return result;
            }

            // Token: 0x06000CAA RID: 3242
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
            {
                t = Mathf.Clamp01(t);
                a.x = a.x + (b.x - a.x) * t;
                a.y = a.y + (b.y - a.y) * t;
                a.z = a.z + (b.z - a.z) * t;
                return a;
            }

            // Token: 0x06000CAB RID: 3243
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
            {
                Vector3 res;
                res.x = a.x + (b.x - a.x) * t;
                res.y = a.y + (b.y - a.y) * t;
                res.z = a.z + (b.z - a.z) * t;
                return res;
            }

            // Token: 0x06000CAC RID: 3244
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
            {
                Vector3 a = target - current;
                float magnitude = a.magnitude;
                Vector3 result;
                if (magnitude <= maxDistanceDelta || magnitude < 1E-45f)
                {
                    result = target;
                }
                else
                {
                    result = current + a / magnitude * maxDistanceDelta;
                }
                return result;
            }

            // Token: 0x06000CAD RID: 3245
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
            {
                float deltaTime = Time.deltaTime;
                return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
            }

            // Token: 0x06000CAE RID: 3246
            [ExcludeFromDocs]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
            {
                float deltaTime = Time.deltaTime;
                float positiveInfinity = float.PositiveInfinity;
                return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
            }

            // Token: 0x06000CAF RID: 3247
            public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
            {
                smoothTime = Math.Max(0.0001f, smoothTime);
                float num = 2f / smoothTime;
                float num2 = num * deltaTime;
                float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
                Vector3 vector = current - target;
                Vector3 vector2 = target;
                float maxLength = maxSpeed * smoothTime;
                vector = Vector3.ClampMagnitude(vector, maxLength);
                target = current - vector;
                Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
                currentVelocity = (currentVelocity - num * vector3) * d;
                Vector3 vector4 = target + (vector + vector3) * d;
                if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
                {
                    vector4 = vector2;
                    currentVelocity = (vector4 - vector2) / deltaTime;
                }
                return vector4;
            }

            // Token: 0x170002A1 RID: 673
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
                        default:
                            throw new IndexOutOfRangeException("Invalid Vector3 index!");
                    }
                    return result;
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            this.x = value;
                            return;
                        case 1:
                            this.y = value;
                            return;
                        case 2:
                            this.z = value;
                            return;
                        default:
                            throw new IndexOutOfRangeException("Invalid Vector3 index!");
                    }
                }
            }

            // Token: 0x06000CB2 RID: 3250
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set(float newX, float newY, float newZ)
            {
                this.x = newX;
                this.y = newY;
                this.z = newZ;
            }

            // Token: 0x06000CB3 RID: 3251
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Scale(Vector3 a, Vector3 b)
            {
                a.x = a.x * b.x;
                a.y = a.y * b.y;
                a.z = a.z * b.z;
                return a;
            }

            // Token: 0x06000CB4 RID: 3252
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Scale(Vector3 scale)
            {
                this.x *= scale.x;
                this.y *= scale.y;
                this.z *= scale.z;
            }

            // Token: 0x06000CB5 RID: 3253
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
            {
                Vector3 res;
                res.x = lhs.y * rhs.z - lhs.z * rhs.y;
                res.y = lhs.z * rhs.x - lhs.x * rhs.z;
                res.z = lhs.x * rhs.y - lhs.y * rhs.x;
                return res;
            }

            // Token: 0x06000CB6 RID: 3254
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
            }

            // Token: 0x06000CB7 RID: 3255
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Vector3 && this.Equals((Vector3)other);
            }

            // Token: 0x06000CB8 RID: 3256
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Vector3 other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z);
            }

            // Token: 0x06000CB9 RID: 3257

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
            {
                return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
            }

            // Token: 0x06000CBA RID: 3258
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Normalize(Vector3 value)
            {

                float num = Vector3.Magnitude(value);
                Vector3 result;
                if (num > 1E-05f)
                {
                    float recip = 1.0f / num;
                    result.x = value.x * recip;
                    result.y = value.y * recip;
                    result.z = value.z * recip;
                }
                else
                {
                    result.x = 0.0f;
                    result.y = 0.0f;
                    result.z = 0.0f;
                }
                return result;
            }

            // Token: 0x06000CBB RID: 3259
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Normalize()
            {

                float num = Vector3.Magnitude(this);
                if (num > 1E-05f)
                {
                    float recip = 1.0f / num;
                    this.x *= recip;
                    this.y *= recip;
                    this.z *= recip;
                }
                else
                {
                    this.x = 0f;
                    this.y = 0f;
                    this.z = 0f;
                }

            }

            // Token: 0x170002A2 RID: 674
            // (get) Token: 0x06000CBC RID: 3260
            public Vector3 normalized
            {
                get
                {
                    return Vector3.Normalize(this);
                }
            }

            // Token: 0x06000CBD RID: 3261
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Dot(Vector3 lhs, Vector3 rhs)
            {
                return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
            }

            // Token: 0x06000CBE RID: 3262
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Project(Vector3 vector, Vector3 onNormal)
            {
                float num = Vector3.Dot(onNormal, onNormal);
                Vector3 result;
                if (num < Mathf.Epsilon)
                {
                    result = Vector3.zero;
                }
                else
                {
                    result = onNormal * Vector3.Dot(vector, onNormal) / num;
                }
                return result;
            }

            // Token: 0x06000CBF RID: 3263
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
            {
                return vector - Vector3.Project(vector, planeNormal);
            }

            // Token: 0x06000CC0 RID: 3264
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Angle(Vector3 from, Vector3 to)
            {
                float num = (float)Math.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
                float result;
                if (num < 1E-15f)
                {
                    result = 0f;
                }
                else
                {
                    result = (float)Math.Acos(Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f)) * 57.29578f;
                }
                return result;
            }

            // Token: 0x06000CC1 RID: 3265
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
            {
                float num3 = Vector3.Angle(from, to);
                float num2 = Math.Sign(Vector3.Dot(axis, Vector3.Cross(from, to)));
                return num3 * num2;
            }

            // Token: 0x06000CC2 RID: 3266
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Vector3 a, Vector3 b)
            {
                a.x = a.x - b.x;
                a.y = a.y - b.y;
                a.z = a.z - b.z;
                return (float)Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
            }

            // Token: 0x06000CC3 RID: 3267
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
            {
                Vector3 result;
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

            // Token: 0x06000CC4 RID: 3268
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Magnitude(Vector3 vector)
            {
                return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
            }

            // Token: 0x170002A3 RID: 675
            // (get) Token: 0x06000CC5 RID: 3269
            public float magnitude
            {
                get
                {
                    return (float)Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
                }
            }

            // Token: 0x06000CC6 RID: 3270
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float SqrMagnitude(Vector3 vector)
            {
                return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
            }

            // Token: 0x170002A4 RID: 676
            // (get) Token: 0x06000CC7 RID: 3271
            public float sqrMagnitude
            {
                get
                {
                    return this.x * this.x + this.y * this.y + this.z * this.z;
                }
            }

            // Token: 0x06000CC8 RID: 3272
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Min(Vector3 lhs, Vector3 rhs)
            {
                lhs.x = Math.Min(lhs.x, rhs.x);
                lhs.y = Math.Min(lhs.y, rhs.y);
                lhs.z = Math.Min(lhs.z, rhs.z);
                return lhs;
            }

            // Token: 0x06000CC9 RID: 3273
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 Max(Vector3 lhs, Vector3 rhs)
            {
                lhs.x = Math.Max(lhs.x, rhs.x);
                lhs.y = Math.Max(lhs.y, rhs.y);
                lhs.z = Math.Max(lhs.z, rhs.z);
                return lhs;
            }

            // Token: 0x170002A5 RID: 677
            // (get) Token: 0x06000CCA RID: 3274
            public static Vector3 zero
            {
                get
                {
                    return Vector3.zeroVector;
                }
            }

            // Token: 0x170002A6 RID: 678
            // (get) Token: 0x06000CCB RID: 3275
            public static Vector3 one
            {
                get
                {
                    return Vector3.oneVector;
                }
            }

            // Token: 0x170002A7 RID: 679
            // (get) Token: 0x06000CCC RID: 3276
            public static Vector3 forward
            {
                get
                {
                    return Vector3.forwardVector;
                }
            }

            // Token: 0x170002A8 RID: 680
            // (get) Token: 0x06000CCD RID: 3277
            public static Vector3 back
            {
                get
                {
                    return Vector3.backVector;
                }
            }

            // Token: 0x170002A9 RID: 681
            // (get) Token: 0x06000CCE RID: 3278
            public static Vector3 up
            {
                get
                {
                    return Vector3.upVector;
                }
            }

            // Token: 0x170002AA RID: 682
            // (get) Token: 0x06000CCF RID: 3279
            public static Vector3 down
            {
                get
                {
                    return Vector3.downVector;
                }
            }

            // Token: 0x170002AB RID: 683
            // (get) Token: 0x06000CD0 RID: 3280
            public static Vector3 left
            {
                get
                {
                    return Vector3.leftVector;
                }
            }

            // Token: 0x170002AC RID: 684
            // (get) Token: 0x06000CD1 RID: 3281
            public static Vector3 right
            {
                get
                {
                    return Vector3.rightVector;
                }
            }

            // Token: 0x170002AD RID: 685
            // (get) Token: 0x06000CD2 RID: 3282
            public static Vector3 positiveInfinity
            {
                get
                {
                    return Vector3.positiveInfinityVector;
                }
            }

            // Token: 0x170002AE RID: 686
            // (get) Token: 0x06000CD3 RID: 3283
            public static Vector3 negativeInfinity
            {
                get
                {
                    return Vector3.negativeInfinityVector;
                }
            }

            // Token: 0x06000CD4 RID: 3284
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator +(Vector3 a, Vector3 b)
            {
                a.x += b.x;
                a.y += b.y;
                a.z += b.z;
                return a;
            }

            // Token: 0x06000CD5 RID: 3285
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator -(Vector3 a, Vector3 b)
            {
                a.x -= b.x;
                a.y -= b.y;
                a.z -= b.z;
                return a;
            }

            // Token: 0x06000CD6 RID: 3286
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator -(Vector3 a)
            {
                a.x = -a.x;
                a.y = -a.y;
                a.z = -a.z;
                return a;
            }

            // Token: 0x06000CD7 RID: 3287
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator *(Vector3 a, float d)
            {
                a.x *= d;
                a.y *= d;
                a.z *= d;
                return a;
            }

            // Token: 0x06000CD8 RID: 3288
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator *(float d, Vector3 a)
            {
                a.x *= d;
                a.y *= d;
                a.z *= d;
                return a;
            }

            // Token: 0x06000CD9 RID: 3289
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3 operator /(Vector3 a, float d)
            {
                float recip = 1.0f / d;
                a.x *= recip;
                a.y *= recip;
                a.z *= recip;
                return a;
            }

            // Token: 0x06000CDA RID: 3290
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Vector3 lhs, Vector3 rhs)
            {
                return Vector3.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
            }

            // Token: 0x06000CDB RID: 3291
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Vector3 lhs, Vector3 rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06000CDC RID: 3292
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
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x170002AF RID: 687
            // (get) Token: 0x06000CDE RID: 3294
            [Obsolete("Use Vector3.forward instead.")]
            public static Vector3 fwd
            {
                get
                {
                    Vector3 res;
                    res.x = 0f;
                    res.y = 0f;
                    res.z = 1f;
                    return res;
                }
            }

            // Token: 0x06000CDF RID: 3295
            [Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float AngleBetween(Vector3 from, Vector3 to)
            {
                return (float)Math.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
            }

            // Token: 0x06000CE0 RID: 3296
            [Obsolete("Use Vector3.ProjectOnPlane instead.")]
            public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
            {
                return Vector3.ProjectOnPlane(fromThat, excludeThis);
            }

            // Token: 0x06000CE1 RID: 3297
            static Vector3()
            {
            }

            // Token: 0x06000CE2 RID: 3298
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

            // Token: 0x06000CE3 RID: 3299
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

            // Token: 0x06000CE4 RID: 3300
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);

            // Token: 0x040006A7 RID: 1703
            public const float kEpsilon = 1E-05f;

            // Token: 0x040006A8 RID: 1704
            public const float kEpsilonNormalSqrt = 1E-15f;

            // Token: 0x040006A9 RID: 1705
            public float x;

            // Token: 0x040006AA RID: 1706
            public float y;

            // Token: 0x040006AB RID: 1707
            public float z;

            // Token: 0x040006AC RID: 1708
            private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

            // Token: 0x040006AD RID: 1709
            private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

            // Token: 0x040006AE RID: 1710
            private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

            // Token: 0x040006AF RID: 1711
            private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

            // Token: 0x040006B0 RID: 1712
            private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

            // Token: 0x040006B1 RID: 1713
            private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

            // Token: 0x040006B2 RID: 1714
            private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

            // Token: 0x040006B3 RID: 1715
            private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

            // Token: 0x040006B4 RID: 1716
            private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

            // Token: 0x040006B5 RID: 1717
            private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        }
    }

}