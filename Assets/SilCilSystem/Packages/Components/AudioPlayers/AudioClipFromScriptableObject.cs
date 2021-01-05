using UnityEngine;
using SilCilSystem.Singletons;

namespace SilCilSystem.Components.AudioPlayers
{
    public class AudioClipFromScriptableObject : MonoBehaviour
    {
        [SerializeField] private ScriptableObject m_audioClipResources = default;

        private void Awake()
        {
            AudioPlayer.Instance.Clips = m_audioClipResources as IAudioClipResources;
        }
    }
}