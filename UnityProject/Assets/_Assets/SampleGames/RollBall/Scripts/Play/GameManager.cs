using System.Collections;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Singletons;

namespace Samples.RollBall
{
    public class GameManager : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] private VariableBool m_isPlaying = default;
        [SerializeField] private ReadonlyInt m_itemCount = default;
        
        [Header("Next Scene")]
        [SerializeField] private string m_nextScene = "Title";
        [SerializeField] private float m_waitTimeToNextScene = 3f;

        [Header("Audio")]
        [SerializeField] private string m_gameOverKey = "RollBall_GameOver";
        [SerializeField] private string m_gameClearKey = "RollBall_GameClear";

        public IEnumerator Start()
        {
            // 遷移処理待ち.
            yield return SceneLoader.WaitLoading;

            // ゲーム開始.
            m_isPlaying.Value = true;

            // ゲーム終了まで待機.
            yield return new WaitWhile(() => m_isPlaying);

            // しばらく待機してから次のシーンへ.
            yield return new WaitForSeconds(m_waitTimeToNextScene);
            SceneLoader.LoadScene(m_nextScene);
        }

        private void Update()
        {
            if (!m_isPlaying) return;
            if (m_itemCount == 0) FinishGame(true);
        }

        public void FinishGame(bool isClear)
        {
            m_isPlaying.Value = false;
            AudioPlayer.PlaySE((isClear) ? m_gameClearKey : m_gameOverKey);
        }
    }
}