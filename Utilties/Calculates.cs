using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

using WackyBag;

namespace WackyBagTr.Utilties
{
	public static class Calculates
	{

		/// <summary>
		/// 获取 在Box中 到Point最远的点，可用于判断碰撞
		/// </summary>
		public static Vector2 GetFarestPoint(Rectangle Box, Vector2 Point)
		{
			Vector2 NearestPoint = Point;
			if (Box.Right - NearestPoint.X > NearestPoint.X - Box.Left) NearestPoint.X = Box.Right;
			else NearestPoint.X = Box.Left;
			if (Box.Bottom - NearestPoint.Y > NearestPoint.Y - Box.Top) NearestPoint.Y = Box.Bottom;
			else NearestPoint.Y = Box.Top;
			return NearestPoint;
		}
		/// <summary>
		/// 判断点是否在圆内
		/// </summary>
		/// <param name="Pos">圆心</param>
		/// <param name="R">半径</param>
		/// <param name="Point">目标点</param>
		/// <returns></returns>
		public static bool CheckPointInCircle(Vector2 Pos, float R, Vector2 Point) => (Point - Pos).LengthSquared() <= R * R;
		/// <summary>
		/// 判断Box与 Pos为圆心，R为半径的圆 是否碰撞
		/// </summary>
		public static bool CheckAABBvCircleColliding(Rectangle Box, Vector2 Pos, float R)
		{
			Vector2 P = Box.ClosestPointInRect(Pos);
			return CheckPointInCircle(Pos, R, P);
		}
		/// <summary>
		/// 判断Box与 Pos为圆心，半径MinR到MaxR的圆环 是否碰撞
		/// </summary>
		public static bool CheckAABBvAnnulusColliding(Rectangle Box, Vector2 Pos, float MaxR, float MinR)
		{
			Vector2 PMin = Box.ClosestPointInRect(Pos);
			Vector2 PMax = GetFarestPoint(Box, Pos);
			return CheckPointInCircle(Pos, MaxR, PMin) && !CheckPointInCircle(Pos, MinR, PMax);
		}

		/// <summary>
		/// 计算二维向量叉乘的长
		/// </summary>
		public static float CrossProduct(Vector2 v1, Vector2 v2) => v1.X * v2.Y - v1.Y * v2.X;
		/// <summary>
		/// 根据相对位置，相对速度，固定发射速度进行预判
		/// </summary>
		/// <param name="OffsetPos">相对位置</param>
		/// <param name="OffsetVel">相对速度</param>
		/// <param name="Speed">固定发射速度</param>
		public static float? PredictWithVel(Vector2 OffsetPos, Vector2 OffsetVel, float Speed)
		{
			float D = CrossProduct(OffsetPos, OffsetVel) / (Speed * OffsetPos.Length());
			if (D > 1 || D < -1) return null;
			else return (float)Math.Asin(D) + OffsetPos.ToRotation();
		}
		/// <summary>
		/// 预判，返回速度，如果没有，返回Vector2.Normalize(OffsetVel) * Speed)
		/// </summary>
		/// <param name="OffsetPos"></param>
		/// <param name="OffsetVel"></param>
		/// <param name="Speed"></param>
		/// <returns></returns>
		public static Vector2 PredictWithVelDirect(Vector2 OffsetPos, Vector2 OffsetVel, float Speed)
		{
			//float? D= PredictWithVel(OffsetPos, OffsetVel, Speed);
			//return (D.HasValue ? D.Value.ToRotationVector2() * Speed : Vector2.Normalize(OffsetVel) * Speed);
			return (PredictWithVel(OffsetPos, OffsetVel, Speed) ?? OffsetPos.ToRotation()).ToRotationVector2() * Speed;
		}

		public static int WeightedChoose(int I, params int[] values)
		{
			for (int i = 0; i < values.Length; ++i)
			{
				if (I < values[i]) return i;
				I -= values[i];
			}
			return -1;
		}
		/// <summary>
		/// 计算以origin为起始点，向direction方向移动，直到碰到方块或到达最远距离的距离，不考虑斜方块和半砖
		/// </summary>
		public static float CanHitLineDistance(Vector2 origin, float direction, float MaxDistance = 2200f, bool fallThroughPlasform = true)
		{

			if (MaxDistance == 0) return 0f;
			bool first = true;
			Vector2 Offset = direction.ToRotationVector2() * MaxDistance;
			Vector2 Unit = Vector2.Normalize(Offset);
			foreach (var i in EnumTilesInLine(origin,  Offset))
			{
				Tile tile = Main.tile[i.X, i.Y];
				if (tile.HasTile && Main.tileSolid[tile.TileType] && (Main.tileSolidTop[tile.TileType] ? !fallThroughPlasform : true))
				{
					if (first) return 0f;
					Vector2 tilePos = i.ToVector2() * 16;
					Vector2 HitT = Unit * (tilePos.Y - origin.Y) / Unit.Y;
					Vector2 HitB = Unit * (tilePos.Y + 16 - origin.Y) / Unit.Y;
					Vector2 HitL = Unit * (tilePos.X - origin.X) / Unit.X;
					Vector2 HitR = Unit * (tilePos.X + 16 - origin.X) / Unit.X;
					float HitTD = Vector2.Dot(HitT, Unit);
					float HitBD = Vector2.Dot(HitB, Unit);
					float HitLD = Vector2.Dot(HitL, Unit);
					float HitRD = Vector2.Dot(HitR, Unit);
					List<float> vs = new List<float>() { HitBD, HitLD, HitRD };
					//vs.Sort();
					//return vs[1];
					float vs1 = HitTD; float vs2 = HitBD;
					foreach (var j in vs)
					{
						if (j < vs1)
						{
							vs2 = vs1; vs1 = j;
						}
						else if (j < vs2)
						{
							vs2 = j;
						}
					}
					return vs2;
				}
				first = false;
			}
			return MaxDistance;
		}
		/// <summary>
		/// 计算以origin为起始点，向direction方向移动，直到碰到方块或到达最远距离的距离，考虑斜方块和半砖
		/// </summary>
		public static float CanHitLineDistancePerfect(Vector2 origin, float direction, float MaxDistance = 2200f, bool fallThroughPlasform = true)
		{

			if (MaxDistance == 0) return 0f;
			bool first = true;
			Vector2 Offset = direction.ToRotationVector2() * MaxDistance;
			Vector2 Unit = Vector2.Normalize(Offset);
			foreach (var i in EnumTilesInLine(origin, Offset))
			{
				Tile tile = Main.tile[i.X, i.Y];

				if (tile.HasTile && Main.tileSolid[tile.TileType] && (Main.tileSolidTop[tile.TileType] ? !fallThroughPlasform : true))
				{
					if (first) return 0f;
					Vector2 tilePos = i.ToVector2() * 16;
					List<Vector2> Points = new List<Vector2>();
					if (tile.IsHalfBlock)
					{
						Points.Add(tilePos + new Vector2(0, 8));
						Points.Add(tilePos + new Vector2(16, 8));
						Points.Add(tilePos + new Vector2(16, 16));
						Points.Add(tilePos + new Vector2(0, 16));
					}
					else
					{
						switch ((int)tile.Slope)
						{
							case 0:
								{
									Points.Add(tilePos + new Vector2(0, 0));
									Points.Add(tilePos + new Vector2(16, 0));
									Points.Add(tilePos + new Vector2(16, 16));
									Points.Add(tilePos + new Vector2(0, 16));
								}
								break;
							case 1:
								{
									Points.Add(tilePos + new Vector2(0, 0));
									Points.Add(tilePos + new Vector2(16, 16));
									Points.Add(tilePos + new Vector2(0, 16));
								}
								break;
							case 2:
								{
									Points.Add(tilePos + new Vector2(16, 0));
									Points.Add(tilePos + new Vector2(16, 16));
									Points.Add(tilePos + new Vector2(0, 16));
								}
								break;
							case 3:
								{
									Points.Add(tilePos + new Vector2(0, 0));
									Points.Add(tilePos + new Vector2(16, 0));
									Points.Add(tilePos + new Vector2(0, 16));
								}
								break;
							case 4:
								{
									Points.Add(tilePos + new Vector2(0, 0));
									Points.Add(tilePos + new Vector2(16, 0));
									Points.Add(tilePos + new Vector2(16, 16));
								}
								break;
						}
					}
					//List<float> Collidings = new List<float>();
					//for (int k = 0; k < Points.Count-1; ++k) {
					//	Collidings.Add(LineCollitionDistance(origin, direction, Points[k], (Points[k+1]-Points[k]).ToRotation()));
					//}
					//Collidings.Add(LineCollitionDistance(origin, direction, Points[Points.Count - 1], (Points[Points.Count - 1] - Points[0]).ToRotation()));
					//Collidings.Sort();
					//return Collidings[Collidings.Count-3];
					List<Vector2> Collidings = new List<Vector2>();
					Vector2 End = direction.ToRotationVector2() * MaxDistance + origin;
					foreach (var k in WackyBag.Util.EnumPairs(Points))
					{
						Collidings.AddRange(Terraria.Collision.CheckLinevLine(origin, End, k.Item1, k.Item2));
					}
					if (Collidings.Count <= 0) continue;
					float Distance = MaxDistance;
					foreach (var k in Collidings)
					{
						float NowDistance = (k - origin).Length();
						if (NowDistance < Distance) Distance = NowDistance;
					}
					return Distance;
				}
				first = false;
			}
			return MaxDistance;
		}
		/// <summary>
		/// 线1 过原点，方向Direction1 与 线2 过Point2 ，方向Direction2 交点到原点的距离（反向为负）
		/// </summary>
		public static float LineCollitionDistance(float Direction1, Vector2 Point2, float Direction2)
		{
			float P2Angle = Point2.ToRotation();
			float Angle1 = P2Angle - Direction1;
			float Angle2 = (float)Math.PI - P2Angle + Direction2;
			float Angle3 = (float)Math.PI - Angle1 - Angle2;
			float Distance = (float)(Point2.Length() / Math.Sin(Angle3) * Math.Sin(Angle2));
			return Distance;
		}
		/// <summary>
		/// 线1 过Point1，方向Direction1 与 线2 过Point2 ，方向Direction2 交点到Point1的距离（反向为负）
		/// </summary>
		public static float LineCollitionDistance(Vector2 Point1, float Direction1, Vector2 Point2, float Direction2) => LineCollitionDistance(Direction1, Point2 - Point1, Direction2);
		/// <summary>
		/// 线1 过原点，方向Direction1 与 线2 过Point2 ，方向Direction2 交点到原点的距离（反向为负）
		/// </summary>
		public static float LineCollitionDistance(Vector2 Direction1, Vector2 Point2, Vector2 Direction2)
		{
			return Direction1.Length() * CrossProduct(Point2, Direction2) / CrossProduct(Direction1, Direction2);
		}
		/// <summary>
		/// 线1 Point1a，Point1b 与 线2 Point2a ，Point2b 交点到Point1a的距离（反向为负）
		/// </summary>
		public static float LineCollitionDistance(Vector2 Point1a, Vector2 Point1b, Vector2 Point2a, Vector2 Point2b)
		{
			return LineCollitionDistance(Point1b - Point1a, Point2a - Point1a, Point2b - Point2a);
		}
		/// <summary>
		/// 计算碰撞箱以Velocity速度移动，碰撞后最终速度
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="Velocity"></param>
		/// <param name="StopWhenHit">是否在碰到物块时结束计算</param>
		/// <param name="fallThrough">是否穿过平台</param>
		/// <param name="fall2">在Velocity.Y>1时穿过平台</param>
		/// <param name="gravDir"></param>
		/// <returns></returns>
		public static Vector2 TileCollisionPerfect(Rectangle rect, Vector2 Velocity, bool StopWhenHit = false, bool fallThrough = false, bool fall2 = false, int gravDir = 1)
		{
			Vector2 Position = rect.Location.ToVector2();
			int Width = rect.Width;
			int Height = rect.Height;
			Vector2 NewPosition = Position;
			float PL = Math.Min(Width, Height);
			Vector2 PV = Vector2.Normalize(Velocity) * PL;
			Vector2 PV0 = PV;
			int i = 1;
			bool RealFall = fallThrough || (Velocity.Y > 1 && fall2);
			for (; PL * i < Velocity.Length(); ++i)
			{
				Vector2 NPL = Collision.TileCollision(NewPosition, PV, Width, Height, RealFall, false, gravDir);
				NewPosition += NPL;
				if (NPL != PV && StopWhenHit) return NewPosition - Position;
				PV = NPL;
			}
			NewPosition += Collision.TileCollision(NewPosition, (PV / PV0.Length()) * (Velocity.Length() - PL * (i - 1)), Width, Height, RealFall, false, gravDir);
			return NewPosition - Position;
		}


		///// <summary>
		///// 枚举有宽度的线上的物块，从上往下
		///// </summary>
		//public static IEnumerable<Point> EnumTilesInWideLine(Vector2 Start, Vector2 End, float Width)
		//{
		//	Vector2 v = Vector2.Normalize(End - Start).RotatedBy((float)Math.PI / 2) * Width / 2;
		//	return EnumTilesInConvexPolygon(Start + v, End + v, End - v, Start - v);
		//}

		public static IEnumerable<Point> EnumTilesInLine(Vector2 Start, Vector2 Offset) {
			var enumer = WackyBag.Utils.Calculate.EnumGridsInLine((Start / 16).ToSysVec(), (Offset / 16).ToSysVec(),new System.Drawing.Rectangle(0,0,Main.mapMaxX,Main.mapMaxY));
			return (IEnumerable<Point>)enumer;
		}

		/// <summary>
		/// 确定点是否在线的上方（世界上线的下方）
		/// </summary>
		public static bool PointAboveLine(Vector2 Point, Vector2 Start, Vector2 End)
		{
			Vector2 Unit = Vector2.Normalize(End - Start);
			Vector2 OffsetPoint = Point - Start;
			Vector2 PointXOnLine = Unit / Unit.X * OffsetPoint.X;
			return OffsetPoint.Y > PointXOnLine.Y;
		}
		/// <summary>
		/// 确定点是否在线的上方（世界上线的下方）
		/// </summary>
		public static bool PointAboveLine(Vector2 Point, Vector2 Line)
		{
			Vector2 Unit = Vector2.Normalize(Line);
			Vector2 OffsetPoint = Point;
			Vector2 PointXOnLine = Unit / Unit.X * OffsetPoint.X;
			return OffsetPoint.Y > PointXOnLine.Y;
		}

		/// <summary>
		/// 获取两个向量的夹角的cos值
		/// </summary>
		public static float AngleCos(Vector2 a, Vector2 b) => (Vector2.Dot(a, b)) / (a.Length() * b.Length());
		/// <summary>
		/// 获取两个向量的夹角的sin值
		/// </summary>
		public static float AngleSin(Vector2 a, Vector2 b) => CrossProduct(a, b) / (a.Length() * b.Length());

		public static double StdGaussian()
		{
			var rand = new Random();
			double u = -2 * Math.Log(rand.NextDouble());
			double v = 2 * Math.PI * rand.NextDouble();
			return Math.Sqrt(u) * Math.Cos(v);
		}
	}
}
