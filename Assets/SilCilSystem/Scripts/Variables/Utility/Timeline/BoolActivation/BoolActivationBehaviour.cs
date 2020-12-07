using UnityEngine.Playables;

namespace SilCilSystem.Variables.Timeline
{
    public class BoolActivationBehaviour : PlayableBehaviour
    {    
        public Variable<bool> Variable { get; set; }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            Variable.Value = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            Variable.Value = false;
        }
    }
}