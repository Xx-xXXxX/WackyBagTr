using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace WackyBagTr.Projectiles.Behaviors
{
	public abstract class BehaviorModifiedProjectile: ModProjectile//,IProjectileBehavior//
																						  //where TProjectile : ModProjectile
	{
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		public IProjectileBehavior ProjBehavior { get; protected set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		public abstract (IProjectileBehavior,Action) CtorBehavior();


		public override void AI()
		{
			ProjBehavior.Warp()?.AI();
		}

		public override bool? CanDamage()
		{
			return ProjBehavior.Warp()?.CanDamage();
		}

		public override bool? CanHitNPC(NPC target)
		{
			return ProjBehavior.Warp()?.CanHitNPC(target);
		}

		public override bool CanHitPlayer(Player target)
		{
			return ProjBehavior.Warp()?.CanHitPlayer(target)?? true;
		}

		public override bool CanHitPvp(Player target)
		{
			return ProjBehavior.Warp()?.CanHitPvp(target) ?? true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return ProjBehavior.Warp()?.Colliding(projHitbox, targetHitbox);
		}

		public override void CutTiles()
		{
			ProjBehavior.Warp()?.CutTiles();
		}


		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			ProjBehavior.Warp()?.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return ProjBehavior.Warp()?.GetAlpha(lightColor);
		}


		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			ProjBehavior.Warp()?.ModifyDamageHitbox(ref hitbox);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			ProjBehavior.Warp()?.ModifyHitNPC(target, ref modifiers);
		}

		public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			ProjBehavior.Warp()?.ModifyHitPlayer(target, ref modifiers);
		}


		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			ProjBehavior.Warp()?.OnHitNPC(target, hit, damageDone);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			ProjBehavior.Warp()?.OnHitPlayer(target, info);
		}

		public override void OnKill(int timeLeft)
		{
			ProjBehavior.Warp()?.OnKill(timeLeft);
			ProjBehavior.Warp()?.Pause();
			ProjBehavior.Dispose();
		}

		public override void OnSpawn(IEntitySource source)
		{
			ProjBehavior.Warp()?.OnSpawn(source);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return ProjBehavior.Warp()?.OnTileCollide(oldVelocity) ?? true;
		}

		public override void PostAI()
		{
			ProjBehavior.Warp()?.PostAI();
		}

		public override void PostDraw(Color lightColor)
		{
			ProjBehavior.Warp()?.PostDraw(lightColor);
		}

		public override bool PreAI()
		{
			return ProjBehavior.Warp()?.PreAI() ?? true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return ProjBehavior.Warp()?.PreDraw(ref lightColor) ?? true;
		}

		public override bool PreDrawExtras()
		{
			return ProjBehavior.Warp()?.PreDrawExtras() ?? true;
		}

		public override bool PreKill(int timeLeft)
		{
			return ProjBehavior.Warp()?.PreKill(timeLeft) ?? true;
		}

		public override void SetDefaults()
		{
			Action action;
			(ProjBehavior, action) = CtorBehavior();
			ProjBehavior.Initialize();
			ProjBehavior.Activate();
			action?.Invoke();
			ProjBehavior.Warp()?.SetDefaults();
		}

		public override bool ShouldUpdatePosition()
		{
			return ProjBehavior.Warp()?.ShouldUpdatePosition() ?? true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			return ProjBehavior.Warp()?.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac) ?? true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			//base.SendExtraAI(writer);
			ProjBehavior.Warp()?.SendExtraAI(writer);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			ProjBehavior.Warp()?.ReciveExtraAI(reader);
		}
	}
}
