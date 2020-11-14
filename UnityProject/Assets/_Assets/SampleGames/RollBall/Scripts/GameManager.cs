using System.Collections;
using UnityEngine;
using SilCilSystem.Variables;

namespace RollBall
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VariableBool m_isPlaying = default;
        [SerializeField] private ReadonlyFloat m_time = default;
        [SerializeField] private ReadonlyInt m_itemCount = default;

        public IEnumerator Start()
        {
            // ゲーム開始.
            m_isPlaying.Value = true; 

            // ステージにあるItemを全部取るか, 時間が無くなるまで待機.
            yield return new WaitUntil(() => m_itemCount.Value == 0 || m_time.Value < 0f);

            // ゲーム終了.
            m_isPlaying.Value = false;
        }
    }
}