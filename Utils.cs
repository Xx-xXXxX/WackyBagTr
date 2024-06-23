using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using Terraria;
using WackyBagTr.Behaviors;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework;

using static WackyBagTr.Utilties.Calculates;

namespace WackyBagTr
{
	public static class Utils
	{
		/*
		/// <summary>
		/// when a new player is joined, need update anything
		/// </summary>
		/// <returns></returns>
		public static bool NeedNetInitToNewPlayer()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				foreach (var i in Netplay.Clients)
				{
					if (i != null && i.IsActive && i.State == 3)
					{
						return true;//存在需要同步的端
					}
				}
			}
			return false;
		}
		*/

		public static bool TrySetActive(this IBehavior behavior, bool active)
		{
			if (behavior.Active == active) return false;
			else if (behavior.Active && !active)
			{
				//if (!behavior.CanPause()) return false;
				behavior.Pause();
				//behavior.Active = false;
				return true;
			}
			else
			{
				//if (!behavior.CanActivate()) return false;
				behavior.Activate();
				//behavior.Active = true;
				return true;
			}
		}

		public static TBehavior? Warp<TBehavior>(this TBehavior behavior)
			where TBehavior : class,IBehavior
		{ 
			return behavior.Active ? behavior : null;
		}

		[Obsolete]
		private static void ProjectileHitNPC(Projectile proj, int npcId) {
		
			// second 200

			///* 0x00058CAE 1109         */ IL_462A: ldloc.s V_9
			///* 0x00058CB0 20C8000000   */ IL_462C: ldc.i4    200
			///* 0x00058CB5 FE04         */ IL_4631: clt
			///* 0x00058CB7 1107         */ IL_4633: ldloc.s V_7
			///* 0x00058CB9 5F           */ IL_4635: and
			///* 0x00058CBA 3AECC3FFFF   */ IL_4636: brtrue IL_0A27
			 
		}

		

		public static Texture2D GetTexture(this Projectile proj)
		{
			Main.instance.LoadProjectile(proj.type);
			return TextureAssets.Projectile[proj.type].Value;
		}
		public static void BetterDrawProj(Projectile Projectile, Color lightColor)
		{
			Texture2D texture = GetTexture(Projectile);
			Vector2 drawOrigin = texture.Size() / 2;
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
		}

		/// <summary>
		/// 将n限制在[l,r)
		/// </summary>
		public static int Limit(int n, int l, int r)
		{
			if (n < l) n = l;
			if (n >= r) n = r;
			return n;
		}
		/// <summary>
		/// 返回n在[l,r)中循环的结果
		/// </summary>
		public static int LimitLoop(int n, int l, int r)
		{
			int d = r - l;
			n = ((n - l) % d) + l;
			if (n < l) n += d;
			return n;
		}
		/// <summary>
		/// 返回n在[l,r)中循环的结果
		/// </summary>
		public static double LimitLoop(double n, double l, double r)
		{
			double d = r - l;
			n = ((n - l) % d) + l;
			if (n < l) n += d;
			return n;
		}
		/// <summary>
		/// 返回n在[l,r)中循环的结果
		/// </summary>
		public static float LimitLoop(float n, float l, float r)
		{
			float d = r - l;
			n = ((n - l) % d) + l;
			if (n < l) n += d;
			return n;
		}

		/// <summary>
		/// 在其他边不变的情况下设置左边界
		/// </summary>
		public static Rectangle SetLeft(this Rectangle rect, int L)
		{
			rect.Width += L - rect.X;
			rect.X = L;
			return rect;
		}
		/// <summary>
		/// 在其他边不变的情况下设置右边界
		/// </summary>
		public static Rectangle SetRight(this Rectangle rect, int R)
		{
			rect.Width = R - rect.X; return rect;
		}
		/// <summary>
		/// 在其他边不变的情况下设置上边界
		/// </summary>
		public static Rectangle SetTop(this Rectangle rect, int T)
		{
			rect.Height += T - rect.Y;
			rect.Y = T;
			return rect;
		}
		/// <summary>
		/// 在其他边不变的情况下设置下边界
		/// </summary>
		public static Rectangle SetBottom(this Rectangle rect, int B)
		{
			rect.Height = B - rect.X; return rect;
		}
		/// <summary>
		/// 在右下角不变的情况下设置左上角
		/// </summary>
		public static Rectangle SetLT(this Rectangle rect, Point point)
		{
			rect.SetLeft(point.X);
			rect.SetTop(point.Y);
			return rect;
		}
		/// <summary>
		/// 在左上角不变的情况下设置右下角
		/// </summary>
		public static Rectangle SetRB(this Rectangle rect, Point point)
		{
			rect.SetRight(point.X);
			rect.SetBottom(point.Y);
	
			return rect;
		}
		/// <summary>
		/// 移动
		/// </summary>
		public static Rectangle MoveBy(this Rectangle rect, Point point)
		{
			rect.X += point.X;
			rect.Y += point.Y;
			return rect;
		}
		/// <summary>
		/// 获取两个向量的夹角的cos值
		/// </summary>
		public static float AngleCos(Vector2 a, Vector2 b) => (Vector2.Dot(a, b)) / (a.Length() * b.Length());
		/// <summary>
		/// 获取两个向量的夹角的sin值
		/// </summary>
		public static float AngleSin(Vector2 a, Vector2 b) => CrossProduct(a, b) / (a.Length() * b.Length());

		/// <summary>
		/// 设置Vector2的长度
		/// </summary>
		public static Vector2 WithLength(this Vector2 vector, float Length)
		{
			return vector *= (Length / vector.Length());
		}

		public static Vector2 WithRotation(this Vector2 vector, float angle)
		{
			float length = vector.Length();
			return angle.ToRotationVector2() * length;
		}
		public static Vector2 WithRotation(this Vector2 vector, Vector2 angle)
		{
			float length = vector.Length();
			return angle.WithLength(length);
		}
		/// <summary>
		/// 保持entity在世界中（否则会直接被active=false或报错）
		/// </summary>
		/// <param name="entity"></param>
		public static void SetInWorld(Entity entity)
		{

			if (entity.position.Y + entity.velocity.Y < 16)
				entity.position.Y = 16 - entity.velocity.Y;

			if (entity.position.X + entity.velocity.X < 16)
				entity.position.X = 16 - entity.velocity.X;

			if (entity.position.Y + entity.height + entity.velocity.Y > Main.maxTilesY * 16 - 16)
				entity.position.Y = Main.maxTilesY * 16 - 16 - entity.velocity.Y - entity.height;

			if (entity.position.X + entity.width + entity.velocity.X > Main.maxTilesX * 16 - 16)
				entity.position.X = Main.maxTilesX * 16 - 16 - entity.velocity.X - entity.width;
		}
		public static Microsoft.Xna.Framework.Vector2 ToXnaVec(this System.Numerics.Vector2 vec)
			=> new(vec.X, vec.Y);
		public static System.Numerics.Vector2 ToSysVec(this Microsoft.Xna.Framework.Vector2 vec)
			=> new(vec.X, vec.Y);
		public static float SmoothUp_0_Inf_to_0_1(float x) {
			return 2 * MathF.Atan(x) / MathF.PI;
		}
		public static float SmoothUp_0_Inf_to_0_Inf(float x)
		{
			return MathF.Log(x+1);
		}

		public static float Smooth_Neg1_1_to_NegInf_Inf(float x) {
			return MathF.Atan(x * MathHelper.PiOver2);
		}
		public static Entity? GetTarget(this NPC npc) {
			if (npc.HasPlayerTarget)
			{
				return Main.player[npc.target];
			}
			else if (npc.HasNPCTarget) {
				return Main.npc[npc.target - 300];
			}
			return null;
		}

		public static float ValueMoveTo(float from, float to, float step) {
			if (from < to)
			{
				if (to - from < step) return to;
				return from + step;
			}
			else {
				if (from - to < step) return to;
				return from - step;
			}

		}

		public static int RandIntoInt(float value) { 
			int a=(int)MathF.Floor(value);
			if(Main.rand.NextFloat()<value-a) a ++;
			return a;
		}
	}
}
