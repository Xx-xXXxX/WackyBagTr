using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WackyBagTr.NPCs.Behaviors
{
	//[Obsolete("Sync Problem")]
	public class ServerLoopDo<TModNPC> : NPCBehavior<TModNPC>
		where TModNPC : ModNPC
	{
		public ServerLoopDo(TModNPC modNPC) : base(modNPC)
		{
		}

		protected List<Func<bool>> actions = [];

		public void Add(Func<bool> fn) { 
			actions.Add(fn);
		}

		public override void AI()
		{
			base.AI();
			if (Main.netMode == NetmodeID.MultiplayerClient) return;
			for (int i = 0; i < actions.Count; i++)
			{
				var res = actions[i]();
				if (!res) {
					actions.RemoveAt(i);
					i--;
				}
			}
		}
	}
}
