using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;

namespace WackyBagTr.Projectiles.Behaviors
{
	public class Damping : ProjectileBehavior<ModProjectile>
	{
		public float DampingValue;
		public Damping(ModProjectile modProj, float damping) : base(modProj)
		{
			this.DampingValue = damping;
		}
		public override void AI()
		{
			base.AI();
			Projectile.velocity *= DampingValue;
		}
	}
}
