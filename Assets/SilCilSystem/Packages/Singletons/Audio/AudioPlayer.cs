using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;
using SilCilSystem.Singletons;

namespace SilCilSystem.Audio
{
    public class AudioPlayer : SingletonMonoBehaviour<AudioPlayer>
    {
		public static void PlayBGM(AudioClip clip) => Instance._PlayBGM(clip);
		public static void PlaySE(AudioClip clip, Vector3 worldPosition = default) => Instance._PlaySE(clip, worldPosition);

		[Header("Volume")]
		[SerializeField] private ReadonlyPropertyFloat m_defaultBGMVolume = new ReadonlyPropertyFloat(1f);
		[SerializeField] private ReadonlyPropertyFloat m_defaultSEVolume = new ReadonlyPropertyFloat(1f);

		[Header("BGM")]
		[SerializeField] private ReadonlyPropertyBool m_loop = new ReadonlyPropertyBool(true);
		[SerializeField] private ReadonlyPropertyFloat m_fadeTime = new ReadonlyPropertyFloat(0.2f);
		[SerializeField] private InterpolationCurve m_fadeCurve = default;

		[Header("Audio Sources")]
		[SerializeField] private AudioSource m_bgmSource = default;
		[SerializeField] private AudioSource[] m_seSources = default;
		
        protected override void OnAwake() { }
		protected override void OnDestroyCallback() { }

		private void _PlayBGM(AudioClip clip)
        {
			m_nextBGM = clip;
			m_multiply = -1f;
        }

		private void _PlaySE(AudioClip clip, Vector3 worldPosition)
        {
            foreach (var item in m_seSources)
            {
				if (item == null || item.isPlaying) continue;
				item.clip = clip;
				item.transform.position = worldPosition;
				item.Play();
				return;
            }
#if UNITY_EDITOR
			Debug.Log("AudioPlayer: PlaySE is skipped.");
#endif
		}

		private float m_multiply = 0f;
		private float m_volumeRate = 0f;

		private AudioClip m_nextBGM = default;

		private void Update()
        {
            // SEの音量を設定.
            foreach (var item in m_seSources)
            {
				item.volume = m_defaultSEVolume;
            }

            // BGMのフェード処理.
            if (m_fadeTime > 0f)
            {
				m_volumeRate += Time.deltaTime * m_multiply / m_fadeTime;
				m_volumeRate = Mathf.Clamp01(m_volumeRate);
            }
            else
            {
				m_volumeRate = (m_nextBGM == null) ? 1f : 0f;
            }

			// BGMの音量が0になったら、次のBGMに切り替え.
			if(m_volumeRate == 0f && m_nextBGM != null)
            {
				m_bgmSource.clip = m_nextBGM;
				m_bgmSource.Play();
				m_multiply = 1f;
				m_nextBGM = null;
            }

			m_bgmSource.loop = m_loop;
			m_bgmSource.volume = Mathf.Lerp(0f, m_defaultBGMVolume, m_fadeCurve.Evaluate(m_volumeRate));
        }
	}
}