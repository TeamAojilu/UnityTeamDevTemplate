using System;
using UnityEngine;
using UnityEngine.Playables;
using SilCilSystem.Math;

namespace SilCilSystem.Variables.Timeline
{
    [Serializable]
    public abstract class TweenVariableAsset<T, TBehaviour> : PlayableAsset
        where TBehaviour : TweenVariableBehaviour<T>, new()
    {
        [SerializeField] private T m_startValue = default;
        [SerializeField] private T m_endValue = default;
        [SerializeField] private InterpolationCurve m_curve = default;
        
        private T Interpolate(T start, T end, float t)
        {
            return Lerp(start, end, m_curve.Evaluate(t));
        }

        protected abstract T Lerp(T start, T end, float t);
        protected abstract Variable<T> GetVariable();
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var playable = ScriptPlayable<TBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.Variable = GetVariable();
            behaviour.StartValue = m_startValue;
            behaviour.EndValue = m_endValue;
            behaviour.Interpolate = Interpolate;
            return playable;
        }
    }
}