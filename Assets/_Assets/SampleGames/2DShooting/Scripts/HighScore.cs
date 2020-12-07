using UnityEngine;
using SilCilSystem.Variables;
using System;

namespace Samples.Shooting2D
{
	public class HighScore : MonoBehaviour
	{
		private const string HighScoreKey = "highScore";

		[Header("Variables")]
		[SerializeField] private VariableInt m_highScore = default;

		[Header("Events")]
		[SerializeField] private GameEventIntListener m_onScoreChanged = default;
		[SerializeField] private GameEventListener m_onGameOver = default;

		private IDisposable m_disposable;

		private void Start()
		{
			Initialize();

			var disposable = new CompositeDisposable();
			disposable.Add(m_onScoreChanged.Subscribe(OnScoreChanged));
			disposable.Add(m_onGameOver.Subscribe(OnGameOver));
			m_disposable = disposable;
		}

        private void OnDestroy()
        {
			m_disposable?.Dispose();
        }

        private void Initialize()
		{
			m_highScore.Value = PlayerPrefs.GetInt(HighScoreKey, 0);
		}
		
		private void OnScoreChanged(int score)
        {
			if (score > m_highScore.Value)
			{
				m_highScore.Value = score;
			}
		}

		private void OnGameOver()
		{
			PlayerPrefs.SetInt(HighScoreKey, m_highScore.Value);
			PlayerPrefs.Save();
			Initialize();
		}
	}
}