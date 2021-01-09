using System;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Variables
{
    public abstract class GameEvent : VariableAsset
    {
        public abstract IDisposable Subscribe(Action action);
        public abstract void Publish();
    }

    public abstract class GameEventListener : VariableAsset
    {
        public abstract IDisposable Subscribe(Action action);
    }
}

namespace SilCilSystem.Variables.Generic
{
    public abstract class Variable<T> : VariableAsset
    {
        public abstract T Value { get; set; }
        public void SetValue(T value) => Value = value;
        public static implicit operator T(Variable<T> variable) => variable.Value;
    }

    public abstract class ReadonlyVariable<T> : VariableAsset
    {
        public abstract T Value { get; }
        public static implicit operator T(ReadonlyVariable<T> variable) => variable.Value;
    }
    
    public abstract class GameEvent<T> : GameEvent
    {
        public abstract IDisposable Subscribe(Action<T> action);
        public abstract void Publish(T value);

        public override void Publish() => Publish(default);
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }
    
    public abstract class GameEventListener<T> : GameEventListener
    {
        public abstract IDisposable Subscribe(Action<T> action);

        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }
}
