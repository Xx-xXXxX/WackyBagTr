using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace WackyBagTr.Dusts
{
	public class DelegateDust:ModDust
	{
		public static void Only1Frame(Dust dust) {
			if (dust.fadeIn >= 1) dust.active = false;
			dust.fadeIn = 1;
		}
		public static Dust New(Action<Dust> Update, Action<Dust> Draw, Vector2 position, Vector2? velocity=null, float? rotation=null,Color color=default) {
			var res = Dust.NewDustPerfect(position,ModContent.DustType<DelegateDust>(),velocity,newColor:color);
			res.customData = (Update, Draw);
			res.rotation = rotation ?? 0f;
			return res;
		}

		public override string Texture => "Terraria/Images/MagicPixel";
		public override void OnSpawn(Dust dust)
		{
			base.OnSpawn(dust);
			dust.noGravity = true;
			dust.noLight=true;
			
		}
		public override bool Update(Dust dust)
		{
			(((Action<Dust>, Action<Dust>))(dust.customData)).Item1(dust);
			return false;
		}
		public override bool PreDraw(Dust dust)
		{
			(((Action<Dust>, Action<Dust>))(dust.customData)).Item2(dust);
			//TextureAssets.MagicPixel
			return false;
		}
	}
}
