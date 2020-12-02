using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;
using System;

namespace SilCilSystem.Singletons
{
    public class AudioPlayer : SingletonMonoBehaviour<AudioPlayer>
    {
		public static void PlayBGM(string name) => Instance._PlayBGM(name);
		public static void PlaySE(string name) => Instance._PlaySE(name);

		[Header("Volume")]
		[SerializeField] private ReadonlyPropertyFloat m_defaultBGMVolume = new ReadonlyPropertyFloat(1f);
		[SerializeField] private ReadonlyPropertyFloat m_defaultSEVolume = new ReadonlyPropertyFloat(1f);

		[Header("BGM")]
		[SerializeField] private ReadonlyPropertyBool m_loop = new ReadonlyPropertyBool(true);
		[SerializeField] private ReadonlyPropertyFloat m_fadeTime = new ReadonlyPropertyFloat(0.2f);
		[SerializeField] private InterpolationCurve m_fadeCurve = default; 

		[Header("Audio Sources")]
		[SerializeField] private AudioSource m_bgmSource = default;
		[SerializeField] private AudioSource m_seSource = default;

		[Header("Events")]
		[SerializeField] private GameEventStringListener m_playBGM = default;
		[SerializeField] private GameEventStringListener m_playSE = default;

		private IDisposable m_disposable = default;

		public IAudioClipResources Clips { get; set; }
		
        protected override void OnAwake() 
		{
			var disposable = new CompositeDisposable();
			disposable.Add(m_playBGM?.Subscribe(_PlayBGM));
			disposable.Add(m_playSE?.Subscribe(_PlaySE));
			m_disposable = disposable;
		}

        protected override void OnDestroyCallback() 
		{
			m_disposable?.Dispose();
		}

		protected void _PlayBGM(string name)
        {
			m_nextBGM = Clips?.GetClip(name);
			m_multiply = -1f;
        }

		protected void _PlaySE(string name)
        {
			var clip = Clips?.GetClip(name);
			m_seSource.PlayOneShot(clip);
        }

		private float m_multiply = 0f;
		private float m_volumeRate = 0f;

		private AudioClip m_nextBGM = default;

		private void Update()
        {
			// SEの音量を設定.
			m_seSource.volume = m_defaultSEVolume;

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