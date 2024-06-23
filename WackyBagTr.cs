using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace WackyBagTr
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class WackyBagTr : Mod
	{
		public override void Load()
		{
			var v = this.Assets.Request<Effect>("Assets/Effects/DrawInRect");
			Utilties.Wacky.drawInMinimapOnly = v;
		}
	}
}
