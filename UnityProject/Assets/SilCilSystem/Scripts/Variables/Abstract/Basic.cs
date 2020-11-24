using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
	public abstract class Variable<T> : ScriptableObject
	{
		public abstract T Value{ get; set; }
	}

	public abstract class ReadonlyVariable<T> : ScriptableObject
	{
		public abstract T Value{ get; }
	}

	public abstract class GameEvent : ScriptableObject
	{
		public abstract IDisposable Subscribe(Action action);
		public abstract void Publish();
	}

	public abstract class GameEvent<T> : ScriptableObject
	{
		public abstract IDisposable Subscribe(Action<T> action);
		public abstract void Publish(T arg);
	}

	public abstract class GameEventListener : ScriptableObject
	{
		public abstract IDisposable Subscibe(Action action);
	}

	public abstract class GameEventListener<T> : ScriptableObject
	{
		public abstract IDisposable Subscibe(Action<T> action);
	}
}
