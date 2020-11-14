using UnityEngine;
using SilCilSystem.Variables;
using System;

public class GameEventVector3Listener : GameEventListener<Vector3>, IGameEventSetter<GameEventVector3>
{
    [SerializeField] private GameEventVector3 m_gameEvent = default;

    public void SetGameEvent(GameEventVector3 gameEvent)
    {
        m_gameEvent = gameEvent;
    }

    public override IDisposable Subscibe(Action<Vector3> action)
    {
        return m_gameEvent.Subscribe(action);
    }
}
