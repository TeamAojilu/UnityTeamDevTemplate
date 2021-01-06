using UnityEngine;

namespace SilCilSystem.Audio
{
	public interface IAudioClipResources
    {
		AudioClip GetClip(string name);
    }
}