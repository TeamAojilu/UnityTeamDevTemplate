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
        [SerializeField] private ReadonlyFloat m_time = default;
        [SerializeField] private ReadonlyInt m_itemCount = default;
        
        [Header("Next Scene")]
        [SerializeField] private string m_nextScene = "Title";
        [SerializeField] private float m_waitTimeToNextScene = 3f;

        public IEnumerator Start()
        {
            // 遷移処理待ち.
            yield return SceneLoader.WaitLoading;

            // ゲーム開始.
            m_isPlaying.Value = true; 

            // ステージにあるItemを全部取るか, 時間が無くなるまで待機.
            yield return new WaitUntil(() => m_itemCount.Value == 0 || m_time.Value < 0f);

            // ゲーム終了.
            m_isPlaying.Value = false;

            // しばらく待機してから次のシーンへ.
            yield return new WaitForSeconds(m_waitTimeToNextScene);
            SceneLoader.LoadScene(m_nextScene);
        }
    }
}