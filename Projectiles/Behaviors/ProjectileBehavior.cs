using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

using WackyBagTr.Behaviors;

namespace WackyBagTr.Projectiles.Behaviors
{
	public interface IProjectileBehavior:IBehavior
	{
		public virtual void SetDefaults() { }

		/// <summary>
		/// Gets called when your projectiles spawns in world.<br/>
		/// Called on the client or server spawning the projectile via Projectile.NewProjectile.<br/>
		/// </summary>
		public virtual void OnSpawn(IEntitySource source)
		{
		}


		/// <summary>
		/// Allows you to determine how this projectile behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
		/// </summary>
		/// <returns>Whether or not to stop other AI.</returns>
		public virtual bool PreAI()
		{
			return true;
		}
		/*
		/// <summary>
		/// Allows you to determine how this projectile behaves. This will only be called if PreAI returns true.
		/// <br/> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#custom-ai">Basic Projectile Guide</see> teaches the basics of writing a custom AI, such as timers, gravity, rotation, etc.
		/// </summary>
		public virtual void AI()
		{

		}*/

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will be called regardless of what PreAI returns.
		/// </summary>
		public virtual void PostAI()
		{
		}

		/// <summary>
		/// Whether or not this projectile should update its position based on factors such as its velocity, whether it is in liquid, etc. Return false to make its velocity have no effect on its position. Returns true by default.
		/// </summary>
		public virtual bool ShouldUpdatePosition()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this projectile interacts with tiles. Return false if you completely override or cancel this projectile's tile collision behavior. Returns true by default.
		/// </summary>
		/// <param name="width"> The width of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.width. </param>
		/// <param name="height"> The height of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.height. </param>
		/// <param name="fallThrough"> Whether or not this projectile falls through platforms and similar tiles. </param>
		/// <param name="hitboxCenterFrac"> Determines by how much the tile collision hitbox's position (top left corner) will be offset from this projectile's real center. If vanilla doesn't modify it, defaults to half the hitbox size (new Vector2(0.5f, 0.5f)). </param>
		/// <returns></returns>
		public virtual bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when this projectile collides with a tile. OldVelocity is the velocity before tile collision. The velocity that takes tile collision into account can be found with Projectile.velocity. Return true to allow the vanilla tile collision code to take place (which normally kills the projectile). Returns true by default.
		/// </summary>
		/// <param name="oldVelocity">The velocity of the projectile upon collision.</param>
		public virtual bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
		/*
		/// <summary>
		/// Return true or false to specify if the projectile can cut tiles like vines, pots, and Queen Bee larva. Return null for vanilla decision.
		/// </summary>
		public virtual bool? CanCutTiles()
		{
			return null;
		}
		*/

		/// <summary>
		/// Code ran when the projectile cuts tiles. Only runs if CanCutTiles() returns true. Useful when programming lasers and such.
		/// </summary>
		public virtual void CutTiles()
		{
		}

		/// <summary>
		/// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
		/// </summary>
		public virtual bool PreKill(int timeLeft)
		{
			return true;
		}

		public virtual void OnKill(int timeLeft) { }

		/// <summary>
		/// Whether or not this projectile is capable of killing tiles (such as grass) and damaging NPCs/players.
		/// Return false to prevent it from doing any sort of damage.
		/// Return true if you want the projectile to do damage regardless of the default blacklist.
		/// Return null to let the projectile follow vanilla can-damage-anything rules. This is what happens by default.
		/// </summary>
		public virtual bool? CanDamage()
		{
			return null;
		}
		/*
		/// <summary>
		/// Whether or not this minion can damage NPCs by touching them. Returns false by default. Note that this will only be used if this projectile is considered a pet.
		/// </summary>
		public virtual bool MinionContactDamage()
		{
			return false;
		}*/

		/// <summary>
		/// Allows you to change the hitbox used by this projectile for damaging players and NPCs.
		/// </summary>
		/// <param name="hitbox"></param>
		public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given NPC. Return true to allow hitting the target, return false to block this projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public virtual bool? CanHitNPC(NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this projectile does to an NPC. This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers">The modifiers for this strike.</param>
		public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this projectile hits an NPC (for example, inflicting debuffs). This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="hit">The damage.</param>
		/// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
		public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given opponent player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target</param>
		public virtual bool CanHitPvp(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether this hostile projectile can hit the given player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public virtual bool CanHitPlayer(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that this hostile projectile does to a player.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers"></param>
		public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this hostile projectile hits a player. <br/>
		/// Only runs on the local client in multiplayer.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="info"></param>
		public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
		{
		}

		/// <summary>
		/// Allows you to use custom collision detection between this projectile and a player or NPC that this projectile can damage. Useful for things like diagonal lasers, projectiles that leave a trail behind them, etc.
		/// </summary>
		/// <param name="projHitbox">The hitbox of the projectile.</param>
		/// <param name="targetHitbox">The hitbox of the target.</param>
		/// <returns>Whether they collide or not.</returns>
		public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return null;
		}


		/// <summary>
		/// Allows you to determine the color and transparency in which this projectile is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// </summary>
		public virtual Color? GetAlpha(Color lightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile. Use the Main.EntitySpriteDraw method for drawing. Returns false to stop the game from drawing extras textures related to the projectile (for example, the chains for grappling hooks), useful if you're manually drawing the extras. Returns true by default.
		/// </summary>
		public virtual bool PreDrawExtras()
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile, or to modify the way it is drawn. Use the Main.EntitySpriteDraw method for drawing. Return false to stop the vanilla projectile drawing code (useful if you're manually drawing the projectile). Returns true by default.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center. </param>
		public virtual bool PreDraw(ref Color lightColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this projectile. Use the Main.EntitySpriteDraw method for drawing. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center, after being modified by vanilla and other mods. </param>
		public virtual void PostDraw(Color lightColor)
		{
		}

		/// <summary>
		/// When used in conjunction with "Projectile.hide = true", allows you to specify that this projectile should be drawn behind certain elements. Add the index to one and only one of the lists. For example, the Nebula Arcanum projectile draws behind NPCs and tiles.
		/// </summary>
		public virtual void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
		}
	}

	public abstract class ProjectileBehavior<TModProjectile>(TModProjectile ModProjectile) : Behavior, IProjectileBehavior
		where TModProjectile:ModProjectile
	{
		public TModProjectile ModProjectile { get; } = ModProjectile;
		public Projectile Projectile => ModProjectile.Projectile;
		/*
		/// <summary>
		/// Allows you to set all your projectile's properties, such as width, damage, aiStyle, penetrate, etc.
		/// </summary>
		public virtual void SetDefaults()
		{
			//base.SetDefaults();
		}

		/// <summary>
		/// Gets called when your projectiles spawns in world.<br/>
		/// Called on the client or server spawning the projectile via Projectile.NewProjectile.<br/>
		/// </summary>
		public virtual void OnSpawn(IEntitySource source)
		{
		}


		/// <summary>
		/// Allows you to determine how this projectile behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
		/// </summary>
		/// <returns>Whether or not to stop other AI.</returns>
		public virtual bool PreAI()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will only be called if PreAI returns true.
		/// <br/> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#custom-ai">Basic Projectile Guide</see> teaches the basics of writing a custom AI, such as timers, gravity, rotation, etc.
		/// </summary>
		public virtual void AI()
		{
			
		}

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will be called regardless of what PreAI returns.
		/// </summary>
		public virtual void PostAI()
		{
		}

		/// <summary>
		/// Whether or not this projectile should update its position based on factors such as its velocity, whether it is in liquid, etc. Return false to make its velocity have no effect on its position. Returns true by default.
		/// </summary>
		public virtual bool ShouldUpdatePosition()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this projectile interacts with tiles. Return false if you completely override or cancel this projectile's tile collision behavior. Returns true by default.
		/// </summary>
		/// <param name="width"> The width of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.width. </param>
		/// <param name="height"> The height of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.height. </param>
		/// <param name="fallThrough"> Whether or not this projectile falls through platforms and similar tiles. </param>
		/// <param name="hitboxCenterFrac"> Determines by how much the tile collision hitbox's position (top left corner) will be offset from this projectile's real center. If vanilla doesn't modify it, defaults to half the hitbox size (new Vector2(0.5f, 0.5f)). </param>
		/// <returns></returns>
		public virtual bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when this projectile collides with a tile. OldVelocity is the velocity before tile collision. The velocity that takes tile collision into account can be found with Projectile.velocity. Return true to allow the vanilla tile collision code to take place (which normally kills the projectile). Returns true by default.
		/// </summary>
		/// <param name="oldVelocity">The velocity of the projectile upon collision.</param>
		public virtual bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
		
		/// <summary>
		/// Return true or false to specify if the projectile can cut tiles like vines, pots, and Queen Bee larva. Return null for vanilla decision.
		/// </summary>
		public virtual bool? CanCutTiles()
		{
			return null;
		}
		

		/// <summary>
		/// Code ran when the projectile cuts tiles. Only runs if CanCutTiles() returns true. Useful when programming lasers and such.
		/// </summary>
		public virtual void CutTiles()
		{
		}

		/// <summary>
		/// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
		/// </summary>
		public virtual bool PreKill(int timeLeft)
		{
			return true;
		}

		/// <summary>
		/// Allows you to control what happens when this projectile is killed (for example, creating dust or making sounds). Also useful for creating retrievable ammo. Called on all clients and the server in multiplayer, so be sure to use `if (Projectile.owner == Main.myPlayer)` if you are spawning retrievable ammo. (As seen in ExampleJavelinProjectile)
		/// </summary>
		public virtual void OnKill(int timeLeft)
		{
		}


		/// <summary>
		/// Whether or not this projectile is capable of killing tiles (such as grass) and damaging NPCs/players.
		/// Return false to prevent it from doing any sort of damage.
		/// Return true if you want the projectile to do damage regardless of the default blacklist.
		/// Return null to let the projectile follow vanilla can-damage-anything rules. This is what happens by default.
		/// </summary>
		public virtual bool? CanDamage()
		{
			return null;
		}

		/// <summary>
		/// Allows you to change the hitbox used by this projectile for damaging players and NPCs.
		/// </summary>
		/// <param name="hitbox"></param>
		public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given NPC. Return true to allow hitting the target, return false to block this projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public virtual bool? CanHitNPC(NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this projectile does to an NPC. This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers">The modifiers for this strike.</param>
		public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this projectile hits an NPC (for example, inflicting debuffs). This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="hit">The damage.</param>
		/// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
		public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given opponent player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target</param>
		public virtual bool CanHitPvp(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether this hostile projectile can hit the given player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public virtual bool CanHitPlayer(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that this hostile projectile does to a player.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers"></param>
		public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this hostile projectile hits a player. <br/>
		/// Only runs on the local client in multiplayer.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="info"></param>
		public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
		{
		}

		/// <summary>
		/// Allows you to use custom collision detection between this projectile and a player or NPC that this projectile can damage. Useful for things like diagonal lasers, projectiles that leave a trail behind them, etc.
		/// </summary>
		/// <param name="projHitbox">The hitbox of the projectile.</param>
		/// <param name="targetHitbox">The hitbox of the target.</param>
		/// <returns>Whether they collide or not.</returns>
		public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return null;
		}


		/// <summary>
		/// Allows you to determine the color and transparency in which this projectile is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// </summary>
		public virtual Color? GetAlpha(Color lightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile. Use the Main.EntitySpriteDraw method for drawing. Returns false to stop the game from drawing extras textures related to the projectile (for example, the chains for grappling hooks), useful if you're manually drawing the extras. Returns true by default.
		/// </summary>
		public virtual bool PreDrawExtras()
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile, or to modify the way it is drawn. Use the Main.EntitySpriteDraw method for drawing. Return false to stop the vanilla projectile drawing code (useful if you're manually drawing the projectile). Returns true by default.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center. </param>
		public virtual bool PreDraw(ref Color lightColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this projectile. Use the Main.EntitySpriteDraw method for drawing. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center, after being modified by vanilla and other mods. </param>
		public virtual void PostDraw(Color lightColor)
		{
		}

		/// <summary>
		/// When used in conjunction with "Projectile.hide = true", allows you to specify that this projectile should be drawn behind certain elements. Add the index to one and only one of the lists. For example, the Nebula Arcanum projectile draws behind NPCs and tiles.
		/// </summary>
		public virtual void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
		}*/
	}

	public interface IProjectileBehaviorMarked : IBehavior
	{
		/// <summary>
		/// Allows you to set all your projectile's properties, such as width, damage, aiStyle, penetrate, etc.
		/// </summary>
		public void SetDefaults()
		;

		/// <summary>
		/// Gets called when your projectiles spawns in world.<br/>
		/// Called on the client or server spawning the projectile via Projectile.NewProjectile.<br/>
		/// </summary>
		public void OnSpawn(IEntitySource source)
		;


		/// <summary>
		/// Allows you to determine how this projectile behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
		/// </summary>
		/// <returns>Whether or not to stop other AI.</returns>
		public bool PreAI()
		;
		/*
		/// <summary>
		/// Allows you to determine how this projectile behaves. This will only be called if PreAI returns true.
		/// <br/> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#custom-ai">Basic Projectile Guide</see> teaches the basics of writing a custom AI, such as timers, gravity, rotation, etc.
		/// </summary>
		public void AI()
		;*/

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will be called regardless of what PreAI returns.
		/// </summary>
		public void PostAI()
		;

		/// <summary>
		/// Whether or not this projectile should update its position based on factors such as its velocity, whether it is in liquid, etc. Return false to make its velocity have no effect on its position. Returns true by default.
		/// </summary>
		public bool ShouldUpdatePosition()
		;

		/// <summary>
		/// Allows you to determine how this projectile interacts with tiles. Return false if you completely override or cancel this projectile's tile collision behavior. Returns true by default.
		/// </summary>
		/// <param name="width"> The width of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.width. </param>
		/// <param name="height"> The height of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.height. </param>
		/// <param name="fallThrough"> Whether or not this projectile falls through platforms and similar tiles. </param>
		/// <param name="hitboxCenterFrac"> Determines by how much the tile collision hitbox's position (top left corner) will be offset from this projectile's real center. If vanilla doesn't modify it, defaults to half the hitbox size (new Vector2(0.5f, 0.5f)). </param>
		/// <returns></returns>
		public bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		;

		/// <summary>
		/// Allows you to determine what happens when this projectile collides with a tile. OldVelocity is the velocity before tile collision. The velocity that takes tile collision into account can be found with Projectile.velocity. Return true to allow the vanilla tile collision code to take place (which normally kills the projectile). Returns true by default.
		/// </summary>
		/// <param name="oldVelocity">The velocity of the projectile upon collision.</param>
		public bool OnTileCollide(Vector2 oldVelocity)
		;
		/*
		/// <summary>
		/// Return true or false to specify if the projectile can cut tiles like vines, pots, and Queen Bee larva. Return null for vanilla decision.
		/// </summary>
		public  bool? CanCutTiles()
		;
		*/

		/// <summary>
		/// Code ran when the projectile cuts tiles. Only runs if CanCutTiles() returns true. Useful when programming lasers and such.
		/// </summary>
		public void CutTiles()
		;

		/// <summary>
		/// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
		/// </summary>
		public bool PreKill(int timeLeft)
		;

		/// <summary>
		/// Allows you to control what happens when this projectile is killed (for example, creating dust or making sounds). Also useful for creating retrievable ammo. Called on all clients and the server in multiplayer, so be sure to use `if (Projectile.owner == Main.myPlayer)` if you are spawning retrievable ammo. (As seen in ExampleJavelinProjectile)
		/// </summary>
		public void OnKill(int timeLeft)
		;


		/// <summary>
		/// Whether or not this projectile is capable of killing tiles (such as grass) and damaging NPCs/players.
		/// Return false to prevent it from doing any sort of damage.
		/// Return true if you want the projectile to do damage regardless of the default blacklist.
		/// Return null to let the projectile follow vanilla can-damage-anything rules. This is what happens by default.
		/// </summary>
		public bool? CanDamage()
		;
		/*
		/// <summary>
		/// Whether or not this minion can damage NPCs by touching them. Returns false by default. Note that this will only be used if this projectile is considered a pet.
		/// </summary>
		public  bool MinionContactDamage()
		;*/

		/// <summary>
		/// Allows you to change the hitbox used by this projectile for damaging players and NPCs.
		/// </summary>
		/// <param name="hitbox"></param>
		public void ModifyDamageHitbox(ref Rectangle hitbox)
		;

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given NPC. Return true to allow hitting the target, return false to block this projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public bool? CanHitNPC(NPC target)
		;

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this projectile does to an NPC. This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers">The modifiers for this strike.</param>
		public void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		;

		/// <summary>
		/// Allows you to create special effects when this projectile hits an NPC (for example, inflicting debuffs). This method is only called for the owner of the projectile, meaning that in multi-player, projectiles owned by a player call this method on that client, and projectiles owned by the server such as enemy projectiles call this method on the server.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="hit">The damage.</param>
		/// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
		public void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		;

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given opponent player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target</param>
		public bool CanHitPvp(Player target)
		;

		/// <summary>
		/// Allows you to determine whether this hostile projectile can hit the given player. Return false to block this projectile from hitting the target. Returns true by default.
		/// </summary>
		/// <param name="target">The target.</param>
		public bool CanHitPlayer(Player target)
		;

		/// <summary>
		/// Allows you to modify the damage, etc., that this hostile projectile does to a player.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers"></param>
		public void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		;

		/// <summary>
		/// Allows you to create special effects when this hostile projectile hits a player. <br/>
		/// Only runs on the local client in multiplayer.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="info"></param>
		public void OnHitPlayer(Player target, Player.HurtInfo info)
		;

		/// <summary>
		/// Allows you to use custom collision detection between this projectile and a player or NPC that this projectile can damage. Useful for things like diagonal lasers, projectiles that leave a trail behind them, etc.
		/// </summary>
		/// <param name="projHitbox">The hitbox of the projectile.</param>
		/// <param name="targetHitbox">The hitbox of the target.</param>
		/// <returns>Whether they collide or not.</returns>
		public bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		;


		/// <summary>
		/// Allows you to determine the color and transparency in which this projectile is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// </summary>
		public Color? GetAlpha(Color lightColor)
		;

		/// <summary>
		/// Allows you to draw things behind this projectile. Use the Main.EntitySpriteDraw method for drawing. Returns false to stop the game from drawing extras textures related to the projectile (for example, the chains for grappling hooks), useful if you're manually drawing the extras. Returns true by default.
		/// </summary>
		public bool PreDrawExtras()
		;

		/// <summary>
		/// Allows you to draw things behind this projectile, or to modify the way it is drawn. Use the Main.EntitySpriteDraw method for drawing. Return false to stop the vanilla projectile drawing code (useful if you're manually drawing the projectile). Returns true by default.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center. </param>
		public bool PreDraw(ref Color lightColor)
		;

		/// <summary>
		/// Allows you to draw things in front of this projectile. Use the Main.EntitySpriteDraw method for drawing. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center, after being modified by vanilla and other mods. </param>
		public void PostDraw(Color lightColor)
		;

		/// <summary>
		/// When used in conjunction with "Projectile.hide = true", allows you to specify that this projectile should be drawn behind certain elements. Add the index to one and only one of the lists. For example, the Nebula Arcanum projectile draws behind NPCs and tiles.
		/// </summary>
		public void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		;
	}

}
