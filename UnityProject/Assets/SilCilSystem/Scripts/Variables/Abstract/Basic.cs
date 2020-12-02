using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
	public abstract class Variable<T> : ScriptableObject
	{
		public abstract T Value{ get; set; }
		public void SetValue(T value) => Value = value;
		public static implicit operator T(Variable<T> variable) => variable.Value;
	}

	public abstract class ReadonlyVariable<T> : ScriptableObject
	{
		public abstract T Value{ get; }
		public static implicit operator T(ReadonlyVariable<T> variable) => variable.Value;
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
		public abstract IDisposable Subscribe(Action action);
	}

	public abstract class GameEventListener<T> : ScriptableObject
	{
		public abstract IDisposable Subscribe(Action<T> action);
	}
}
