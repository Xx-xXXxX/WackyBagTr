using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;

using WackyBag.Structures.Collections;

namespace WackyBagTr.Behaviors
{
	
	/// <summary>
	/// 通过组合模式操作behavior
	/// Add应在Initialize前完成，否则应该报错
	/// 否则可能出现联机同步错误
	/// </summary>
	public interface IBehaviorComponentIn<in TBehavior> : IBehavior
	{
		/// <summary>
		/// 加入成员，应在Initialize前完成
		/// </summary>
		int Add(TBehavior behavior);
	}
	public interface IBehaviorComponentOut<out TBehavior>: IBehavior, IBehaviorsCollection<TBehavior>
	{
	}
	public interface IBehaviorComponent<TBehavior> : IBehaviorComponentIn<TBehavior>, IBehaviorComponentOut<TBehavior> { }

	public abstract class BehaviorsListOut<TBehavior>: BehaviorsCollection<TBehavior>, IBehaviorComponentOut<TBehavior>//, IBehaviorComponent<RealBehaviorType>
		where TBehavior : IBehavior
	{
		/// <summary>
		/// 获取Behavior
		/// </summary>
		public TBehavior this[int id] => BehaviorsList[id];
		/// <summary>
		/// 装有Behavior的容器
		/// </summary>
		public abstract IReadOnlyList<TBehavior> BehaviorsList { get; }
		public override required IEnumerable<TBehavior> Behaviors { get=>BehaviorsList; init { } }
		//public override IEnumerable<TBehavior> ActiveBehaviors => BehaviorsList;
		//IEnumerable<TBehavior> IBehaviorsCollection<TBehavior>.ActiveBehaviors { get; }
	}

	/// <summary>
	/// 通过组合模式操作behavior
	/// </summary>
	public abstract class BehaviorsList<TBehavior> : BehaviorsListOut<TBehavior>,IBehaviorComponent<TBehavior>
		where TBehavior : IBehavior
	{
		/// <summary>
		/// 添加新的behavior
		/// </summary>
		/// <param name="behavior"></param>
		/// <returns>其id</returns>
		public abstract int Add(TBehavior behavior);
	}

	public class BehaviorsListStored<TBehavior> : BehaviorsList<TBehavior>
		where TBehavior : IBehavior
	{
		/// <summary>
		/// 装有Behavior的容器
		/// </summary>
		//protected List<RealBehaviorType> BehaviorsList = new List<RealBehaviorType>();
		protected IList<TBehavior> behaviorsList = [];
		public override IReadOnlyList<TBehavior> BehaviorsList => (IReadOnlyList<TBehavior>)behaviorsList;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
		public override int Add(TBehavior behavior)
		{
			int i = behaviorsList.Count;
			behaviorsList.Add(behavior);
			nameToBehaviors.TryAdd(behavior.BehaviorName, behavior);
			return i;
		}
		protected Dictionary<string, TBehavior> nameToBehaviors = [];
		public override IDictionary<string, TBehavior> NameToBehaviors => nameToBehaviors;
	}

}
