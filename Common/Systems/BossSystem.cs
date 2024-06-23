using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

using WackyBag;

using static Terraria.GameContent.Bestiary.IL_BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions;
using static WackyBagTr.Common.Systems.BossData;

namespace WackyBagTr.Common.Systems
{

	
	public record struct BossData(
		int bosstype,
		string internalName,
		float Progression,
		List<int> IDs,
		AdditionalData Additional
		)
	{
		public record struct AdditionalData(
			string? displayName=null,
			Func<LocalizedText>? spawnInfo = null,
			List<int>? spawnItems = null,
			List<int>? collectibles = null,
			Func<bool>? availability = null,
			Func<List<Asset<Texture2D>>>? overrideHeadTextures = null,
			Dictionary<int, LocalizedText>? limbs = null,
			Func<NPC, LocalizedText>? despawnMessage = null,
			Action<SpriteBatch, Rectangle, Color>? customPortrait = null
			) { 
			
		}
	}

	public abstract class BossSystem:ModSystem
	{
		
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		private Dictionary<string, int> bossToId;
		public IDictionary<string, int> BossToId=> bossToId;
		public BossData[] BossDatas { get; private set; }
		private BitArray BossDowned;
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

		public override void PostSetupContent()
		{
			base.PostSetupContent();
			BossDatas=GetBossDatas();
			BossDowned = new(BossDatas.Length);
			bossToId = new();
			if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
			{
				return;
			}

			for (int i = 0; i < BossDatas.Length; i++)
			{
				var item = BossDatas[i];
				int i_ = i;
				bossToId.Add(item.internalName, i);

				Dictionary<string, object> innerData=new();

				item.Additional.displayName?.AddTo(innerData, "displayName");//if (item.Additional.displayName != null) innerData.Add("displayName", item.Additional.displayName);
				item.Additional.spawnInfo?.AddTo(innerData, "spawnInfo");
				item.Additional.spawnItems?.AddTo(innerData, "spawnItems");
				item.Additional.collectibles?.AddTo(innerData, "collectibles");
				item.Additional.overrideHeadTextures?.AddTo(innerData, "overrideHeadTextures");
				item.Additional.availability?.AddTo(innerData, "availability");
				item.Additional.limbs?.AddTo(innerData, "limbs");
				item.Additional.despawnMessage?.AddTo(innerData, "despawnMessage");
				item.Additional.customPortrait?.AddTo(innerData, "customPortrait");

				//if(item.Additional.spawnInfo!=null) 
				bossChecklistMod.Call(
					"LogBoss",
					Mod,
					item.internalName,
					item.Progression,
					()=>BossDowned[i_],
					item.bosstype,
					innerData
					);
			}
		}

		/// <summary>
		/// do at SetStaticDefaults;
		/// </summary>
		/// <returns></returns>
		protected abstract BossData[] GetBossDatas();

		public override void ClearWorld()
		{
			BossDowned.SetAll(false);
		}

		public static string GetDoneBossText(string BossName) { 
			return $"BossDowned{BossName}";
		}

		public void SetBossDone(string BossName) {
			BossDowned[bossToId[BossName]] = true;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			base.SaveWorldData(tag);
			foreach (var item in bossToId)
			{
				if (BossDowned[item.Value]) {
					tag[GetDoneBossText(item.Key)] = true;
				}
			}
		}
		public override void LoadWorldData(TagCompound tag)
		{
			base.LoadWorldData(tag);
			foreach (var item in bossToId)
			{
				if (tag.ContainsKey(GetDoneBossText(item.Key)))
				{
					BossDowned[item.Value] = (bool)tag[GetDoneBossText(item.Key)];
				}
			}
		}
		public override void NetSend(BinaryWriter writer)
		{
			base.NetSend(writer);
			byte[] bytes = new byte[BossDatas.Length >> 3+1];
			//BossDowned.
			BossDowned.CopyTo(bytes,0);
			writer.Write(bytes);
		}

		public override void NetReceive(BinaryReader reader)
		{
			base.NetReceive(reader);
			byte[] bytes = new byte[BossDatas.Length >> 3 + 1];
			reader.Read(bytes,0, bytes.Length);
			BossDowned=new BitArray(bytes);
		}
	}
}
