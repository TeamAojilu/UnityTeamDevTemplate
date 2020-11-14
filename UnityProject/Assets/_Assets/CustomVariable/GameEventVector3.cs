using UnityEngine;
using SilCilSystem.Variables;
using System;

[CreateAssetMenu(menuName = "GameEvents/Custom/Vector3")]
public class GameEventVector3 : GameEvent<Vector3>
{
    private event Action<Vector3> m_event;

    public override void Publish(Vector3 arg)
    {
        m_event?.Invoke(arg);
    }

    public override IDisposable Subscribe(Action<Vector3> action)
    {
        m_event += action;
        return new DelegateDispose(() => m_event -= action);
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(GameEventVector3))]
public class GameEventVector3Editor : SilCilSystem.Editors.GameEventEditor<GameEventVector3, GameEventVector3Listener> { }
#endif
