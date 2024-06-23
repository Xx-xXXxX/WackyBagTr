using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;

using WackyBag.Structures.Collections;

namespace WackyBagTr.NPCs.Behaviors
{
	public class DoWhenHP<TModNPC> : NPCBehavior<TModNPC>
		where TModNPC : ModNPC
	{
		public DoWhenHP(TModNPC modNPC) : base(modNPC)
		{
		}

		protected readonly UnorderedList<(Action<TModNPC> action, float HPPercentage, bool done)> actions = [];

		public void AddAction(Action<TModNPC> action, float HPPercentage) { 
			actions.Add((action, HPPercentage,false));
		}

		public override void AI()
		{
			base.AI();
			for (int i = 0; i < actions.Count; i++) { 
				var action= actions[i];
				if ( ((float)NPC.life / NPC.lifeMax)< action.HPPercentage)
				{
					action.action(ModNPC);
					action.done = true;
					actions.Remove(i);
					i--;
				}
			}
		}
	}
}
