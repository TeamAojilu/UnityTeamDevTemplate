using System;
using UnityEngine;
using UnityEngine.Playables;

namespace SilCilSystem.Variables.Timeline
{
    [Serializable]
    public class BoolActivationAsset : PlayableAsset
    {
        [SerializeField] private VariableBool m_variable = default;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var playable = ScriptPlayable<BoolActivationBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.Variable = m_variable;
            return playable;
        }
    }
}