using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Inject
{


    namespace UnityEngine
    {

        public struct Matrix4x4 : IEquatable<Matrix4x4>
        {
            // Token: 0x06000C61 RID: 3169 RVA: 0x00010144 File Offset: 0x0000E344
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
            {
                this.m00 = column0.x;
                this.m01 = column1.x;
                this.m02 = column2.x;
                this.m03 = column3.x;
                this.m10 = column0.y;
                this.m11 = column1.y;
                this.m12 = column2.y;
                this.m13 = column3.y;
                this.m20 = column0.z;
                this.m21 = column1.z;
                this.m22 = column2.z;
                this.m23 = column3.z;
                this.m30 = column0.w;
                this.m31 = column1.w;
                this.m32 = column2.w;
                this.m33 = column3.w;
            }
            // Token: 0x06000C62 RID: 3170 RVA: 0x00010224 File Offset: 0x0000E424
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Quaternion GetRotation()
            {
                Quaternion result;
                Matrix4x4.GetRotation_Injected(ref this, out result);
                return result;
            }

            // Token: 0x06000C63 RID: 3171 RVA: 0x0001023C File Offset: 0x0000E43C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Vector3 GetLossyScale()
            {
                Vector3 result;
                Matrix4x4.GetLossyScale_Injected(ref this, out result);
                return result;
            }

            // Token: 0x06000C64 RID: 3172 RVA: 0x00010252 File Offset: 0x0000E452
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private bool IsIdentity()
            {
                return Matrix4x4.IsIdentity_Injected(ref this);
            }

            // Token: 0x06000C65 RID: 3173 RVA: 0x0001025A File Offset: 0x0000E45A
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private float GetDeterminant()
            {
                return Matrix4x4.GetDeterminant_Injected(ref this);
            }

            // Token: 0x06000C66 RID: 3174 RVA: 0x00010264 File Offset: 0x0000E464
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private FrustumPlanes DecomposeProjection()
            {
                FrustumPlanes result;
                Matrix4x4.DecomposeProjection_Injected(ref this, out result);
                return result;
            }

            // Token: 0x17000296 RID: 662
            // (get) Token: 0x06000C67 RID: 3175 RVA: 0x0001027C File Offset: 0x0000E47C
            public Quaternion rotation
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this.GetRotation();
                }
            }

            // Token: 0x17000297 RID: 663
            // (get) Token: 0x06000C68 RID: 3176 RVA: 0x00010298 File Offset: 0x0000E498
            public Vector3 lossyScale
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this.GetLossyScale();
                }
            }

            // Token: 0x17000298 RID: 664
            // (get) Token: 0x06000C69 RID: 3177 RVA: 0x000102B4 File Offset: 0x0000E4B4
            public bool isIdentity
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this.IsIdentity();
                }
            }

            // Token: 0x17000299 RID: 665
            // (get) Token: 0x06000C6A RID: 3178 RVA: 0x000102D0 File Offset: 0x0000E4D0
            public float determinant
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this.GetDeterminant();
                }
            }

            // Token: 0x1700029A RID: 666
            // (get) Token: 0x06000C6B RID: 3179 RVA: 0x000102EC File Offset: 0x0000E4EC
            public FrustumPlanes decomposeProjection
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this.DecomposeProjection();
                }
            }

            // Token: 0x06000C6C RID: 3180 RVA: 0x00010307 File Offset: 0x0000E507
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool ValidTRS()
            {
                return Matrix4x4.ValidTRS_Injected(ref this);
            }

            // Token: 0x06000C6D RID: 3181 RVA: 0x00010310 File Offset: 0x0000E510
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Determinant(Matrix4x4 m)
            {
                return m.determinant;
            }

            // Token: 0x06000C6E RID: 3182 RVA: 0x0001032C File Offset: 0x0000E52C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
            {
                Matrix4x4 result;
                Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out result);
                return result;
            }

            // Token: 0x06000C6F RID: 3183 RVA: 0x00010347 File Offset: 0x0000E547
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
            {
                this = Matrix4x4.TRS(pos, q, s);
            }

            // Token: 0x06000C70 RID: 3184 RVA: 0x00010358 File Offset: 0x0000E558
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Inverse(Matrix4x4 m)
            {
                Matrix4x4 result;
                Matrix4x4.Inverse_Injected(ref m, out result);
                return result;
            }

            // Token: 0x1700029B RID: 667
            // (get) Token: 0x06000C71 RID: 3185 RVA: 0x00010370 File Offset: 0x0000E570
            public Matrix4x4 inverse
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Matrix4x4.Inverse(this);
                }
            }

            // Token: 0x06000C72 RID: 3186 RVA: 0x00010390 File Offset: 0x0000E590
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Transpose(Matrix4x4 m)
            {
                Matrix4x4 result;
                Matrix4x4.Transpose_Injected(ref m, out result);
                return result;
            }

            // Token: 0x1700029C RID: 668
            // (get) Token: 0x06000C73 RID: 3187 RVA: 0x000103A8 File Offset: 0x0000E5A8
            public Matrix4x4 transpose
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Matrix4x4.Transpose(this);
                }
            }

            // Token: 0x06000C74 RID: 3188 RVA: 0x000103C8 File Offset: 0x0000E5C8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
            {
                Matrix4x4 result;
                Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out result);
                return result;
            }

            // Token: 0x06000C75 RID: 3189 RVA: 0x000103E8 File Offset: 0x0000E5E8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
            {
                Matrix4x4 result;
                Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out result);
                return result;
            }

            // Token: 0x06000C76 RID: 3190 RVA: 0x00010404 File Offset: 0x0000E604
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
            {
                Matrix4x4 result;
                Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out result);
                return result;
            }

            // Token: 0x06000C77 RID: 3191 RVA: 0x00010420 File Offset: 0x0000E620
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
            {
                Matrix4x4 result;
                Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out result);
                return result;
            }

            // Token: 0x06000C78 RID: 3192 RVA: 0x00010440 File Offset: 0x0000E640
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Frustum(FrustumPlanes fp)
            {
                return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
            }

            // Token: 0x1700029D RID: 669
            public float this[int row, int column]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return this[row + column * 4];
                }
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set
                {
                    this[row + column * 4] = value;
                }
            }

            // Token: 0x1700029E RID: 670
            public float this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    float result;
                    switch (index)
                    {
                        case 0:
                            result = this.m00;
                            break;
                        case 1:
                            result = this.m10;
                            break;
                        case 2:
                            result = this.m20;
                            break;
                        case 3:
                            result = this.m30;
                            break;
                        case 4:
                            result = this.m01;
                            break;
                        case 5:
                            result = this.m11;
                            break;
                        case 6:
                            result = this.m21;
                            break;
                        case 7:
                            result = this.m31;
                            break;
                        case 8:
                            result = this.m02;
                            break;
                        case 9:
                            result = this.m12;
                            break;
                        case 10:
                            result = this.m22;
                            break;
                        case 11:
                            result = this.m32;
                            break;
                        case 12:
                            result = this.m03;
                            break;
                        case 13:
                            result = this.m13;
                            break;
                        case 14:
                            result = this.m23;
                            break;
                        case 15:
                            result = this.m33;
                            break;
                        default:
                            throw new IndexOutOfRangeException("Invalid matrix index!");
                    }
                    return result;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set
                {
                    switch (index)
                    {
                        case 0:
                            this.m00 = value;
                            break;
                        case 1:
                            this.m10 = value;
                            break;
                        case 2:
                            this.m20 = value;
                            break;
                        case 3:
                            this.m30 = value;
                            break;
                        case 4:
                            this.m01 = value;
                            break;
                        case 5:
                            this.m11 = value;
                            break;
                        case 6:
                            this.m21 = value;
                            break;
                        case 7:
                            this.m31 = value;
                            break;
                        case 8:
                            this.m02 = value;
                            break;
                        case 9:
                            this.m12 = value;
                            break;
                        case 10:
                            this.m22 = value;
                            break;
                        case 11:
                            this.m32 = value;
                            break;
                        case 12:
                            this.m03 = value;
                            break;
                        case 13:
                            this.m13 = value;
                            break;
                        case 14:
                            this.m23 = value;
                            break;
                        case 15:
                            this.m33 = value;
                            break;
                        default:
                            throw new IndexOutOfRangeException("Invalid matrix index!");
                    }
                }
            }

            // Token: 0x06000C7D RID: 3197 RVA: 0x00010700 File Offset: 0x0000E900
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
            }

            // Token: 0x06000C7E RID: 3198 RVA: 0x00010774 File Offset: 0x0000E974
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Matrix4x4 && this.Equals((Matrix4x4)other);
            }

            // Token: 0x06000C7F RID: 3199 RVA: 0x000107A8 File Offset: 0x0000E9A8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Matrix4x4 other)
            {
                return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
            }

            // Token: 0x06000C80 RID: 3200 RVA: 0x00010830 File Offset: 0x0000EA30
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
            {
                Matrix4x4 result;
                result.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
                result.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
                result.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
                result.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
                result.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
                result.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
                result.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
                result.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
                result.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
                result.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
                result.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
                result.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
                result.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
                result.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
                result.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
                result.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
                return result;
            }

            // Token: 0x06000C81 RID: 3201 RVA: 0x00010CA8 File Offset: 0x0000EEA8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
            {
                Vector4 result;
                result.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
                result.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
                result.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
                result.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
                return result;
            }

            // Token: 0x06000C82 RID: 3202 RVA: 0x00010DD8 File Offset: 0x0000EFD8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
            {
                return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
            }

            // Token: 0x06000C83 RID: 3203 RVA: 0x00010E54 File Offset: 0x0000F054
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06000C84 RID: 3204 RVA: 0x00010E74 File Offset: 0x0000F074
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector4 GetColumn(int index)
            {
                Vector4 result;
                switch (index)
                {
                    case 0:
                        result.x = this.m00;
                        result.y = this.m10;
                        result.z = this.m20;
                        result.w = this.m30;
                        break;
                    case 1:
                        result.x = this.m01;
                        result.y = this.m11;
                        result.z = this.m21;
                        result.w = this.m31;
                        break;
                    case 2:
                        result.x = this.m02;
                        result.y = this.m12;
                        result.z = this.m22;
                        result.w = this.m32;
                        break;
                    case 3:
                        result.x = this.m03;
                        result.y = this.m13;
                        result.z = this.m23;
                        result.w = this.m33;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid column index!");
                }
                return result;
            }

            // Token: 0x06000C85 RID: 3205 RVA: 0x00010F38 File Offset: 0x0000F138
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector4 GetRow(int index)
            {
                Vector4 result;
                switch (index)
                {
                    case 0:
                        result.x = this.m00;
                        result.y = this.m01;
                        result.z = this.m02;
                        result.w = this.m03;
                        break;
                    case 1:
                        result.x = this.m10;
                        result.y = this.m11;
                        result.z = this.m12;
                        result.w = this.m13;
                        break;
                    case 2:
                        result.x = this.m20;
                        result.y = this.m21;
                        result.z = this.m22;
                        result.w = this.m23;
                        break;
                    case 3:
                        result.x = this.m30;
                        result.y = this.m31;
                        result.z = this.m32;
                        result.w = this.m33;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid row index!");
                }
                return result;
            }

            // Token: 0x06000C86 RID: 3206 RVA: 0x00010FF9 File Offset: 0x0000F1F9
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetColumn(int index, Vector4 column)
            {
                this[0, index] = column.x;
                this[1, index] = column.y;
                this[2, index] = column.z;
                this[3, index] = column.w;
            }

            // Token: 0x06000C87 RID: 3207 RVA: 0x00011038 File Offset: 0x0000F238
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetRow(int index, Vector4 row)
            {
                this[index, 0] = row.x;
                this[index, 1] = row.y;
                this[index, 2] = row.z;
                this[index, 3] = row.w;
            }

            // Token: 0x06000C88 RID: 3208 RVA: 0x00011078 File Offset: 0x0000F278
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3 MultiplyPoint(Vector3 point)
            {
                Vector3 result;
                result.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
                result.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
                result.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
                float num = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
                num = 1f / num;
                result.x *= num;
                result.y *= num;
                result.z *= num;
                return result;
            }

            // Token: 0x06000C89 RID: 3209 RVA: 0x000111A8 File Offset: 0x0000F3A8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3 MultiplyPoint3x4(Vector3 point)
            {
                Vector3 result;
                result.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
                result.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
                result.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
                return result;
            }

            // Token: 0x06000C8A RID: 3210 RVA: 0x0001126C File Offset: 0x0000F46C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3 MultiplyVector(Vector3 vector)
            {
                Vector3 result;
                result.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
                result.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
                result.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
                return result;
            }

            // Token: 0x06000C8B RID: 3211 RVA: 0x0001131C File Offset: 0x0000F51C
            //public Plane TransformPlane(Plane plane)
            //{
            //    Matrix4x4 inverse = this.inverse;
            //    float x = plane.normal.x;
            //    float y = plane.normal.y;
            //    float z = plane.normal.z;
            //    float distance = plane.distance;
            //    float x2 = inverse.m00 * x + inverse.m10 * y + inverse.m20 * z + inverse.m30 * distance;
            //    float y2 = inverse.m01 * x + inverse.m11 * y + inverse.m21 * z + inverse.m31 * distance;
            //    float z2 = inverse.m02 * x + inverse.m12 * y + inverse.m22 * z + inverse.m32 * distance;
            //    float d = inverse.m03 * x + inverse.m13 * y + inverse.m23 * z + inverse.m33 * distance;
            //    Vector3 vec;
            //    vec.x = x2;
            //    vec.y = y2;
            //    vec.z = z2;
            //    return new Plane(UnityEngine.Vector3, d);
            //}

            // Token: 0x06000C8C RID: 3212 RVA: 0x00011434 File Offset: 0x0000F634
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Scale(Vector3 vector)
            {
                Matrix4x4 result;
                result.m00 = vector.x;
                result.m01 = 0f;
                result.m02 = 0f;
                result.m03 = 0f;
                result.m10 = 0f;
                result.m11 = vector.y;
                result.m12 = 0f;
                result.m13 = 0f;
                result.m20 = 0f;
                result.m21 = 0f;
                result.m22 = vector.z;
                result.m23 = 0f;
                result.m30 = 0f;
                result.m31 = 0f;
                result.m32 = 0f;
                result.m33 = 1f;
                return result;
            }

            // Token: 0x06000C8D RID: 3213 RVA: 0x00011510 File Offset: 0x0000F710
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Translate(Vector3 vector)
            {
                Matrix4x4 result;
                result.m00 = 1f;
                result.m01 = 0f;
                result.m02 = 0f;
                result.m03 = vector.x;
                result.m10 = 0f;
                result.m11 = 1f;
                result.m12 = 0f;
                result.m13 = vector.y;
                result.m20 = 0f;
                result.m21 = 0f;
                result.m22 = 1f;
                result.m23 = vector.z;
                result.m30 = 0f;
                result.m31 = 0f;
                result.m32 = 0f;
                result.m33 = 1f;
                return result;
            }

            // Token: 0x06000C8E RID: 3214 RVA: 0x000115EC File Offset: 0x0000F7EC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Matrix4x4 Rotate(Quaternion q)
            {
                float num = q.x * 2f;
                float num2 = q.y * 2f;
                float num3 = q.z * 2f;
                float num4 = q.x * num;
                float num5 = q.y * num2;
                float num6 = q.z * num3;
                float num7 = q.x * num2;
                float num8 = q.x * num3;
                float num9 = q.y * num3;
                float num10 = q.w * num;
                float num11 = q.w * num2;
                float num12 = q.w * num3;
                Matrix4x4 result;
                result.m00 = 1f - (num5 + num6);
                result.m10 = num7 + num12;
                result.m20 = num8 - num11;
                result.m30 = 0f;
                result.m01 = num7 - num12;
                result.m11 = 1f - (num4 + num6);
                result.m21 = num9 + num10;
                result.m31 = 0f;
                result.m02 = num8 + num11;
                result.m12 = num9 - num10;
                result.m22 = 1f - (num4 + num5);
                result.m32 = 0f;
                result.m03 = 0f;
                result.m13 = 0f;
                result.m23 = 0f;
                result.m33 = 1f;
                return result;
            }

            // Token: 0x1700029F RID: 671
            // (get) Token: 0x06000C8F RID: 3215 RVA: 0x00011764 File Offset: 0x0000F964
            public static Matrix4x4 zero
            {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Matrix4x4.zeroMatrix;
                }
            }

            // Token: 0x170002A0 RID: 672
            // (get) Token: 0x06000C90 RID: 3216 RVA: 0x00011780 File Offset: 0x0000F980
            public static Matrix4x4 identity
            {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Matrix4x4.identityMatrix;
                }
            }

            // Token: 0x06000C91 RID: 3217 RVA: 0x0001179C File Offset: 0x0000F99C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString()
            {
                return string.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
                {
                this.m00,
                this.m01,
                this.m02,
                this.m03,
                this.m10,
                this.m11,
                this.m12,
                this.m13,
                this.m20,
                this.m21,
                this.m22,
                this.m23,
                this.m30,
                this.m31,
                this.m32,
                this.m33
                });
            }

            // Token: 0x06000C92 RID: 3218 RVA: 0x000118AC File Offset: 0x0000FAAC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public string ToString(string format)
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
                {
                this.m00.ToString(format),
                this.m01.ToString(format),
                this.m02.ToString(format),
                this.m03.ToString(format),
                this.m10.ToString(format),
                this.m11.ToString(format),
                this.m12.ToString(format),
                this.m13.ToString(format),
                this.m20.ToString(format),
                this.m21.ToString(format),
                this.m22.ToString(format),
                this.m23.ToString(format),
                this.m30.ToString(format),
                this.m31.ToString(format),
                this.m32.ToString(format),
                this.m33.ToString(format)
                });
            }

            // Token: 0x06000C94 RID: 3220
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

            // Token: 0x06000C95 RID: 3221
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

            // Token: 0x06000C96 RID: 3222
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern bool IsIdentity_Injected(ref Matrix4x4 _unity_self);

            // Token: 0x06000C97 RID: 3223
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern float GetDeterminant_Injected(ref Matrix4x4 _unity_self);

            // Token: 0x06000C98 RID: 3224
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

            // Token: 0x06000C99 RID: 3225
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern bool ValidTRS_Injected(ref Matrix4x4 _unity_self);

            // Token: 0x06000C9A RID: 3226
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void TRS_Injected(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 ret);

            // Token: 0x06000C9B RID: 3227
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Inverse_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

            // Token: 0x06000C9C RID: 3228
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Transpose_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

            // Token: 0x06000C9D RID: 3229
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

            // Token: 0x06000C9E RID: 3230
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

            // Token: 0x06000C9F RID: 3231
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void LookAt_Injected(ref Vector3 from, ref Vector3 to, ref Vector3 up, out Matrix4x4 ret);

            // Token: 0x06000CA0 RID: 3232
            [MethodImpl(MethodImplOptions.InternalCall)]
            private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

            // Token: 0x04000695 RID: 1685
            public float m00;

            // Token: 0x04000696 RID: 1686
            public float m10;

            // Token: 0x04000697 RID: 1687
            public float m20;

            // Token: 0x04000698 RID: 1688
            public float m30;

            // Token: 0x04000699 RID: 1689
            public float m01;

            // Token: 0x0400069A RID: 1690
            public float m11;

            // Token: 0x0400069B RID: 1691
            public float m21;

            // Token: 0x0400069C RID: 1692
            public float m31;

            // Token: 0x0400069D RID: 1693
            public float m02;

            // Token: 0x0400069E RID: 1694
            public float m12;

            // Token: 0x0400069F RID: 1695
            public float m22;

            // Token: 0x040006A0 RID: 1696
            public float m32;

            // Token: 0x040006A1 RID: 1697
            public float m03;

            // Token: 0x040006A2 RID: 1698
            public float m13;

            // Token: 0x040006A3 RID: 1699
            public float m23;

            // Token: 0x040006A4 RID: 1700
            public float m33;

            // Token: 0x040006A5 RID: 1701
            private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

            // Token: 0x040006A6 RID: 1702
            private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
        }
    }
}
