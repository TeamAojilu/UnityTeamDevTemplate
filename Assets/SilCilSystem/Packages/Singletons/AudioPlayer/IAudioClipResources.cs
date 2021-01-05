using UnityEngine;

namespace SilCilSystem.Singletons
{
	public interface IAudioClipResources
    {
		AudioClip GetClip(string name);
    }
}