using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;

namespace WackyBagTr.NPCs.Behaviors
{
	public class Damping : NPCBehavior<ModNPC>
	{
		public float DampingValue;
		public Damping(ModNPC modNPC, float damping) : base(modNPC)
		{
			this.DampingValue = damping;
		}
		public override void AI()
		{
			base.AI();
			NPC.velocity *= DampingValue;
		}
	}
}
