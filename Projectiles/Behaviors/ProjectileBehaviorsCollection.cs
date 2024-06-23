using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;

using WackyBagTr.Behaviors;

namespace WackyBagTr.Projectiles.Behaviors
{
	public class ProjectileBehaviorsCollection<TBehavior> : BehaviorsCollection<TBehavior>, IProjectileBehavior
		where TBehavior : IProjectileBehavior,IBehavior
	{
		public bool? CanDamage()
		{
			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanDamage();
				if(res!=null)return res;
			}
			return null;
		}

		public bool? CanHitNPC(NPC target)
		{
			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanHitNPC(target);
				if (res != null) return res;
			}
			return null;
		}

		public bool CanHitPlayer(Player target)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanHitPlayer(target);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool CanHitPvp(Player target)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanHitPvp(target);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			bool? defValue = null;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.Colliding(projHitbox, targetHitbox);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public void CutTiles()
		{
			foreach (var item in ActiveBehaviors)
			{
				item.CutTiles();
			}
		}

		public void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles,  overPlayers,  overWiresUI);
			}
		}

		public Color? GetAlpha(Color lightColor)
		{
			Color? defValue = null;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.GetAlpha(lightColor);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.ModifyDamageHitbox(ref hitbox);
			}
		}

		public void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.ModifyHitNPC(target, ref modifiers);
			}
		}

		public void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.ModifyHitPlayer(target, ref modifiers);
			}
		}

		public void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.OnHitNPC(target, hit, damageDone);
			}
		}

		public void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.OnHitPlayer(target, info);
			}
		}

		public void OnKill(int timeLeft)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.OnKill(timeLeft);
			}
			//this.Dispose();
		}

		public void OnSpawn(IEntitySource source)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.OnSpawn(source);
			}
		}

		public bool OnTileCollide(Vector2 oldVelocity)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.OnTileCollide(oldVelocity);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public void PostAI()
		{
			foreach (var item in ActiveBehaviors)
			{
				item.PostAI();
			}
		}

		public void PostDraw(Color lightColor)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.PostDraw(lightColor);
			}
		}

		public bool PreAI()
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreAI();
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool PreDraw(ref Color lightColor)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreDraw(ref lightColor);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool PreDrawExtras()
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreDrawExtras();
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool PreKill(int timeLeft)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreKill(timeLeft);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public void SetDefaults()
		{
			//base.SetDefaults();
			foreach (var item in ActiveBehaviors)
			{
				item.SetDefaults();
			}
		}

		public bool ShouldUpdatePosition()
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.ShouldUpdatePosition();
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			var defValue = true;
			foreach (var item in ActiveBehaviors)
			{
				var res = item.TileCollideStyle(ref height, ref height, ref fallThrough, ref hitboxCenterFrac);
				if (res != defValue) return res;
			}
			return defValue;
		}
	}
}
