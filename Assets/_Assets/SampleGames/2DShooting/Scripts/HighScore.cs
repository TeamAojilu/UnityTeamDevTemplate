using UnityEngine;
using SilCilSystem.Variables;

namespace Samples.Shooting2D
{
	public class HighScore : MonoBehaviour
	{
		[Header("PlayerPrefs")]
		[SerializeField] private string m_highScoreKey = "highScore";

		[Header("Variables")]
		[SerializeField] private PropertyInt m_highScore = new PropertyInt(0);

		[Header("Events")]
		[SerializeField] private GameEventIntListener m_onScoreChanged = default;
		[SerializeField] private GameEventListener m_onGameOver = default;

		private void Start()
		{
			Initialize();
			m_onScoreChanged?.Subscribe(OnScoreChanged, gameObject);
			m_onGameOver?.Subscribe(OnGameOver, gameObject);
		}

        private void Initialize()
		{
			m_highScore.Value = PlayerPrefs.GetInt(m_highScoreKey, 0);
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
			PlayerPrefs.SetInt(m_highScoreKey, m_highScore.Value);
			PlayerPrefs.Save();
			Initialize();
		}
	}
}