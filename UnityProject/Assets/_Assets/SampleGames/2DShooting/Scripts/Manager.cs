using UnityEngine;
using SilCilSystem.Variables;
using System;

namespace Samples.Shooting2D
{
	public class Manager : MonoBehaviour
	{
		[Header("Events")]
		[SerializeField] private VariableBool m_isPlaying = default;
		[SerializeField] private VariableInt m_score = default;
		[SerializeField] private GameEventListener m_onGameOver = default;

		[Header("GameObjects")]
		[SerializeField] private GameObject m_title = default;
		[SerializeField] private GameObject m_playerPrefab = default;

		private IDisposable m_disposable = default;

        private void Awake()
        {
            m_isPlaying.Value = false;
			m_score.Value = 0;
			m_disposable = m_onGameOver.Subscribe(OnGameOver);
        }

        private void OnDestroy()
        {
			m_disposable?.Dispose();
        }

        private void Update()
		{
			if (m_isPlaying.Value) return;
         
			if (Input.GetKeyDown(KeyCode.X))
            {
				GameStart();
            }
		}

		private void GameStart()
		{
			m_isPlaying.Value = true;
			m_score.Value = 0;
			m_title.SetActive(false);
			Instantiate(m_playerPrefab, m_playerPrefab.transform.position, m_playerPrefab.transform.rotation);
		}

		private void OnGameOver()
		{
			m_title.SetActive(true);
			m_isPlaying.Value = false;
		}
	}
}