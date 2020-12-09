using UnityEngine;
using SilCilSystem.Variables;
using System;
using UnityEngine.Events;

namespace SilCilSystem.StateMachines
{
    public class PublishOnState : StateMachineBehaviour
    {
        [Serializable]
        private abstract class GameEventInfo<T, TEvent> where TEvent : GameEvent<T>
        {
            [SerializeField] private string m_parameterName = default;
            [SerializeField] private TEvent m_event = default;

            protected abstract T GetParameter(Animator animator, string name);

            public void Publish(Animator animator)
            {
                var value = GetParameter(animator, m_parameterName);
                m_event?.Publish(value);
            }
        }

        [Serializable]
        private class GameEventInfoFloat : GameEventInfo<float, GameEventFloat>
        {
            protected override float GetParameter(Animator animator, string name) => animator.GetFloat(name);
        }

        [Serializable]
        private class GameEventInfoInt : GameEventInfo<int, GameEventInt>
        {
            protected override int GetParameter(Animator animator, string name) => animator.GetInteger(name);
        }

        [Serializable]
        private class GameEventInfoBool : GameEventInfo<bool, GameEventBool>
        {
            protected override bool GetParameter(Animator animator, string name) => animator.GetBool(name);
        }

        [Header("On State Enter")]
        [SerializeField] private GameEvent m_onStateEnter = default;
        [SerializeField] private GameEventInfoInt[] m_onStateEnterInt = default;
        [SerializeField] private GameEventInfoFloat[] m_onStateEnterFloat = default;
        [SerializeField] private GameEventInfoBool[] m_onStateEnterBool = default;
        [SerializeField] private UnityEvent m_onStateEnterEvent = default;

        [Header("On State Exit")]
        [SerializeField] private GameEvent m_onStateExit = default;
        [SerializeField] private GameEventInfoInt[] m_onStateExitInt = default;
        [SerializeField] private GameEventInfoFloat[] m_onStateExitFloat = default;
        [SerializeField] private GameEventInfoBool[] m_onStateExitBool = default;
        [SerializeField] private UnityEvent m_onStateExitEvent = default;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_onStateEnter?.Publish();
            foreach (var info in m_onStateEnterInt) info?.Publish(animator);
            foreach (var info in m_onStateEnterFloat) info?.Publish(animator);
            foreach (var info in m_onStateEnterBool) info?.Publish(animator);
            m_onStateEnterEvent?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_onStateExit?.Publish();
            foreach (var info in m_onStateExitInt) info?.Publish(animator);
            foreach (var info in m_onStateExitFloat) info?.Publish(animator);
            foreach (var info in m_onStateExitBool) info?.Publish(animator);
            m_onStateExitEvent?.Invoke();
        }
    }
}