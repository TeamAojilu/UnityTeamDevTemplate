using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class EventVector3Int : GameEventVector3Int
    {
        private event Action<Vector3Int> m_event = default;

        public override void Publish(Vector3Int value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<Vector3Int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }

        public override void GetAssetName(ref string name) => name = $"{name}_OnChanged";
        public override void OnAttached(IEnumerable<VariableAsset> variables) { }
    }
}