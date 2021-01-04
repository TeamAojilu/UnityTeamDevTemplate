using System;
using UnityEngine;
using System.Diagnostics;
using SilCilSystem.Variables.Generic;
using SilCilSystem.Editors;

namespace SilCilSystem.Variables.Base
{
    public abstract class VariableAsset : ScriptableObject
    {
        [Conditional("UNITY_EDITOR")]
        /// <summary>Available only for UNITY_EDITOR</summary>
        public abstract void GetAssetName(ref string name);

        [Conditional("UNITY_EDITOR")]
        /// <summary>Available only for UNITY_EDITOR</summary>
        public abstract void OnAttached(VariableAsset parent);
    }
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

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(VariableAsset parent)
        {
            m_onValueChanged = parent.GetSubVariable<GameEvent<T>>();
        }

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

        public override T Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(VariableAsset parent)
        {
            m_variable = parent.GetSubVariable<Variable<T>>();
        }
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

        public override void GetAssetName(ref string name) => name = $"{name}_OnChanged";
        public override void OnAttached(VariableAsset parent) { }
    }

    public abstract class GameEventListenerBase<T> : GameEventListener<T>
    {
        [SerializeField] private GameEvent<T> m_event = default;

        public override IDisposable Subscribe(Action<T> action) => m_event?.Subscribe(action);
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEvent<T>>();
        }
    }
}