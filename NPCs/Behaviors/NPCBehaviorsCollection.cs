using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using WackyBagTr.Behaviors;

namespace WackyBagTr.NPCs.Behaviors
{
	public class NPCBehaviorsCollection<TBehavior> : BehaviorsCollection<TBehavior>, INPCBehavior
		where TBehavior : IBehavior,INPCBehavior
	{
		public void AddShops()
		{
			foreach (var item in ActiveBehaviors)
			{
				item.AddShops();
			}
		}

		public override void AI()
		{
			base.AI();
			foreach (var item in ActiveBehaviors)
			{
				item.AI();
			}
		}

		public void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.ApplyDifficultyAndPlayerScaling(numPlayers, bossAdjustment, bossAdjustment);
			}
		}

		public void BossHeadRotation(ref float rotation)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.BossHeadRotation(ref rotation);
			}
		}

		public void BossHeadSlot(ref int index)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.BossHeadSlot(ref index);
			}
		}

		public void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			foreach (var item in ActiveBehaviors)
			{
				item.BossHeadSpriteEffects(ref spriteEffects);
			}
		}

		public void BossLoot(ref string name, ref int potionType)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.BossLoot(ref name, ref potionType);
			}

		}

		public bool? CanBeCaughtBy(Item item, Player player)
		{

			bool? defValue = null;
			foreach (var item1 in ActiveBehaviors)
			{
				var res = item1.CanBeCaughtBy(item, player);
				if (res != defValue) return res;
			}
			return defValue;
		}

		public bool? CanBeHitByItem(Player player, Item item)
		{


			foreach (var item1 in ActiveBehaviors)
			{
				var res = item1.CanBeHitByItem(player, item);
				if (res != null) return res;
			}
			return null;


		}

		public bool CanBeHitByNPC(NPC attacker)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanBeHitByNPC(attacker);
				if (res != true) return res;
			}
			return true;

		}

		public bool? CanBeHitByProjectile(Projectile projectile)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanBeHitByProjectile(projectile);
				if (res != null) return res;
			}
			return null;

		}

		public bool CanChat()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanChat();
				if (res != false) return res;
			}
			return false;

		}

		public bool? CanCollideWithPlayerMeleeAttack(Player player, Item item, Rectangle meleeAttackHitbox)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				var res = item1.CanCollideWithPlayerMeleeAttack(player, item, meleeAttackHitbox);
				if (res != null) return res;
			}
			return null;

		}

		public bool? CanFallThroughPlatforms()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanFallThroughPlatforms();
				if (res != null) return res;
			}
			return null;

		}

		public bool CanGoToStatue(bool toKingStatue)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanGoToStatue(toKingStatue);
				if (res != false) return res;
			}
			return false;

		}

		public bool CanHitNPC(NPC target)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanHitNPC(target);
				if (res != true) return res;
			}
			return true;

		}

		public bool CanHitPlayer(Player target, ref int cooldownSlot)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanHitPlayer(target,	ref cooldownSlot);
				if (res != true) return res;
			}
			return true;

		}

		public bool CanTownNPCSpawn(int numTownNPCs)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CanTownNPCSpawn(numTownNPCs);
				if (res != false) return res;
			}
			return false;

		}

		public bool CheckActive()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CheckActive();
				if (res != true) return res;
			}
			return true;

		}

		public bool CheckConditions(int left, int right, int top, int bottom)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CheckConditions(left, right, top, bottom);
				if (res != true) return res;
			}
			return true;

		}

		public bool CheckDead()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.CheckDead();
				if (res != true) return res;
			}
			return true;

		}

		public void DrawBehind(int index)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.DrawBehind(index);
			}

		}

		public void DrawEffects(ref Color drawColor)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.DrawEffects(ref drawColor);
			}

		}

		public bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.DrawHealthBar(hbPosition, ref scale, ref position);
				if (res != null) return res;
			}
			return null;

		}

		public void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				item1.DrawTownAttackGun(ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
			}

		}

		public void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				item1.DrawTownAttackSwing(ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
			}

		}

		public void FindFrame(int frameHeight)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.FindFrame(frameHeight);
			}

		}

		public Color? GetAlpha(Color drawColor)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.GetAlpha(drawColor);
				if (res != null) return res;
			}
			return null;

		}

		public string GetChat()
		{
			StringBuilder sb = new();
			foreach (var item in ActiveBehaviors)
			{
				sb.Append(item.GetChat());
			}
			return sb.ToString();

		}

		public void HitEffect(NPC.HitInfo hit)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.HitEffect(hit);
			}

		}

		public void LoadData(TagCompound tag)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.LoadData(tag);
			}

		}

		public void ModifyActiveShop(string shopName, Item[] items)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyActiveShop(shopName, items);
			}

		}

		public bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.ModifyCollisionData(victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox);
				if (res != true) return res;
			}
			return true;

		}

		public void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				item1.ModifyHitByItem(player, item, ref modifiers);
			}

		}

		public void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyHitByProjectile(projectile, ref modifiers);
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

		public void ModifyHoverBoundingBox(ref Rectangle boundingBox)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyHoverBoundingBox(ref boundingBox);
			}

		}

		public void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyIncomingHit(ref modifiers);
			}

		}

		public void ModifyNPCLoot(NPCLoot npcLoot)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyNPCLoot(npcLoot);
			}

		}

		public void ModifyTypeName(ref string typeName)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ModifyTypeName(ref typeName);
			}

		}

		public bool NeedSaving()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.NeedSaving();
				if (res != false) return res;
			}
			return false;

		}

		public void OnCaughtBy(Player player, Item item, bool failed)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				item1.OnCaughtBy(player, item, failed);
			}

		}

		public void OnChatButtonClicked(bool firstButton, ref string shopName)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnChatButtonClicked(firstButton, ref shopName);
			}

		}

		public void OnGoToStatue(bool toKingStatue)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnGoToStatue(toKingStatue);
			}

		}

		public void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{

			foreach (var item1 in ActiveBehaviors)
			{
				item1.OnHitByItem(player, item, hit, damageDone);
			}

		}

		public void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnHitByProjectile(projectile, hit, damageDone);
			}

		}

		public void OnHitNPC(NPC target, NPC.HitInfo hit)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnHitNPC(target, hit);
			}

		}

		public void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnHitPlayer(target, hurtInfo);
			}

		}

		public void OnKill()
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnKill();
			}

		}

		public void OnSpawn(IEntitySource source)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.OnSpawn(source);
			}

		}

		public void PostAI()
		{

			foreach (var item in ActiveBehaviors)
			{
				item.PostAI();
			}

		}

		public void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.PostDraw(spriteBatch, screenPos, drawColor);
			}

		}

		public bool PreAI()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreAI();
				if (res != true) return res;
			}
			return true;

		}

		public bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreDraw(spriteBatch, screenPos, drawColor);
				if (res != true) return res;
			}
			return true;

		}

		public bool PreKill()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.PreKill();
				if (res != true) return res;
			}
			return true;

		}

		public void ResetEffects()
		{

			foreach (var item in ActiveBehaviors)
			{
				item.ResetEffects();
			}

		}

		public void SaveData(TagCompound tag)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.SaveData(tag);
			}

		}

		public void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.SetBestiary(database, bestiaryEntry);
			}

		}

		public void SetChatButtons(ref string button, ref string button2)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.SetChatButtons(ref button, ref button2);
			}

		}

		public void SetDefaults()
		{

			foreach (var item in ActiveBehaviors)
			{
				item.SetDefaults();
			}

		}

		public List<string> SetNPCNameList()
		{
			List<string> res = [];

			foreach (var item in ActiveBehaviors)
			{
				res.AddRange( item.SetNPCNameList());
			}
			return res;
		}

		public float SpawnChance(NPCSpawnInfo spawnInfo)
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.SpawnChance(spawnInfo);
				if (res != 0) return res;
			}
			return 0;

		}

		public bool SpecialOnKill()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.SpecialOnKill();
				if (res != false) return res;
			}
			return false;

		}

		public void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackCooldown(ref cooldown, ref randExtraCooldown);
			}

		}

		public void TownNPCAttackMagic(ref float auraLightMultiplier)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackMagic(ref auraLightMultiplier);
			}

		}

		public void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackProj(ref projType, ref attackDelay);
			}

		}

		public void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackProjSpeed(ref multiplier, ref gravityCorrection, ref randomOffset);
			}

		}

		public void TownNPCAttackShoot(ref bool inBetweenShots)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackShoot(ref inBetweenShots);
			}

		}

		public void TownNPCAttackStrength(ref int damage, ref float knockback)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackStrength(ref damage, ref knockback);
			}

		}

		public void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.TownNPCAttackSwing(ref itemWidth, ref itemHeight);
			}

		}

		public void UpdateLifeRegen(ref int damage)
		{

			foreach (var item in ActiveBehaviors)
			{
				item.UpdateLifeRegen(ref damage);
			}

		}

		public bool UsesPartyHat()
		{

			foreach (var item in ActiveBehaviors)
			{
				var res = item.UsesPartyHat();
				if (res != true) return res;
			}
			return true;

		}
	}
}
