using UnityEngine;
using SilCilSystem.Audio;

namespace SilCilSystem.Internals.Audio
{
    internal class AudioClipFromScriptableObject : MonoBehaviour
    {
        [SerializeField] private ScriptableObject m_audioClipResources = default;

        private void Awake()
        {
            AudioPlayer.Instance.Clips = m_audioClipResources as IAudioClipResources;
        }
    }
}