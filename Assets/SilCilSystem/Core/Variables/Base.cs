using System;
using UnityEngine;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Variables.Base
{
    public abstract class VariableAsset : ScriptableObject { }
}

//
// 以下はジェネリクスのシリアライズが可能なUnity2020用のもの.
// Unity2019では使用不可.
//
namespace SilCilSystem.Variables.Base.Generic
{
    public abstract class VariableBase<T> : Variable<T>
    {
        [SerializeField] private T m_value = default;
        [SerializeField] private GameEvent<T> m_onValueChanged = default;

        public override T Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }
    }

    public abstract class ReadonlyVariableBase<T> : ReadonlyVariable<T>
    {
        [SerializeField] private Variable<T> m_variable = default;

        public override T Value => m_variable.Value;
    }

    public abstract class GameEventBase<T> : GameEvent<T>
    {
        private event Action<T> m_event = default;

        public override void Publish(T value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<T> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }

        public override void Publish() => Publish(default(T));
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }

    public abstract class GameEventListenerBase<T> : GameEventListener<T>
    {
        [SerializeField] private GameEvent<T> m_event = default;

        public override IDisposable Subscribe(Action<T> action) => m_event?.Subscribe(action);
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());
    }
}