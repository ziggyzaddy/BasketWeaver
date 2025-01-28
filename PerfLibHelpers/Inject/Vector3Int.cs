using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Inject
{
    namespace UnityEngine
    {
        // Token: 0x0200024C RID: 588
        public struct Vector3Int : IEquatable<Vector3Int>
        {
            // Token: 0x06001514 RID: 5396 RVA: 0x00022071 File Offset: 0x00020271
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3Int(int x, int y, int z)
            {
                this.m_X = x;
                this.m_Y = y;
                this.m_Z = z;
            }

            // Token: 0x17000425 RID: 1061
            // (get) Token: 0x06001515 RID: 5397 RVA: 0x0002208C File Offset: 0x0002028C
            // (set) Token: 0x06001516 RID: 5398 RVA: 0x000220A7 File Offset: 0x000202A7
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

            // Token: 0x17000426 RID: 1062
            // (get) Token: 0x06001517 RID: 5399 RVA: 0x000220B4 File Offset: 0x000202B4
            // (set) Token: 0x06001518 RID: 5400 RVA: 0x000220CF File Offset: 0x000202CF
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

            // Token: 0x17000427 RID: 1063
            // (get) Token: 0x06001519 RID: 5401 RVA: 0x000220DC File Offset: 0x000202DC
            // (set) Token: 0x0600151A RID: 5402 RVA: 0x000220F7 File Offset: 0x000202F7
            public int z
            {
                get
                {
                    return this.m_Z;
                }
                set
                {
                    this.m_Z = value;
                }
            }

            // Token: 0x0600151B RID: 5403 RVA: 0x00022071 File Offset: 0x00020271
            public void Set(int x, int y, int z)
            {
                this.m_X = x;
                this.m_Y = y;
                this.m_Z = z;
            }

            // Token: 0x17000428 RID: 1064
            public int this[int index]
            {
                get
                {
                    int result;
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
                            throw new IndexOutOfRangeException($"Invalid Vector3Int index addressed: {index}");
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
                        default:
                            throw new IndexOutOfRangeException($"Invalid Vector3Int index addressed: {index}");

                    }
                }
            }

            // Token: 0x17000429 RID: 1065
            // (get) Token: 0x0600151E RID: 5406 RVA: 0x000221D8 File Offset: 0x000203D8
            public float magnitude
            {
                get
                {
                    return (float)Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
                }
            }

            // Token: 0x1700042A RID: 1066
            // (get) Token: 0x0600151F RID: 5407 RVA: 0x0002221C File Offset: 0x0002041C
            public int sqrMagnitude
            {
                get
                {
                    return this.x * this.x + this.y * this.y + this.z * this.z;
                }
            }

            // Token: 0x06001520 RID: 5408 RVA: 0x0002225C File Offset: 0x0002045C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static float Distance(Vector3Int a, Vector3Int b)
            {
                return (a - b).magnitude;
            }

            // Token: 0x06001521 RID: 5409 RVA: 0x00022280 File Offset: 0x00020480
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
            {
                Vector3Int res;
                res.m_X = Math.Min(lhs.x, rhs.x);
                res.m_Y = Math.Min(lhs.y, rhs.y);
                res.m_Z = Math.Min(lhs.z, rhs.z);
                return res;
            }

            // Token: 0x06001522 RID: 5410 RVA: 0x000222D4 File Offset: 0x000204D4
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
            {
                Vector3Int res;
                res.m_X = Math.Max(lhs.x, rhs.x);
                res.m_Y = Math.Max(lhs.y, rhs.y);
                res.m_Z = Math.Max(lhs.z, rhs.z);
                return res;
            }

            // Token: 0x06001523 RID: 5411 RVA: 0x00022328 File Offset: 0x00020528
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3Int Scale(Vector3Int a, Vector3Int b)
            {
                Vector3Int res;
                res.m_X = a.x * b.x;
                res.m_Y = a.y * b.y;
                res.m_Z = a.z * b.z;
                return res;
            }

            // Token: 0x06001524 RID: 5412 RVA: 0x0002236F File Offset: 0x0002056F
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Scale(Vector3Int scale)
            {
                this.x *= scale.x;
                this.y *= scale.y;
                this.z *= scale.z;
            }

            // Token: 0x06001525 RID: 5413 RVA: 0x000223B0 File Offset: 0x000205B0
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clamp(Vector3Int min, Vector3Int max)
            {
                this.x = Math.Max(min.x, this.x);
                this.x = Math.Min(max.x, this.x);
                this.y = Math.Max(min.y, this.y);
                this.y = Math.Min(max.y, this.y);
                this.z = Math.Max(min.z, this.z);
                this.z = Math.Min(max.z, this.z);
            }

            // Token: 0x06001526 RID: 5414 RVA: 0x00022450 File Offset: 0x00020650
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Vector3(Vector3Int v)
            {
                Vector3 res;
                res.x = (float)v.x;
                res.y = (float)v.y;
                res.z = (float)v.z;
                return res;
            }

            // Token: 0x06001527 RID: 5415 RVA: 0x00022484 File Offset: 0x00020684
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static explicit operator Vector2Int(Vector3Int v)
            {
                return new Vector2Int(v.x, v.y);
            }

            // Token: 0x06001528 RID: 5416 RVA: 0x000224AC File Offset: 0x000206AC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Vector3Int FloorToInt(Vector3 v)
            {
                Vector3Int res;
                res.m_X = (int)Math.Floor(v.x);
                res.m_Y = (int)Math.Floor(v.y);
                res.m_Z = (int)Math.Floor(v.z);
                return res;
            }

            // Token: 0x06001529 RID: 5417 RVA: 0x000224EC File Offset: 0x000206EC
            public static Vector3Int CeilToInt(Vector3 v)
            {
                Vector3Int res;
                res.m_X = (int)Math.Floor(v.x);
                res.m_Y = (int)Math.Floor(v.y);
                res.m_Z = (int)Math.Floor(v.z);
                return new Vector3Int((int)Math.Ceiling(v.x), (int)Math.Ceiling(v.y), (int)Math.Ceiling(v.z));
            }

            // Token: 0x0600152A RID: 5418 RVA: 0x0002252C File Offset: 0x0002072C
            public static Vector3Int RoundToInt(Vector3 v)
            {
                Vector3Int res;
                res.m_X = (int)Math.Round(v.x);
                res.m_Y = (int)Math.Round(v.y);
                res.m_Z = (int)Math.Round(v.z);
                return res;
            }

            // Token: 0x0600152B RID: 5419 RVA: 0x0002256C File Offset: 0x0002076C
            public static Vector3Int operator +(Vector3Int a, Vector3Int b)
            {
                Vector3Int res;
                res.m_X = a.x + b.x;
                res.m_Y = a.y + b.y;
                res.m_Z = a.z + b.z;
                return res;
            }

            // Token: 0x0600152C RID: 5420 RVA: 0x000225B4 File Offset: 0x000207B4
            public static Vector3Int operator -(Vector3Int a, Vector3Int b)
            {
                Vector3Int res;
                res.m_X = a.x - b.x;
                res.m_Y = a.y - b.y;
                res.m_Z = a.z - b.z;
                return res;
            }

            // Token: 0x0600152D RID: 5421 RVA: 0x000225FC File Offset: 0x000207FC
            public static Vector3Int operator *(Vector3Int a, Vector3Int b)
            {
                Vector3Int res;
                res.m_X = a.x * b.x;
                res.m_Y = a.y * b.y;
                res.m_Z = a.z * b.z;
                return res;
            }

            // Token: 0x0600152E RID: 5422 RVA: 0x00022644 File Offset: 0x00020844
            public static Vector3Int operator *(Vector3Int a, int b)
            {
                Vector3Int res;
                res.m_X = a.x * b;
                res.m_Y = a.y * b;
                res.m_Z = a.z * b;
                return res;
            }

            // Token: 0x0600152F RID: 5423 RVA: 0x0002267C File Offset: 0x0002087C
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
            {
                return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
            }

            // Token: 0x06001530 RID: 5424 RVA: 0x000226CC File Offset: 0x000208CC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
            {
                return !(lhs == rhs);
            }

            // Token: 0x06001531 RID: 5425 RVA: 0x000226EC File Offset: 0x000208EC
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override bool Equals(object other)
            {
                return other is Vector3Int && this.Equals((Vector3Int)other);
            }

            // Token: 0x06001532 RID: 5426 RVA: 0x00022720 File Offset: 0x00020920
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(Vector3Int other)
            {
                return this == other;
            }

            // Token: 0x06001533 RID: 5427 RVA: 0x00022744 File Offset: 0x00020944
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetHashCode()
            {
                int hashCode = this.y.GetHashCode();
                int hashCode2 = this.z.GetHashCode();
                return this.x.GetHashCode() ^ hashCode << 4 ^ hashCode >> 28 ^ hashCode2 >> 4 ^ hashCode2 << 28;
            }

            // Token: 0x06001534 RID: 5428 RVA: 0x000227AC File Offset: 0x000209AC
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(this.x.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append(this.y.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append(this.z.ToString());
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }

            // Token: 0x06001535 RID: 5429 RVA: 0x000227FC File Offset: 0x000209FC
            public string ToString(string format)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("(");
                stringBuilder.Append(this.x.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append(this.y.ToString());
                stringBuilder.Append(", ");
                stringBuilder.Append(this.z.ToString());
                stringBuilder.Append(")");
                return stringBuilder.ToString();

            }

            // Token: 0x1700042B RID: 1067
            // (get) Token: 0x06001536 RID: 5430 RVA: 0x00022858 File Offset: 0x00020A58
            public static Vector3Int zero
            {
                get
                {
                    return Vector3Int.s_Zero;
                }
            }

            // Token: 0x1700042C RID: 1068
            // (get) Token: 0x06001537 RID: 5431 RVA: 0x00022874 File Offset: 0x00020A74
            public static Vector3Int one
            {
                get
                {
                    return Vector3Int.s_One;
                }
            }

            // Token: 0x1700042D RID: 1069
            // (get) Token: 0x06001538 RID: 5432 RVA: 0x00022890 File Offset: 0x00020A90
            public static Vector3Int up
            {
                get
                {
                    return Vector3Int.s_Up;
                }
            }

            // Token: 0x1700042E RID: 1070
            // (get) Token: 0x06001539 RID: 5433 RVA: 0x000228AC File Offset: 0x00020AAC
            public static Vector3Int down
            {
                get
                {
                    return Vector3Int.s_Down;
                }
            }

            // Token: 0x1700042F RID: 1071
            // (get) Token: 0x0600153A RID: 5434 RVA: 0x000228C8 File Offset: 0x00020AC8
            public static Vector3Int left
            {
                get
                {
                    return Vector3Int.s_Left;
                }
            }

            // Token: 0x17000430 RID: 1072
            // (get) Token: 0x0600153B RID: 5435 RVA: 0x000228E4 File Offset: 0x00020AE4
            public static Vector3Int right
            {
                get
                {
                    return Vector3Int.s_Right;
                }
            }

            // Token: 0x0600153C RID: 5436 RVA: 0x00022900 File Offset: 0x00020B00
            // Note: this type is marked as 'beforefieldinit'.
            static Vector3Int()
            {
            }

            // Token: 0x040007FA RID: 2042
            private int m_X;

            // Token: 0x040007FB RID: 2043
            private int m_Y;

            // Token: 0x040007FC RID: 2044
            private int m_Z;

            // Token: 0x040007FD RID: 2045
            private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

            // Token: 0x040007FE RID: 2046
            private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

            // Token: 0x040007FF RID: 2047
            private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

            // Token: 0x04000800 RID: 2048
            private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

            // Token: 0x04000801 RID: 2049
            private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

            // Token: 0x04000802 RID: 2050
            private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);
        }
    }
}