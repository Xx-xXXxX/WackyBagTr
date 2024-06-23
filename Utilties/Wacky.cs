using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;
using ReLogic.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Map;
using Terraria.ModLoader;

using WackyBag;
using WackyBagTr.Dusts;

using SMatrix = System.Numerics.Matrix3x2;
namespace WackyBagTr.Utilties
{
	public static class Wacky
	{
		public static void DrawLine(SpriteBatch sb, Vector2 pos, Vector2 line, float lineWidth = 2, Color? color = null) {
			sb.Draw(TextureAssets.MagicPixel.Value,
		   pos - Main.screenPosition,
		   null,
		   color ?? Color.White,
		   line.ToRotation()-MathHelper.PiOver2,
		   new Vector2(0.5f, 0),
		   new Vector2(line.Length()/1000, lineWidth),
		   SpriteEffects.None,
		   0);
		}

		static FieldInfo getbeginCalled;
		static Wacky() {
			Type type = typeof(SpriteBatch);
			getbeginCalled = type.GetField("beginCalled", BindingFlags.NonPublic|BindingFlags.Instance)!;
		}

		public static bool Get_beginCalled(SpriteBatch sb) { 
			return (bool)getbeginCalled.GetValue(sb)!;
		}

		public static Dust DrawLineFrame(Vector2 pos,Vector2 line,float lineWidth=2, Color? color=null) {


			Dust dust = DelegateDust.New(DelegateDust.Only1Frame, (dust) => {
				DrawLine(Main.spriteBatch, dust.position, dust.velocity, lineWidth, color);
			},pos,line);
			return dust;
		}

		public static Dust DrawTextFrame(Vector2 pos, string str, Color? color = null)
		{
			Dust dust = DelegateDust.New(DelegateDust.Only1Frame, (dust) => {
				Main.spriteBatch.DrawString(FontAssets.MouseText.Value,str,pos-Main.screenPosition,color??Color.White);
			}, pos, Vector2.Zero);
			return dust;
		}

		internal static Asset<Effect>? drawInMinimapOnly;
		public static Effect DrawInRectOnly {
			get {
				//if (drawInMinimapOnly == null) ;
				return drawInMinimapOnly?.Value??throw new InvalidOperationException($"{nameof(drawInMinimapOnly)} haven't load");
			}
		}

		/// <summary>
		/// Get Matrix from draw
		/// </summary>
		/// <param name="textureSize"></param>
		/// <param name="DrawPos"></param>
		/// <param name="sourceRectangle"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static SMatrix GetTransformMatrix(Point textureSize, Vector2 DrawPos, Rectangle? sourceRectangle, float rotation, Vector2 origin, Vector2 scale) {
			Vector2 textureSizeVec= textureSize.ToVector2();


			sourceRectangle ??=new(0,0,textureSize.X,textureSize.Y);
			//Vector2 realOrig = (origin + sourceRectangle.Value.Location.ToVector2())/textureSize.ToVector2();
			
			SMatrix res = SMatrix.Identity;
			// 0,0,1,1

			res = SMatrix.Multiply(res,SMatrix.CreateScale(textureSizeVec.ToSysVec()));
			//0,0,w,h

			res = SMatrix.Multiply(res, SMatrix.CreateTranslation(-origin.X- sourceRectangle.Value.X, -origin.Y- sourceRectangle.Value.Y));
			
			
			res = SMatrix.Multiply(res, SMatrix.CreateScale(scale.X, scale.Y));
			

			res = SMatrix.Multiply(res, SMatrix.CreateRotation(rotation));
			

			res = SMatrix.Multiply(res,SMatrix.CreateTranslation(DrawPos.X, DrawPos.Y));
			return res;
		}

		/// <summary>
		/// <para>draw in map as if in world.</para>
		/// <para>can be used in <see cref="ModNPC.BossHeadRotation(ref float)"/></para>
		/// <para>DON'T use in <see cref="ModNPC.BossHeadSlot(ref int)"/> or get cooked by BossCheckList</para>
		/// <para>Also don't set -1 in <see cref="ModNPC.BossHeadSlot(ref int)"/> or draw won't happen</para>
		/// <para>Draw in Minimap is cutted</para>
		/// </summary>
		/// <param name="WldPos">Draw position in world</param>
		/// <param name="extras">more things to do, e.g. add shader</param>
		public static void DrawInMap(Texture2D texture, Vector2 WldPos, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float worthless = 0f,Action? extras=null)
		{
			var spriteBatch = Main.spriteBatch;
			var screenPosition = Main.screenPosition;
			var miniMapWidth = Main.miniMapWidth;
			var miniMapHeight=Main.miniMapHeight;
			Vector2 MapOuter = new(10, 10);//num7 num8
										   //float num7 = 10;
										   //float num8 = 10;

			//
			float scale2=2f;//=Main.mapFullscreen?Main.mapFullscreenScale:(Main.MapScale!=1? Main.mapOverlayScale: Main.mapMinimapScale);// num5
			
			Vector2 offset2=new(200,300);//(num,num2)
			if (Main.mapFullscreen)
			{
				scale2 = Main.mapFullscreenScale;
				offset2 = -Main.mapFullscreenPos * scale2 + Main.ScreenSize.ToVector2() / 2;
				offset2 += MapOuter * scale2;
				//offset2 += Main.ScreenSize.ToVector2() / 2;
				//offset2 += new Vector2(200, 300);
				//offset2 += Main.screenPosition / 16 * scale2;
			}
			else if (Main.mapStyle == 1)
			{
				scale2 = Main.mapMinimapScale;
				offset2 = new(Main.miniMapX, Main.miniMapY);

				float num31 = (screenPosition.X + (float)(PlayerInput.RealScreenWidth / 2)) / 16f;
				float num32 = (screenPosition.Y + (float)(PlayerInput.RealScreenHeight / 2)) / 16f;
				//float num11 = (0f - (num31 - (float)(int)((screenPosition.X + (float)(PlayerInput.RealScreenWidth / 2)) / 16f))) * scale2;
				//float num12 = (0f - (num32 - (float)(int)((screenPosition.Y + (float)(PlayerInput.RealScreenHeight / 2)) / 16f))) * scale2;
				float num15 = (float)miniMapWidth / scale2;
				float num16 = (float)miniMapHeight / scale2;
				float num13 = (float)(int)num31 - num15 / 2f;
				float num14 = (float)(int)num32 - num16 / 2f;

				//if (num13 < MapOuter.X)
					offset2.X -= (num13 - MapOuter.X) * scale2;

				//if (num14 < MapOuter.Y)
					offset2.Y -= (num14 - MapOuter.Y) * scale2;
			}
			else if (Main.mapStyle == 2) {
				scale2 = Main.mapOverlayScale;
				Vector2 ScreenCenterOfTileWld = (Main.screenPosition + Main.ScreenSize.ToVector2() / 2) / 16f;
				//float num36 = (screenPosition.X + (float)(screenWidth / 2)) / 16f;
				//float num37 = (screenPosition.Y + (float)(screenHeight / 2)) / 16f;
				ScreenCenterOfTileWld *= scale2;

				//num36 *= scale2;
				//num37 *= scale2;
				offset2 = -ScreenCenterOfTileWld + Main.ScreenSize.ToVector2() / 2;
				//num = 0f - num36 + (float)(screenWidth / 2);
				//num2 = 0f - num37 + (float)(screenHeight / 2);

				offset2 += MapOuter * scale2;
				/*
				num += num7 * num5;
				num2 += num8 * num5;*/
			}

			Vector2 DrawPos= WldPos/16*scale2 + offset2-Vector2.One*10f*scale2;
			Matrix transformMatrix = Main.UIScaleMatrix;
			Matrix transformMatrix2 = Main.UIScaleMatrix;

			Matrix MapScaleMatrix = Matrix.CreateScale(Main.MapScale);
			if (Main.mapStyle != 1)
				transformMatrix = Matrix.Identity;

			if (Main.mapFullscreen)
				transformMatrix = Matrix.Identity;

			if (!Main.mapFullscreen && Main.mapStyle == 1)
			{
				transformMatrix *= MapScaleMatrix;
				transformMatrix2 *= MapScaleMatrix;
			}

			//bool doDraw = Get_beginCalled(spriteBatch);
			if (Main.mapFullscreen && extras != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				extras.Invoke();
			}
			else if (!Main.mapFullscreen && Main.mapStyle == 2 && extras != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix);
				extras.Invoke();
			}
			else if (!Main.mapFullscreen && Main.mapStyle == 1)
			{


				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix2);
				extras?.Invoke();
				//if(spriteBatch.)
				//spriteBatch.End();
				//if (doDraw)
				//{
				//	spriteBatch.End();
				//	//doDraw=true;
				//}
				//spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

				//spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

				System.Numerics.Matrix3x2 matrix = GetTransformMatrix(new(texture.Width, texture.Height), DrawPos, sourceRectangle, rotation, origin, scale / 16 * scale2);

				//void DrawP(Vector2 p1,Vector2 p2) {
				//	Vector2 pos1 = System.Numerics.Vector2.Transform(p1.ToSysVec(), matrix).ToXnaVec();
				//	Vector2 pos2 = System.Numerics.Vector2.Transform(p2.ToSysVec(), matrix).ToXnaVec();
				//	Vector2 offset = pos2 - pos1;
				//	spriteBatch.Draw(TextureAssets.MagicPixel.Value, pos1, null, Color.Red, offset.ToRotation() - MathHelper.PiOver2, new Vector2(0.5f, 0), new Vector2(1, offset.Length() / TextureAssets.MagicPixel.Value.Height), SpriteEffects.None, 0);
				//}
				//DrawP(new(0, 0), new(0, 1));
				//DrawP(new(1, 1), new(0, 1));
				//DrawP(new(1, 1), new(1, 0));
				//DrawP(new(0, 0), new(1, 0));
				//var MatImpl=matrix.imp
				//DrawLine(spriteBatch, System.Numerics.Vector2.Transform(DrawPos.ToSysVec(), matrix).ToXnaVec()+Main.screenPosition, System.Numerics.Vector2.Transform((DrawPos + new Vector2(1000, 1000)).ToSysVec(), matrix).ToXnaVec() - System.Numerics.Vector2.Transform(DrawPos.ToSysVec(), matrix).ToXnaVec(), 4, Color.Green);


				//DrawInRectOnly.Parameters["TransformMatrix"].SetValue(new float[] { matrix .M11,matrix.M12,matrix.M21,matrix.M22,matrix.M31,matrix.M32});
				DrawInRectOnly.Parameters["MatX"].SetValue(new float[] { matrix.M11, matrix.M12 });
				DrawInRectOnly.Parameters["MatY"].SetValue(new float[] { matrix.M21, matrix.M22 });
				DrawInRectOnly.Parameters["MatZ"].SetValue(new float[] { matrix.M31, matrix.M32 });
				//pos1 =new(Main.miniMapX,Main.miniMapY);
				//pos2 =;
				//offset = new Vector2(miniMapWidth,miniMapHeight);
				//spriteBatch.Draw(TextureAssets.MagicPixel.Value, pos1, null, Color.Red, offset.ToRotation() - MathHelper.PiOver2, new Vector2(0.5f, 0), new Vector2(4, offset.Length() / TextureAssets.MagicPixel.Value.Height), SpriteEffects.None, 0);

				DrawInRectOnly.Parameters["RectLT"].SetValue(new float[] { Main.miniMapX, Main.miniMapY });

				DrawInRectOnly.Parameters["RectRB"].SetValue(new float[] { Main.miniMapX + Main.miniMapWidth, Main.miniMapY + Main.miniMapHeight });

				DrawInRectOnly.CurrentTechnique.Passes["Pass1"].Apply();
			}

			spriteBatch.Draw(texture,DrawPos,sourceRectangle,color,rotation,origin,scale/16*scale2, effects,worthless);

			if (Main.mapFullscreen && extras != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				//extras.Invoke();
			}
			else if (!Main.mapFullscreen && Main.mapStyle == 2 && extras != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix);

			}
			else if (!Main.mapFullscreen && Main.mapStyle == 1)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix2);
				//DrawInRectOnly.CurrentTechnique.Passes[]
			}
			//if (Main.mapStyle == 1)
			//{
			//	spriteBatch.End();
			//	if(doDraw)
			//		spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

			//}
		}
	}
}
