using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WackyBagTr.Behaviors
{
	/// <summary>
	/// behavior, contains net update, can be used into combine and automata (seems generally a mark)
	/// 行为，可以启动和暂停（好像没什么区别），包含联机同步，用于组合与自动机
	/// </summary>
	public interface IBehavior
	{
		/// <summary>
		/// when init (register)
		/// </summary>
		void Initialize();
		/// <summary>
		/// when dispose (owner killed)
		/// </summary>
		void Dispose();
		/// <summary>
		/// send data to sync, do when NetUpdate=true
		/// </summary>
		/// <param name="writer"></param>
		void SendExtraAI(BinaryWriter writer);
		/// <summary>
		/// recive data to sync
		/// </summary>
		/// <param name="reader"></param>
		void ReciveExtraAI(BinaryReader reader);
		/// <summary>
		/// general data share
		/// </summary>
		object? Call(params object[] vs);
		/// <summary>
		/// name of behavior, should consider duplicate name condition
		/// </summary>
		string BehaviorName { get=>this.GetType().Name; }
		/// <summary>
		/// whether do net update
		/// </summary>
		/// [Obsolete("dk when to full sync or frame sync")]
		bool NetUpdate { get=>true; }
		
		/// <summary>
		/// do things here
		/// </summary>
		void AI();

		/// <summary>
		/// whether this is on, 
		/// should be default to be false
		/// </summary>
		bool Active { get; }
		/// <summary>
		/// when pause
		/// should do Action=false
		/// </summary>
		void Pause();
		/// <summary>
		/// when activate
		/// should do Action=true
		/// </summary>
		void Activate();

	}
	public abstract class Behavior : IBehavior
	{
		public virtual string BehaviorName { get=>this.GetType().Name; }

		public virtual bool NetUpdate { get=>true; }

		public virtual object? Call(params object[] vs)
		{
			return null;
		}
		public virtual void Dispose()
		{

		}

		public virtual void Initialize()
		{

		}

		public virtual void ReciveExtraAI(BinaryReader reader)
		{

		}

		public virtual void SendExtraAI(BinaryWriter writer)
		{

		}


	

		public bool Active { get; private set; }

		public virtual void Activate() { Active = true; }
		public virtual void Pause() { Active = false; }
		//public virtual void PausableAI() { }
		
		public virtual void AI()
		{
			
			
		}
	}
	public static class BehaviorUtils {
		
		
	}


}
