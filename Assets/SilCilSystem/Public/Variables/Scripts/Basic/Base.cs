using System;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Variables.Base
{
    public abstract class VariableAsset : ScriptableObject
    {
        [Conditional("UNITY_EDITOR")]
        /// <summary>Available only for UNITY_EDITOR</summary>
        public abstract void GetAssetName(ref string name);

        [Conditional("UNITY_EDITOR")]
        /// <summary>Available only for UNITY_EDITOR</summary>
        public abstract void OnAttached(IEnumerable<VariableAsset> variables);
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
        [SerializeField, HideInInspector] private GameEvent<T> m_onValueChanged = default;

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEvent<T> onChanged)
                {
                    m_onValueChanged = onChanged;
                    return;
                }
            }
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
        [SerializeField, HideInInspector] private Variable<T> m_variable = default;

        public override T Value => m_variable;

        public override void GetAssetName(ref string name) => name = $"{name}_Readonly";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variables is Variable<T> value)
                {
                    m_variable = value;
                    return;
                }
            }
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
        public override void OnAttached(IEnumerable<VariableAsset> variables) { }
    }

    public abstract class GameEventListenerBase<T> : GameEventListener<T>
    {
        [SerializeField, HideInInspector] private GameEvent<T> m_event = default;

        public override IDisposable Subscribe(Action<T> action) => m_event?.Subscribe(action);
        public override IDisposable Subscribe(Action action) => Subscribe(_ => action?.Invoke());

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEvent<T> onChanged)
                {
                    m_event = onChanged;
                    return;
                }
            }
        }
    }
}