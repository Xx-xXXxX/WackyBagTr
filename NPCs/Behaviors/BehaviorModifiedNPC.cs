using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using WackyBagTr.Projectiles.Behaviors;

namespace WackyBagTr.NPCs.Behaviors
{
	public abstract class BehaviorModifiedNPC:ModNPC
	{

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		public INPCBehavior NPCBehavior { get; protected set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。



		public abstract (INPCBehavior,Action) CtorBehavior();

		public override void AddShops()
		{
			NPCBehavior.Warp()?.AddShops();
		}

		public override void AI()
		{
			base.AI();
			NPCBehavior.Warp()?.AI();
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPCBehavior.Warp()?.ApplyDifficultyAndPlayerScaling(numPlayers, balance, bossAdjustment);
		}

		public override void BossHeadRotation(ref float rotation)
		{
			NPCBehavior.Warp()?.BossHeadRotation(ref rotation);
		}

		public override void BossHeadSlot(ref int index)
		{
			NPCBehavior.Warp()?.BossHeadSlot(ref index);
		}

		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			NPCBehavior.Warp()?.BossHeadSpriteEffects(ref spriteEffects);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			NPCBehavior.Warp()?.BossLoot(ref name, ref potionType);
		}

		public object? Call(params object[] vs)
		{
			return NPCBehavior.Warp()?.Call(vs);
		}

		public override bool? CanBeCaughtBy(Item item, Player player)
		{
			return NPCBehavior.Warp()?.CanBeCaughtBy(item, player);
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return NPCBehavior.Warp()?.CanBeHitByItem(player, item);
		}

		public override bool CanBeHitByNPC(NPC attacker)
		{
			return NPCBehavior.Warp()?.CanBeHitByNPC(attacker)??true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return NPCBehavior.Warp()?.CanBeHitByProjectile(projectile);
		}

		public override bool CanChat()
		{
			return NPCBehavior.Warp()?.CanChat() ?? NPC.townNPC;
		}

		public override bool? CanCollideWithPlayerMeleeAttack(Player player, Item item, Rectangle meleeAttackHitbox)
		{
			return NPCBehavior.Warp()?.CanCollideWithPlayerMeleeAttack(player, item, meleeAttackHitbox);
		}

		public override bool? CanFallThroughPlatforms()
		{
			return NPCBehavior.Warp()?.CanFallThroughPlatforms();
		}

		public override bool CanGoToStatue(bool toKingStatue)
		{
			return NPCBehavior.Warp()?.CanGoToStatue(toKingStatue) ?? false;
		}

		public override bool CanHitNPC(NPC target)
		{
			return NPCBehavior.Warp()?.CanHitNPC(target)?? true;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return NPCBehavior.Warp()?.CanHitPlayer(target, ref cooldownSlot) ?? true;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			return NPCBehavior.Warp()?.CanTownNPCSpawn(numTownNPCs) ?? false;
		}

		public override bool CheckActive()
		{
			return NPCBehavior.Warp()?.CheckActive() ?? true;
		}

		public override bool CheckConditions(int left, int right, int top, int bottom)
		{
			return NPCBehavior.Warp()?.CheckConditions(left, right, top, bottom) ?? true;
		}

		public override bool CheckDead()
		{
			return NPCBehavior.Warp()?.CheckDead() ?? true;
		}


		public void Dispose()
		{
			NPCBehavior.Warp()?.Dispose();
		}

		public override void DrawBehind(int index)
		{
			NPCBehavior.Warp()?.DrawBehind(index);
		}

		public override void DrawEffects(ref Color drawColor)
		{
			NPCBehavior.Warp()?.DrawEffects(ref drawColor);
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return NPCBehavior.Warp()?.DrawHealthBar(hbPosition, ref scale, ref position);
		}

		public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{
			NPCBehavior.Warp()?.DrawTownAttackGun(ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			NPCBehavior.Warp()?.DrawTownAttackSwing(ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
		}

		public override void FindFrame(int frameHeight)
		{
			NPCBehavior.Warp()?.FindFrame(frameHeight);
		}

		public override Color? GetAlpha(Color drawColor)
		{
			return NPCBehavior.Warp()?.GetAlpha(drawColor);
		}

		public override string GetChat()
		{
			return NPCBehavior.Warp()?.GetChat()??"";
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			NPCBehavior.Warp()?.HitEffect(hit);
		}

		public void Initialize()
		{
			NPCBehavior.Warp()?.Initialize();
		}

		public override void LoadData(TagCompound tag)
		{
			NPCBehavior.Warp()?.LoadData(tag);
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
			NPCBehavior.Warp()?.ModifyActiveShop(shopName, items);
		}

		public override bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
		{
			return NPCBehavior.Warp()?.ModifyCollisionData(victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox) ?? true;
		}

		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			NPCBehavior.Warp()?.ModifyHitByItem(player, item, ref modifiers);
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
			NPCBehavior.Warp()?.ModifyHitByProjectile(projectile, ref modifiers);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			NPCBehavior.Warp()?.ModifyHitNPC(target, ref modifiers);
		}

		public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
			NPCBehavior.Warp()?.ModifyHitPlayer(target, ref modifiers);
		}

		public override void ModifyHoverBoundingBox(ref Rectangle boundingBox)
		{
			NPCBehavior.Warp()?.ModifyHoverBoundingBox(ref boundingBox);
		}

		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			NPCBehavior.Warp()?.ModifyIncomingHit(ref modifiers);
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			NPCBehavior.Warp()?.ModifyNPCLoot(npcLoot);
		}

		public override void ModifyTypeName(ref string typeName)
		{
			NPCBehavior.Warp()?.ModifyTypeName(ref typeName);
		}

		public override bool NeedSaving()
		{
			return NPCBehavior.Warp()?.NeedSaving() ?? false;
		}

		public override void OnCaughtBy(Player player, Item item, bool failed)
		{
			NPCBehavior.Warp()?.OnCaughtBy(player, item, failed);
		}

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			NPCBehavior.Warp()?.OnChatButtonClicked(firstButton, ref shopName);
		}

		public override void OnGoToStatue(bool toKingStatue)
		{
			NPCBehavior.Warp()?.OnGoToStatue(toKingStatue);
		}

		public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
			NPCBehavior.Warp()?.OnHitByItem(player, item, hit, damageDone);
		}

		public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			NPCBehavior.Warp()?.OnHitByProjectile(projectile, hit, damageDone);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit)
		{
			NPCBehavior.Warp()?.OnHitNPC(target, hit);
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			NPCBehavior.Warp()?.OnHitPlayer(target, hurtInfo);
		}

		public override void OnKill()
		{
			NPCBehavior.Warp()?.OnKill();
			NPCBehavior.Warp()?.Pause();
			NPCBehavior.Dispose();
		}

		public override void OnSpawn(IEntitySource source)
		{
			NPCBehavior.Warp()?.OnSpawn(source);
		}

		public override void PostAI()
		{
			NPCBehavior.Warp()?.PostAI();
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			NPCBehavior.Warp()?.PostDraw(spriteBatch, screenPos, drawColor);
		}

		public override bool PreAI()
		{
			return NPCBehavior.Warp()?.PreAI() ?? true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			return NPCBehavior.Warp()?.PreDraw(spriteBatch, screenPos, drawColor) ?? true;
		}

		public override bool PreKill()
		{
			return NPCBehavior.Warp()?.PreKill() ?? true;
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			NPCBehavior.Warp()?.ReciveExtraAI(reader);
		}

		public override void ResetEffects()
		{
			NPCBehavior.Warp()?.ResetEffects();
		}

		public override void SaveData(TagCompound tag)
		{
			NPCBehavior.Warp()?.SaveData(tag);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			NPCBehavior.Warp()?.SendExtraAI(writer);
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			NPCBehavior.Warp()?.SetBestiary(database, bestiaryEntry);
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			NPCBehavior.Warp()?.SetChatButtons(ref button, ref button2);
		}

		/// <summary>
		/// <inheritdoc/>
		/// ctor, init, activate, setdefaults
		/// </summary>
		public override void SetDefaults()
		{
			Action action;
			(NPCBehavior, action) = CtorBehavior();
			NPCBehavior.Initialize();
			NPCBehavior.Activate();
			action();
			NPCBehavior.Warp()?.SetDefaults();
		}

		public override List<string> SetNPCNameList()
		{
			return NPCBehavior.Warp()?.SetNPCNameList()??[];
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return NPCBehavior.Warp()?.SpawnChance(spawnInfo) ?? 0;
		}

		public override bool SpecialOnKill()
		{
			return NPCBehavior.Warp()?.SpecialOnKill() ?? false;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			NPCBehavior.Warp()?.TownNPCAttackCooldown(ref cooldown, ref randExtraCooldown);
		}

		public override void TownNPCAttackMagic(ref float auraLightMultiplier)
		{
			NPCBehavior.Warp()?.TownNPCAttackMagic(ref auraLightMultiplier);
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			NPCBehavior.Warp()?.TownNPCAttackProj(ref projType, ref attackDelay);
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			NPCBehavior.Warp()?.TownNPCAttackProjSpeed(ref multiplier, ref gravityCorrection, ref randomOffset);
		}

		public override void TownNPCAttackShoot(ref bool inBetweenShots)
		{
			NPCBehavior.Warp()?.TownNPCAttackShoot(ref inBetweenShots);
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			NPCBehavior.Warp()?.TownNPCAttackStrength(ref damage, ref knockback);
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			NPCBehavior.Warp()?.TownNPCAttackSwing(ref itemWidth, ref itemHeight);
		}

		public override void UpdateLifeRegen(ref int damage)
		{
			NPCBehavior.Warp()?.UpdateLifeRegen(ref damage);
		}

		public override bool UsesPartyHat()
		{
			return NPCBehavior.Warp()?.UsesPartyHat() ?? true;
		}
	}
}
