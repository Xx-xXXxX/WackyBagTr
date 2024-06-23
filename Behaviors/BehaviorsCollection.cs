using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyBagTr.Behaviors
{
	public interface IBehaviorsCollection<out TBehavior> : IBehavior,IEnumerable<TBehavior>
	{
		public IEnumerable<TBehavior> ActiveBehaviors { get; }
		//public IReadOnlyDictionary<string, TBehavior> NameToBehaviors { get; }

	}

	public class BehaviorsCollection<TBehavior> : Behavior, IBehaviorsCollection<TBehavior>
		where TBehavior : IBehavior
	{
		
		protected readonly Stack<int> storedActiveBehaviors = [];
		public virtual IEnumerable<TBehavior> ActiveBehaviors=> Behaviors.Where(i=>i.Active);
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		public virtual IEnumerable<TBehavior> Behaviors { get; init; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
		public virtual IDictionary<string, TBehavior>? NameToBehaviors { get; init; }
		public override void Initialize()
		{
			foreach (var item in Behaviors)
			{
				item.Initialize();
			}
			base.Initialize();
		}

		public override void Dispose()
		{
			foreach (var item in Behaviors)
			{
				item.Dispose();
			}
			base.Dispose();
		}
		/*
		public override void AI()
		{
			foreach (var item in Behaviors) {
				item.AI();
			}
			base.AI();
		}*/

		public override object? Call(params object[] vs)
		{
			if (NameToBehaviors != null && vs[0] is string name) {
				if (NameToBehaviors.TryGetValue(name, out var behavior)) {
					return behavior.Call(vs[1..]);
				}
			}
			return null;
		}

		/// <summary>
		/// 如果存在需要同步的组件则同步
		/// </summary>
		public override bool NetUpdate
		{
			get
			{
				if (NetUpdateThis) return true;
				foreach (var i in ActiveBehaviors) if (i.NetUpdate) return true;
				return false;
			}
		}

		protected bool NetUpdateThis { get; }

		private int netSpanTime = 0;
		protected virtual int NetSpanTimeMax{get=>60;}

		public override void ReciveExtraAI(BinaryReader reader)
		{
			base.ReciveExtraAI(reader);
			bool netUpdateThis = reader.ReadBoolean();
			if (netUpdateThis) OnNetUpdateReceive(reader);
			foreach (TBehavior behavior	in Behaviors)
			{

				Terraria.BitsByte bits = reader.ReadByte();
				bool NetUpdate = bits[0];
				if (NetUpdate)
					behavior.ReciveExtraAI(reader);
			}
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			bool NetUpdateAll = false;
			if (netSpanTime <= 0) {
				netSpanTime = NetSpanTimeMax;
				NetUpdateAll = true;
			}
			if (NetUpdateThis|| NetUpdateAll)
			{
				writer.Write(true);
				OnNetUpdateSend(writer);
			}
			else writer.Write(false);
			foreach (TBehavior behavior in Behaviors)
			{
				Terraria.BitsByte bits = 0;
				bool NetUpdate = bits[0] = NetUpdateAll || behavior.NetUpdate;
				writer.Write(bits);
				if (NetUpdate)
					behavior.SendExtraAI(writer);
			}
		}
		protected virtual void OnNetUpdateReceive(BinaryReader reader) { }
		protected virtual void OnNetUpdateSend(BinaryWriter writer) { }

		public IEnumerator<TBehavior> GetEnumerator() {
			return (IEnumerator<TBehavior>)ActiveBehaviors;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public override void Pause()
		{
			base.Pause();
			int i = 0;
			foreach (var item in Behaviors)
			{
				if (item.Active) {
					storedActiveBehaviors.Push(i);
					item.Pause();
				}
				i++;
			}
		}
		public override void Activate()
		{
			base.Activate();
			int i = 0;
			if(storedActiveBehaviors.Count>0)
			foreach (var item in Behaviors)
			{

				if (storedActiveBehaviors.Peek()==i)
				{
					storedActiveBehaviors.Pop();
					item.Activate();
						if (storedActiveBehaviors.Count == 0) break;
				}
				i++;
			}
		}
	}
}
