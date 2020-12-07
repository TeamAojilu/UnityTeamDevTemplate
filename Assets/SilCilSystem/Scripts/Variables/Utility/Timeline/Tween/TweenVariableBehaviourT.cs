using System;
using UnityEngine.Playables;

namespace SilCilSystem.Variables.Timeline
{
    public class TweenVariableBehaviour<T> : PlayableBehaviour
    {
        public Variable<T> Variable { get; set; }
        public T StartValue { get; set; }
        public T EndValue { get; set; }
        public Func<T, T, float, T> Interpolate { get; set; }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            Variable.Value = Interpolate(StartValue, EndValue, 0f);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            Variable.Value = Interpolate(StartValue, EndValue, 1f);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var progress = (float)(playable.GetTime() / playable.GetDuration());
            Variable.Value = Interpolate(StartValue, EndValue, progress);
        }
    }
}