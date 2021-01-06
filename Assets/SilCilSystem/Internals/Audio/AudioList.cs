using System;
using UnityEngine;
using SilCilSystem.Audio;

namespace SilCilSystem.Internals.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioList), menuName = nameof(AudioList), order = 1)]
    internal class AudioList : ScriptableObject, IAudioClipResources
    {
        [Serializable]
        private class AudioInfo
        {
            public string name = "name";
            public AudioClip clip = default;
        }

        [SerializeField] private AudioInfo[] audioInfos = default;

        public AudioClip GetClip(string name)
        {
            foreach(var info in audioInfos)
            {
                if (info.name == name) return info.clip;
            }
            return null;
        }
    }
}