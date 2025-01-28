using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Inject
{
    namespace UnityEngine
    {
        // Token: 0x0200024B RID: 587
        public struct Vector2Int : IEquatable<Vector2Int>
        {
            // Token: 0x060014EE RID: 5358 RVA: 0x0002196D File Offset: 0x0001FB6D
            public Vector2Int(int x, int y)
            {
                this.m_X = x;
                this.m_Y = y;
            }

            // Token: 0x1700041A RID: 1050
            // (get) Token: 0x060014EF RID: 5359 RVA: 0x00021980 File Offset: 0x0001FB80
            // (set) Token: 0x060014F0 RID: 5360 RVA: 0x0002199B File Offset: 0x0001FB9B
            public int x
            {
                get
                {
                    return this.m_X;
                }
                set
                {
                    this.m_X = value;
                }
            }

            // Token: 0x1700041B RID: 1051
            // (get) Token: 0x060014F1 RID: 5361 RVA: 0x000219A8 File Offset: 0x0001FBA8
            // (set) Token: 0x060014F2 RID: 5362 RVA: 0x000219C3 File Offset: 0x0001FBC3
            public int y
            {
                get
                {
                    return this.m_Y;
                }
                set
                {
                    this.m_Y = value;
                }
            }

            // Token: 0x060014F3 RID: 5363 RVA: 0x0002196D File Offset: 0x0001FB6D
            public void Set(int x, int y)
            {
                this.m_X = x;
                this.m_Y = y;
            }

            // Token: 0x1700041C RID: 1052
            public int this[int index]
            {
                get
                {
                    int result;
                    if (index != 0)
                    {
                        if (index != 1)
                        {
                            throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
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
                            throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
                        }
                        this.y = value;
                    }
                    else
                    {
                        this.x = value;
                    }
                }
            }

            // Token: 0x1700041D RID: 1053
            // (get) Token: 0x060014F6 RID: 5366 RVA: 0x00021A70 File Offset: 0x0001FC70
            public float magnitude
            {
                get
                {
                    return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
                }
            }

            // Token: 0x1700041E RID: 1054
            // (get) Token: 0x060014F7 RID: 5367 RVA: 0x00021AA8 File Offset: 0x0001FCA8
            public int sqrMagnitude
            {
                get
                {
                    return this.x * this.x + this.y * this.y;
                }
            }

            // Token: 0x060014F8 RID: 5368 RVA: 0x00021AD8 File Offset: 0x0001FCD8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Vector2Int a, Vector2Int b)
            {
                return (a - b).magnitude;
            }

            // Token: 0x060014F9 RID: 5369 RVA: 0x00021AFC File Offset: 0x0001FCFC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
            {
                Vector2Int res;
                res.m_X = Mathf.Min(lhs.x, rhs.x);
                res.m_Y = Mathf.Min(lhs.y, rhs.y);
                return res;
            }

            // Token: 0x060014FA RID: 5370 RVA: 0x00021B3C File Offset: 0x0001FD3C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
            {
                Vector2Int res;
                res.m_X = Mathf.Max(lhs.x, rhs.x);
                res.m_Y = Mathf.Max(lhs.y, rhs.y);
                return res;
            }

            // Token: 0x060014FB RID: 5371 RVA: 0x00021B7C File Offset: 0x0001FD7C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int Scale(Vector2Int a, Vector2Int b)
            {
                Vector2Int res;
                res.m_X = a.x * b.x;
                res.m_Y = a.y * b.y;
                return res;
            }

            // Token: 0x060014FC RID: 5372 RVA: 0x00021BB4 File Offset: 0x0001FDB4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Scale(Vector2Int scale)
            {
                this.x *= scale.x;
                this.y *= scale.y;
            }

            // Token: 0x060014FD RID: 5373 RVA: 0x00021BE0 File Offset: 0x0001FDE0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clamp(Vector2Int min, Vector2Int max)
            {
                this.x = Math.Max(min.x, this.x);
                this.x = Math.Min(max.x, this.x);
                this.y = Math.Max(min.y, this.y);
                this.y = Math.Min(max.y, this.y);
            }

            // Token: 0x060014FE RID: 5374 RVA: 0x00021C50 File Offset: 0x0001FE50
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Vector2(Vector2Int v)
            {
                Vector2 res;
                res.x = (float)v.x;
                res.y = (float)v.y;
                return res;
            }

            // Token: 0x060014FF RID: 5375 RVA: 0x00021C7C File Offset: 0x0001FE7C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static explicit operator Vector3Int(Vector2Int v)
            {
                return new Vector3Int(v.x, v.y, 0);
            }

            // Token: 0x06001500 RID: 5376 RVA: 0x00021CA8 File Offset: 0x0001FEA8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int FloorToInt(Vector2 v)
            {
                Vector2Int res;
                res.m_X = Mathf.FloorToInt(v.x);
                res.m_Y = Mathf.FloorToInt(v.y);
                return res;
            }

            // Token: 0x06001501 RID: 5377 RVA: 0x00021CDC File Offset: 0x0001FEDC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int CeilToInt(Vector2 v)
            {
                Vector2Int res;
                res.m_X = Mathf.CeilToInt(v.x);
                res.m_Y = Mathf.CeilToInt(v.y);
                return res;
            }

            // Token: 0x06001502 RID: 5378 RVA: 0x00021D10 File Offset: 0x0001FF10
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int RoundToInt(Vector2 v)
            {
                Vector2Int res;
                res.m_X = Mathf.RoundToInt(v.x);
                res.m_Y = Mathf.RoundToInt(v.y);
                return res;
            }

            // Token: 0x06001503 RID: 5379 RVA: 0x00021D44 File Offset: 0x0001FF44
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int operator +(Vector2Int a, Vector2Int b)
            {
                Vector2Int res;
                res.m_X = a.x + b.x;
                res.m_Y = a.y + b.y;
                return res;
            }

            // Token: 0x06001504 RID: 5380 RVA: 0x00021D7C File Offset: 0x0001FF7C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int operator -(Vector2Int a, Vector2Int b)
            {
                Vector2Int res;
                res.m_X = a.x - b.x;
                res.m_Y = a.y - b.y;
                return res;
            }

            // Token: 0x06001505 RID: 5381 RVA: 0x00021DB4 File Offset: 0x0001FFB4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int operator *(Vector2Int a, Vector2Int b)
            {
                Vector2Int res;
                res.m_X = a.x * b.x;
                res.m_Y = a.y * b.y;
                return res;
            }

            // Token: 0x06001506 RID: 5382 RVA: 0x00021DEC File Offset: 0x0001FFEC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector2Int operator *(Vector2Int a, int b)
            {

                Vector2Int res;
                res.m_X = a.x * b;
                res.m_Y = a.y * b;
                return res;
            }

            // Token: 0x06001507 RID: 5383 RVA: 0x00021E18 File Offset: 0x00020018
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
            {
                return lhs.x == rhs.x && lhs.y == rhs.y;
            }

            // Token: 0x06001508 RID: 5384 RVA: 0x00021E54 File Offset: 0x00020054
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06001509 RID: 5385 RVA: 0x00021E74 File Offset: 0x00020074
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Vector2Int && this.Equals((Vector2Int)other);
            }

            // Token: 0x0600150A RID: 5386 RVA: 0x00021EA8 File Offset: 0x000200A8
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Vector2Int other)
            {
                return this.x.Equals(other.x) && this.y.Equals(other.y);
            }

            // Token: 0x0600150B RID: 5387 RVA: 0x00021EF0 File Offset: 0x000200F0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
            }

            // Token: 0x0600150C RID: 5388 RVA: 0x00021F30 File Offset: 0x00020130
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(this.x.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append(this.y.ToString());
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x1700041F RID: 1055
            // (get) Token: 0x0600150D RID: 5389 RVA: 0x00021F74 File Offset: 0x00020174
            public static Vector2Int zero
            {
                get
                {
                    return Vector2Int.s_Zero;
                }
            }

            // Token: 0x17000420 RID: 1056
            // (get) Token: 0x0600150E RID: 5390 RVA: 0x00021F90 File Offset: 0x00020190
            public static Vector2Int one
            {
                get
                {
                    return Vector2Int.s_One;
                }
            }

            // Token: 0x17000421 RID: 1057
            // (get) Token: 0x0600150F RID: 5391 RVA: 0x00021FAC File Offset: 0x000201AC
            public static Vector2Int up
            {
                get
                {
                    return Vector2Int.s_Up;
                }
            }

            // Token: 0x17000422 RID: 1058
            // (get) Token: 0x06001510 RID: 5392 RVA: 0x00021FC8 File Offset: 0x000201C8
            public static Vector2Int down
            {
                get
                {
                    return Vector2Int.s_Down;
                }
            }

            // Token: 0x17000423 RID: 1059
            // (get) Token: 0x06001511 RID: 5393 RVA: 0x00021FE4 File Offset: 0x000201E4
            public static Vector2Int left
            {
                get
                {
                    return Vector2Int.s_Left;
                }
            }

            // Token: 0x17000424 RID: 1060
            // (get) Token: 0x06001512 RID: 5394 RVA: 0x00022000 File Offset: 0x00020200
            public static Vector2Int right
            {
                get
                {
                    return Vector2Int.s_Right;
                }
            }

            // Token: 0x06001513 RID: 5395 RVA: 0x0002201C File Offset: 0x0002021C
            // Note: this type is marked as 'beforefieldinit'.
            static Vector2Int()
            {
            }

            // Token: 0x040007F2 RID: 2034
            private int m_X;

            // Token: 0x040007F3 RID: 2035
            private int m_Y;

            // Token: 0x040007F4 RID: 2036
            private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

            // Token: 0x040007F5 RID: 2037
            private static readonly Vector2Int s_One = new Vector2Int(1, 1);

            // Token: 0x040007F6 RID: 2038
            private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

            // Token: 0x040007F7 RID: 2039
            private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

            // Token: 0x040007F8 RID: 2040
            private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

            // Token: 0x040007F9 RID: 2041
            private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
        }
    }
}